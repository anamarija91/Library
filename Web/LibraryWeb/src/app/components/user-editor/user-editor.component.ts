import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { LibraryRestService } from '../../services/library-rest.service';

import { UserData } from 'src/models/userData';
import { EmailData } from '../../../models/emailData';
import { PhoneData } from 'src/models/phoneData';
import { ControlNames } from '../../constants/controlNames.enum';
import { HttpErrorResponse } from '@angular/common/http';


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
    private formBuilder: FormBuilder,
    protected cdr: ChangeDetectorRef,
    private rest: LibraryRestService,
    private toastr: ToastrService) {
    this.userDataForm = this.createFormGroup();
  }

  public ngOnInit(): void {
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  /* Creates initial form setup */
  public createFormGroup(): FormGroup {
    return this.formBuilder.group({
      firstName: new FormControl('', [
        Validators.required,
        Validators.minLength(2),
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.minLength(2),
      ]),
      dateOfBirth: new FormControl('', Validators.required),
      emails: this.formBuilder.array([]),
      phones: this.formBuilder.array([]),
    });
  }

  /* Gets FormArray from FORM that correspones to control name */
  public getFormArray(name: ControlNames): FormArray {
    return this.userDataForm.get(name) as FormArray;
  }

   /* Gets FormControl from FORM that correspones to control name */
  public getFormControl(name: ControlNames): FormControl {
    return this.userDataForm.get(name) as FormControl;
  }

  /* Fixes date value (toIsoString() returns selected - 1) */
  public date(e: any): void {
    const date = new Date(e.target.value);
    date.setDate(date.getDate() + 1);

    const convertDate = date.toISOString().substring(0, 10);
    this.userDataForm.get(ControlNames.dateOfBirth).setValue(convertDate, {
      onlyself: true,
    });
  }

  /* Submits form => Calls add user service */
  public submit(formDirective: FormGroupDirective): void {
    if (this.userDataForm.invalid) {
      return;
    }

    const formData = this.getCreateUserRequest();

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

  /* Adds new item to FormArray, based on control name item is added to emails or phones */
  public addItem(name: ControlNames): void {
    if (name === ControlNames.emails) {
      const emailGroup = this.formBuilder.group({
            email: new FormControl('', [Validators.email]),
          });

      this.getFormArray(ControlNames.emails).push(emailGroup);
    } else if (name === ControlNames.phones) {
      const phoneGroup = this.formBuilder.group({
            phone: new FormControl('', [Validators.pattern(/^\+385-(1|[1-9][0-9])-[0-9]{3}-[0-9]{4}$/)])
          });

      this.getFormArray(ControlNames.phones).push(phoneGroup);
    }
  }

  /* Deletes item from FormArray, based on control name item is removed from emails or phones */
  public deleteItem(i: number, name: ControlNames): void {
    this.getFormArray(name).removeAt(i);
  }

  /* Gets error message for defined error */
  public getErrorMessage(control: FormControl): string {

    if (control.hasError('required')) {
      return 'You must enter a value';
    }

    return control.hasError('minlength') ? 'You must enter at least 2 characters' : '';
  }

  private resetForm(formDirective: FormGroupDirective): void {

    formDirective.resetForm()
    this.userDataForm.reset();
  }

  /* Transforms form data into user data for Create user call on backend */
  private getCreateUserRequest(): UserData {

    const value = this.userDataForm.value;
    const emails = (value.emails as EmailData[]).map((val: EmailData): string => val.email)
                                                .filter((e: string): any => e);
    const phones = (value.phones as PhoneData[]).map((val: PhoneData): string => val.phone)
                                                .filter((p: string): any => p);

    const formData = {
      firstName: value[ControlNames.firstName],
      lastName: value.lastName,
      dateOfBirth: (value.dateOfBirth as string).split('-').reverse().join('/'),
      emails ,
      phones
    };

    return formData;
  }
}
