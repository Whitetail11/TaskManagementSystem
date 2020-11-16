import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { UpdateUser } from 'src/app/models/updateUser';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.scss']
})
export class UserUpdateComponent implements OnInit {

  form: FormGroup;
  errors: string[] = [];

  constructor(private accountService: AccountService,
    private toastrService: ToastrService,
    private dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) private dialogData) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl(this.dialogData.user.name, Validators.required),
      surname: new FormControl(this.dialogData.user.surname, Validators.required),
      email: new FormControl(this.dialogData.user.email, [Validators.required, Validators.email])
    });
  }

  updateUser() {
    const updateUser: UpdateUser = this.form.value;
    this.accountService.updateUser(updateUser).subscribe(() => {
      this.dialog.closeAll();
      this.toastrService.success('Your profile has been successfully updated.', '', {
        timeOut: 5000
      });
    }, error => {
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
