import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, FormGroupDirective, ValidatorFn } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { LibraryRestService } from '../../services/library-rest.service';
import { UserEditorDataService } from 'src/app/services/user-editor-data.service';

import { ControlNames } from '../../constants/controlNames.enum';
import { HttpErrorResponse } from '@angular/common/http';
import { phoneHint } from '../../constants/constants';


@Component({
  selector: 'app-user-editor',
  templateUrl: './user-editor.component.html',
  styleUrls: ['./user-editor.component.css'],
})
export class UserEditorComponent implements OnInit, OnDestroy {
  public userDataForm: FormGroup;

  private destroy$: Subject<void> = new Subject<void>();

  public readonly controlNames: typeof ControlNames = ControlNames;

  constructor(
    private rest: LibraryRestService,
    private toastr: ToastrService,
    private userDataService: UserEditorDataService,
  ) {
    this.userDataForm = this.userDataService.createFormGroup();
  }

  public ngOnInit(): void {
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  /* Gets Email validators */
  public get EmailValidators(): ValidatorFn[] {
    return this.userDataService.getEmailValidators();
  }

  /* Gets phone validators */
  public get PhoneValidators(): ValidatorFn[] {
    return this.userDataService.getPhoneValidators();
  }

  /* Gets hint for phone format */
  public get phoneHint(): string {
    return phoneHint;
  }

  /* Gets FormArray from FORM that correspones to control name */
  public getFormArray(name: ControlNames): FormArray {
    return this.userDataForm.get(name) as FormArray;
  }

   /* Gets FormControl from FORM that correspones to control name */
  public getFormControl(name: ControlNames): FormControl {
    return this.userDataForm.get(name) as FormControl;
  }

  /* Submits form => Calls add user service */
  public submit(formDirective: FormGroupDirective): void {
    if (this.userDataForm.invalid) {
      return;
    }

    const formData = this.userDataService.transformFormDataIntoUserData(this.userDataForm.value);

    this.rest
      .createNewUser(formData)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (_: any): void => {
          this.toastr.success(
            'User successfully added to database',
            'Create User Request Success',
            {
              positionClass: 'toast-center-center',
            });

          this.resetForm(formDirective);
        },
        (error: HttpErrorResponse): void => {

          this.toastr.error(
            JSON.stringify(error.error.errors),
            'One or more validation errors occurred.',
            {
              positionClass: 'toast-center-center',
            });
        }
      );
  }

   /* Resets form */
  private resetForm(formDirective: FormGroupDirective): void {

    formDirective.resetForm();
    this.getFormArray(this.controlNames.emails).clear();
    this.getFormArray(this.controlNames.phones).clear();
    this.userDataForm.reset();
  }

}
