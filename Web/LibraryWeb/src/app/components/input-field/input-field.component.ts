import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { GetClassNameIfMandatory } from 'src/app/helpers/common';

@Component({
  selector: 'app-input-field',
  templateUrl: './input-field.component.html',
  styleUrls: ['./input-field.component.css']
})
export class InputFieldComponent implements OnInit {
  @Input() public control: FormControl;
  @Input() public label: string;
  @Input() public isRequired: boolean;

  constructor() { }

  public ngOnInit(): void {
  }

  /* Gets Input field component error message */
  public getErrorMessage(): string {
    if (this.control.hasError('required')) {
      return 'You must enter a value';
    }

    return this.control.hasError('minlength')
      ? 'You must enter at least ' + this.control.errors.minlength.requiredLength + ' characters'
      : '';
  }

  /* Gets class attribute value */
  public getClassMandatoryName(): string {
    return GetClassNameIfMandatory(this.isRequired);
  }
}
