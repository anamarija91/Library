import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { LibraryRestService } from '../../services/library-rest.service';

import { UserData } from 'src/models/userData';
import { EmailData } from '../../../models/emailData';

@Component({
  selector: 'app-user-editor',
  templateUrl: './user-editor.component.html',
  styleUrls: ['./user-editor.component.css'],
})
export class UserEditorComponent implements OnInit, OnDestroy {
  public userDataForm: FormGroup;

  private formData: UserData;

  public emailControl = new FormControl('', [Validators.required, Validators.email]);

  public firstNameControl = new FormControl('', [
    Validators.required,
    Validators.pattern('^[a-zA-Z]+$'),
    Validators.minLength(2),
  ]);

  private destroy$: Subject<void> = new Subject<void>();

  constructor(private formBuilder: FormBuilder, protected cdr: ChangeDetectorRef, private rest: LibraryRestService) {
    this.userDataForm = this.createFormGroup();
  }

  public ngOnInit(): void {
    this.addEmail();
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public get phoneForms(): FormArray {
    return this.userDataForm.get('phones') as FormArray;
  }

  public get emailForms(): FormArray {
    return this.userDataForm.get('emails') as FormArray;
  }

  public createFormGroup(): FormGroup {
    return this.formBuilder.group({
      firstName: this.firstNameControl,
      lastName: new FormControl('', [Validators.required, Validators.pattern('^[a-zA-Z]+$')]),
      dateOfBirth: new FormControl('', Validators.required),
      address: new FormControl('', Validators.required),
      emails: this.formBuilder.array([]),
      // phones: this.formBuilder.array([]),
    });
  }

  public date(e: any): void {
    const date = new Date(e.target.value);
    date.setDate(date.getDate() + 1);

    const convertDate = date.toISOString().substring(0, 10);
    this.userDataForm.get('dateOfBirth').setValue(convertDate, {
      onlyself: true,
    });
  }

  public onSubmit(): void {
    if (this.userDataForm.invalid) {
      return;
    }

    const value = this.userDataForm.value;

    console.log(this.userDataForm.value.emails);

    this.formData = {
      firstName: value.firstName,
      lastName: value.lastName,
      dateOfBirth: (value.dateOfBirth as string).split('-').reverse().join('/'),
      email: (value.emails as EmailData[]).map((val: EmailData) => val.email).join(','),
      address: value.address,
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

  public addPhone(): void {
    const phoneGroup = this.formBuilder.group({
      phone: '',
    });

    this.phoneForms.push(phoneGroup);
  }

  public addEmail(): void {
    const emailGroup = this.formBuilder.group({
      email: new FormControl('', [Validators.email]),
    });

    this.emailForms.push(emailGroup);
  }

  public deletePhone(i): void {
    this.phoneForms.removeAt(i);
  }

  public deleteEmail(i): void {
    this.emailForms.removeAt(i);
  }

  public getNameErrorMessage() {
    if (this.firstNameControl.hasError('required')) {
      return 'You must enter a value';
    }

    if (this.firstNameControl.hasError('minlength')) {
      return 'You must enter at least 2 characters';
    }

    return this.firstNameControl.hasError('pattern') ? 'Only letters are allowed' : '';
  }
}
