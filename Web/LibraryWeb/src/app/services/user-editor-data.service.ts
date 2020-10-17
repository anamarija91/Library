import { Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';

import { UserData } from 'src/models/userData';
import { UserEditorForm } from '../../models/general/formModels/userEditorForm';
import { dateFormat, phoneRegex } from '../constants/constants';
import { ElementData } from 'src/models/general/formModels/elementData';
import { FormGroup, ValidatorFn, Validators, FormBuilder, FormControl } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class UserEditorDataService {

  public constructor(private datePipe: DatePipe, private formBuilder: FormBuilder) { }

  /* Creates initial form setup */
  public createFormGroup(): FormGroup {
      return this.formBuilder.group({
        firstName: new FormControl('', this.getNameValidators()),
        lastName: new FormControl('', this.getNameValidators()),
        dateOfBirth: new FormControl('', Validators.required),
        emails: this.formBuilder.array([]),
        phones: this.formBuilder.array([]),
      });
    }

  /* Transforms form data into user data for Create user call on backend */
  public transformFormDataIntoUserData(userData: UserEditorForm): UserData {

    const formData = {
      firstName: userData.firstName,
      lastName: userData.lastName,
      dateOfBirth: this.datePipe.transform(userData.dateOfBirth, dateFormat),
      emails: this.getElementData(userData.emails),
      phones: this.getElementData(userData.phones)
    };

    return formData;
  }

  /* Transfom array values from FormArray */
  private getElementData(elementData: ElementData[]): string[] {
    return elementData.map((val: ElementData): string => val.element)
                      .filter((e: string): string => e);

  }

  public getEmailValidators(): ValidatorFn[] {
    return [Validators.email];
  }

  public getPhoneValidators(): ValidatorFn[] {
    return [Validators.pattern(phoneRegex)];
  }

  public getNameValidators(): ValidatorFn[] {
    return [
      Validators.required,
      Validators.minLength(2),
    ];
  }
}
