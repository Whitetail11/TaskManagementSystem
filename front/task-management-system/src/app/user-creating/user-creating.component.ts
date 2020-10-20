import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AppConstants } from '../models/appConstants';
import { CreateUserModel } from '../models/createUserModel';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-user-creating',
  templateUrl: './user-creating.component.html',
  styleUrls: ['./user-creating.component.scss']
})
export class UserCreatingComponent implements OnInit {

  constructor(private accountService: AccountService, private modalService: NgbModal) { }

  @ViewChild("content", { static: false }) content;
  errors: string[] = [];
  roles = [AppConstants.EXECUTOR_ROLE_NAME, AppConstants.CUSTOMER_ROLE_NAME, AppConstants.ADMIN_ROLE_NAME];

  ngOnInit(): void {
  }

  showModal()
  {
    this.modalService.open(this.content, { size: 'lg' });
  }

  create(form: NgForm)
  {
    this.errors = [];

    const createUserModel: CreateUserModel = 
    { 
      name: form.value.name,
      surname: form.value.surname,
      email: form.value.email, 
      password: form.value.password,
      passwordConfirm: form.value.passwordConfirm,
      role: form.value.role
    };

    this.accountService.createUser(createUserModel).subscribe(() => 
    { 
      this.modalService.dismissAll();
      alert("User was successfuly created.");
    }, err => {
      this.errors = err.error;
    });
  }
}
