import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Register } from 'src/app/models/register';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  constructor(private accountService: AccountService, private router: Router) { }

  errors: string[] = [];
  form: FormGroup;

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl('', [Validators.required]),
      surname: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.email]),
      password: new FormControl('', [Validators.required]),
      passwordConfirm: new FormControl('', [Validators.required])
    });
  }

  signup() {
    this.errors = [];

    const register: Register = 
    { 
      name: this.form.value.name,
      surname: this.form.value.surname,
      email: this.form.value.email, 
      password: this.form.value.password,
      passwordConfirm: this.form.value.passwordConfirm
    };

    this.accountService.register(register).subscribe(() => 
    { 
      this.router.navigate(['tasks']);
    }, err => {
      this.errors = err.error;
      console.log(this.errors);
    });
  }

  getEmailErrorMessage() {
    if (this.form.get('email').hasError('required')) {
      return 'Email is required';
    }
    return 'Email is invalid';
  }

  getPasswordConfirmErrorMessage() {
    if (this.form.get('passwordConfirm').hasError('required')) {
      return 'Password confirmation is required';
    }
    return 'Passwords do not match'
  }
}
