import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { LibraryRestService } from '../../services/library-rest.service';

import { UserData } from 'src/models/userData';
import { EmailData } from '../../../models/emailData';
import { PhoneData } from 'src/models/phoneData';
import { ControlNames } from '../../constants/controlNames.enum';

@Component({
  selector: 'app-user-editor',
  templateUrl: './user-editor.component.html',
  styleUrls: ['./user-editor.component.css'],
})
export class UserEditorComponent implements OnInit, OnDestroy {
  public userDataForm: FormGroup;

  private formData: UserData;

  private destroy$: Subject<void> = new Subject<void>();

  public readonly controlNames: typeof ControlNames = ControlNames;

  constructor(private formBuilder: FormBuilder, protected cdr: ChangeDetectorRef, private rest: LibraryRestService) {
    this.userDataForm = this.createFormGroup();
  }

  public ngOnInit(): void {
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public getFormArray(name: ControlNames): FormArray {
    return this.userDataForm.get(name) as FormArray;
  }

  public getFormControl(name: ControlNames): FormControl {
    return this.userDataForm.get(name) as FormControl;
  }

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

  public date(e: any): void {
    const date = new Date(e.target.value);
    date.setDate(date.getDate() + 1);

    const convertDate = date.toISOString().substring(0, 10);
    this.userDataForm.get(ControlNames.dateOfBirth).setValue(convertDate, {
      onlyself: true,
    });
  }

  public submit(): void {
    if (this.userDataForm.invalid) {
      return;
    }

    const value = this.userDataForm.value;

    this.formData = {
      firstName: value[ControlNames.firstName],
      lastName: value.lastName,
      dateOfBirth: (value.dateOfBirth as string).split('-').reverse().join('/'),
      emails: (value.emails as EmailData[]).map((val: EmailData): string => val.email).join(','),
      phones: (value.phones as PhoneData[]).map((val: PhoneData): string => val.phone).join(',')
    };
    console.log(this.formData);

    this.rest
      .createNewUser(this.formData)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (response) => console.log(response),
        (error) => console.log(error),
      );
  }

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

  public deleteItem(i: number, name: ControlNames): void {
    this.getFormArray(name).removeAt(i);
  }

  public getNameErrorMessage(control: FormControl): string {

    if (control.hasError('required')) {
      return 'You must enter a value';
    }

    return control.hasError('minlength') ? 'You must enter at least 2 characters' : '';
  }
}
