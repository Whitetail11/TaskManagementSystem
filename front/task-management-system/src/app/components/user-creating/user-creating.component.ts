import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AppConstants } from 'src/app/models/appConstants';
import { CreateUser } from 'src/app/models/createUser';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-user-creating',
  templateUrl: './user-creating.component.html',
  styleUrls: ['./user-creating.component.scss']
})
export class UserCreatingComponent implements OnInit {

  constructor(private accountService: AccountService,
    private toastrService: ToastrService,
    private dialog: MatDialog) { }

  errors: string[] = [];
  roles = [AppConstants.EXECUTOR_ROLE_NAME, AppConstants.CUSTOMER_ROLE_NAME, AppConstants.ADMIN_ROLE_NAME];
  form: FormGroup;

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl('', [Validators.required]),
      surname: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      role: new FormControl('', [Validators.required])
    });
  }

  create()
  {
    this.errors = [];

    console.log(this.form);
    const createUser: CreateUser = this.form.value;

    this.accountService.createUser(createUser).subscribe(() => {
      this.dialog.closeAll();
      this.toastrService.success('User has been successfuly created.', '', {
        timeOut: 5000
      });
    }, error => {
      this.errors = error.error;
    });
  }
  
  getEmailErrorMessage() {
    if (this.form.get('email').hasError('required')) {
      return 'Email is required';
    }
    return 'Email is invalid';
  }
}
