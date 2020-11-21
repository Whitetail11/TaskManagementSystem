import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ResetPassword } from 'src/app/models/resetPassword';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {

  form: FormGroup;
  userId: string;
  code: string;
  errors: string[] = [];

  constructor(private route: ActivatedRoute, 
    private accountService: AccountService,
    private router: Router,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.userId = params['userId']; 
      this.code = params['code'];
    })

    this.form = new FormGroup({
      password: new FormControl('', Validators.required),
      passwordConfirm: new FormControl('', Validators.required)
    });
  }

  resetPassword() {
    this.errors = [];

    const resetPassword: ResetPassword = { 
      password: this.form.get('password').value, 
      passwordConfirm: this.form.get('passwordConfirm').value,
      userId: this.userId,
      code: this.code
    };

    this.accountService.resetPassword(resetPassword).subscribe(() => {
      this.toastrService.success('Your password has been successfully reset.', '', {
        timeOut: 5000
      });
      this.router.navigate(['login']);
    }, (error) => {
      this.errors = error.error;
    });
  }

  getPasswordConfirmErrorMessage() {
    if (this.form.get('passwordConfirm').hasError('required')) {
      return 'Password confirmation is required';
    }
    return 'Passwords do not match'
  }
}
