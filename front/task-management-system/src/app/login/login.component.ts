import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginModel } from '../models/loginModel';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private accountService: AccountService, private router: Router) { }

  @ViewChild("form", { static: false }) form: NgForm;
  errors: string[] = [];

  ngOnInit(): void {
  }

  login() {
    this.errors = [];

    const loginModel: LoginModel = 
    { 
      email: this.form.value.email, 
      password : this.form.value.password
    };

    console.log(loginModel);

    this.accountService.login(loginModel).subscribe(() => 
    { 
      this.router.navigate(['tasks']);
    }, err => {
      this.errors = err.error;
      console.log(this.errors);
    });
  }
}
