import { Component } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { debounceTime, finalize, map, Observable, switchMap, take, timer } from 'rxjs';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  errors: string[] | null = null;
  constructor(private fb: FormBuilder, private router:Router, private accountService: AccountService){}

  complexPassword = "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{\":;'?/<>.,])(?!.*\\s).{6,10}$"

  // registerForm = this.fb.group({
  //   displayName: [null,Validators.required],
  //   email: [null,Validators.required,Validators.email,[this.validateEmailNotTaken()]],
  //   password: [null,Validators.required,Validators.pattern(this.complexPassword)]
  // })
  // registerForm = new FormGroup({
  //   displayName: new FormControl('',[Validators.required]),
  //   email: new FormControl('',[Validators.required,Validators.email],[this.validateEmailNotTaken()]),
  //   password: new FormControl('',[Validators.required,Validators.pattern(this.complexPassword)]),
  // })
  registerForm = new FormGroup({
    displayName: new FormControl(null, Validators.required),
    email: new FormControl(null, [Validators.required, Validators.email], [this.validateEmailNotTaken()]),
    password: new FormControl(null, [Validators.required, Validators.pattern(this.complexPassword)])
  });

  onSubmit(){
    this.accountService.register(this.registerForm.value).subscribe({
      next: ()=>this.router.navigateByUrl('/shop'),
      error:error=>this.errors = error.errors
    })
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return (control:AbstractControl) => {
      return control.valueChanges.pipe(
        debounceTime(1000),
        take(1),
        switchMap(() => {
          return this.accountService.checkEmailExists(control.value).pipe(
            map(res => {
              return res ? { emailExists: true } : null;
            })
          );
        })
      )
    };
  }
}
