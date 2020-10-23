import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/login';
import { AccountService } from 'src/app/services/account.service';

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

    const login: Login = 
    { 
      email: this.form.value.email, 
      password : this.form.value.password
    };

    this.accountService.login(login).subscribe(() => 
    { 
      this.router.navigate(['tasks']);
    }, err => {
      this.errors = err.error;
      console.log(this.errors);
    });
  }
}
