import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AppConstants } from '../models/appConstants';
import { CreateUserModel } from '../models/createUserModel';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-user-creating',
  templateUrl: './user-creating.component.html',
  styleUrls: ['./user-creating.component.scss']
})
export class UserCreatingComponent implements OnInit {

  constructor(private accountService: AccountService, private router: Router) { }

  @ViewChild("form", { static: false }) form: NgForm;
  errors: string[] = [];
  roles = [AppConstants.EXECUTOR_ROLE_NAME, AppConstants.CUSTOMER_ROLE_NAME, AppConstants.ADMIN_ROLE_NAME];

  ngOnInit(): void {
  }

  create()
  {
    this.errors = [];

    const createUserModel: CreateUserModel = 
    { 
      name: this.form.value.name,
      surname: this.form.value.surname,
      email: this.form.value.email, 
      password: this.form.value.password,
      passwordConfirm: this.form.value.passwordConfirm,
      role: this.form.value.role
    };

    this.accountService.createUser(createUserModel).subscribe(() => 
    { 
      alert("User was successfuly created.");
      this.router.navigate(['/user-creating']);
    }, err => {
      this.errors = err.error;
    });
  }
}
