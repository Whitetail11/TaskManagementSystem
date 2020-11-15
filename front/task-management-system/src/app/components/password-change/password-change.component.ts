import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { ChangePassword } from 'src/app/models/changePassword';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-password-change',
  templateUrl: './password-change.component.html',
  styleUrls: ['./password-change.component.scss']
})
export class PasswordChangeComponent implements OnInit {

  form: FormGroup;
  errors: string[] = [];

  constructor(private accountService: AccountService,
    private toastrService: ToastrService,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      currentPassword: new FormControl('', Validators.required),
      newPassword: new FormControl('', Validators.required),
      passwordConfirm: new FormControl('', Validators.required)
    });
  }

  changePassword() {
    this.errors = [];
    
    const changePassword: ChangePassword = this.form.value;
    this.accountService.changePassword(changePassword).subscribe(() => {
      this.dialog.closeAll();
      this.toastrService.success('Password has been successfully changed.', '', {
        timeOut: 5000
      });
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
