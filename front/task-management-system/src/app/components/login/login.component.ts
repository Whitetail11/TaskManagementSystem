import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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

  errors: string[] = [];
  form: FormGroup;

  ngOnInit(): void {
    this.form = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required])
      });
  }

  login() {
    this.errors = [];
    console.log(this.form);

    const login: Login = this.form.value;
      
    this.accountService.login(login).subscribe(() => 
    { 
      this.router.navigate(['tasks']);
    }, err => {
      this.errors = err.error;
      console.log(this.errors);
    });
  }

  getEmailErrorMessage() {
    if (this.form.get('email').hasError('required'))
    {
      return 'Email is required';
    }
    return 'Email is invalid';
  }
}
