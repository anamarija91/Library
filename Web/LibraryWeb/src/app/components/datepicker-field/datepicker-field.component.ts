import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl } from '@angular/forms';

import { GetClassNameIfMandatory } from 'src/app/helpers/common';

@Component({
  selector: 'app-datepicker-field',
  templateUrl: './datepicker-field.component.html',
  styleUrls: ['./datepicker-field.component.css']
})
export class DatepickerFieldComponent implements OnInit {
  @Input() public control: AbstractControl;
  @Input() public label: string;
  @Input() public isRequired: boolean;

  constructor() { }

  public ngOnInit(): void {
  }

  /* Sets maximum date value */
  public getMaxDate(): Date {
    const today = new Date();

    today.setFullYear(today.getFullYear() - 3);

    return today;
  }

  public getClassMandatoryName(): string {
    return GetClassNameIfMandatory(this.isRequired);
  }

  /* Fixes date value (toIsoString() returns selected - 1) */
  public date(e: any): void {
    const date = new Date(e.target.value);
    date.setDate(date.getDate() + 1);

    const convertDate = date.toISOString().substring(0, 10);
    (this.control as AbstractControl).setValue(convertDate, { onlySelf: true });
  }

  /* Gets date error message */
  public getErrorMessage(): string {
    return this.control.hasError('required') ? 'You must pick a date value' : '';
  }

}
