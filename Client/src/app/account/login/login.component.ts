import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  complexPassword = "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{\":;'?/<>.,])(?!.*\\s).{6,10}$"
  loginForm = new FormGroup({
    email: new FormControl('',[Validators.required,Validators.email]),
    password: new FormControl('',[Validators.required,Validators.pattern(this.complexPassword)]),
  })
  returnUrl?: string;
  constructor(private accountService:AccountService, private router:Router, private activatedRoute:ActivatedRoute){
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/shop';
  }

  onSubmit(){
    this.accountService.login(this.loginForm.value).subscribe({
      next:user=>{
        if(this.returnUrl)
        this.router.navigateByUrl(this.returnUrl);
      },
      error:error=>console.log(error)
    })
  }
}
