import { Component, ElementRef, Input, OnInit, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements ControlValueAccessor,OnInit {
 
  @Input() type = 'text';
  @Input() label: string = '';

  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this;
  }

  ngOnInit() {
    // const control = this.controlDir.control;
    // const validators = control?.validator ? [control.validator] : [];
    // const asyncValidators = control?.asyncValidator ? [control.asyncValidator] : [];

    // control?.setValidators(validators);
    // control?.setAsyncValidators(asyncValidators);
    // control?.updateValueAndValidity();
  }

  onChange(event:any) { }

  onTouched() { }

  writeValue(obj: any): void {
  }

  registerOnChange(fn: any): void {
  }

  registerOnTouched(fn: any): void {
  }
  get control():FormControl{
    return this.controlDir.control as FormControl;
  }
  
}
