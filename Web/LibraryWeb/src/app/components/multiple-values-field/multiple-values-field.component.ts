import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormControl, ValidatorFn, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-multiple-values-field',
  templateUrl: './multiple-values-field.component.html',
  styleUrls: ['./multiple-values-field.component.css']
})
export class MultipleValuesFieldComponent implements OnInit {
  @Input() public formArray: FormArray;
  @Input() public label: string;
  @Input() public validators: ValidatorFn[];
  @Input() public hint: string;

  constructor(private formBuilder: FormBuilder) { }

  public ngOnInit(): void { }


  /* Adds new item to FormArray */
  public addItem(): void {
    const formGroup = this.formBuilder.group({
      element: new FormControl('', this.validators)
    });

    this.formArray.push(formGroup);
  }

  /* Deletes item from FormArray */
  public deleteItem(i: number): void {
    this.formArray.removeAt(i);
  }

}
