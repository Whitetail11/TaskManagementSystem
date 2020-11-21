import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ForgotPassword } from 'src/app/models/forgotPassword';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  form: FormGroup;
  errors: string[] = [];
  linkIsSent: boolean = false;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email])
    });    
  }

  forgotPassword() {
    this.errors = [];
    const forgotPassword: ForgotPassword = this.form.value;

    this.accountService.forgotPassword(forgotPassword).subscribe(() => {
      this.linkIsSent = true;
    }, (error) => {
      this.errors = error.error;
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
