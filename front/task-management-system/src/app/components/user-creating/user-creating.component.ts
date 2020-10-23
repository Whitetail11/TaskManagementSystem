import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AppConstants } from 'src/app/models/appConstants';
import { CreateUser } from 'src/app/models/createUser';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-user-creating',
  templateUrl: './user-creating.component.html',
  styleUrls: ['./user-creating.component.scss']
})
export class UserCreatingComponent implements OnInit {

  constructor(private accountService: AccountService) { }

  errors: string[] = [];
  roles = [AppConstants.EXECUTOR_ROLE_NAME, AppConstants.CUSTOMER_ROLE_NAME, AppConstants.ADMIN_ROLE_NAME];
  form: FormGroup;
  @ViewChild("dialogCloseBtn") dialogCloseBtn: ElementRef;

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      role: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
      passwordConfirm: new FormControl('', [Validators.required])
    });
  }

  create()
  {
    this.errors = [];

    console.log(this.form);
    const createUser: CreateUser = 
    { 
      name: this.form.value.name,
      surname: this.form.value.surname,
      email: this.form.value.email, 
      password: this.form.value.password,
      passwordConfirm: this.form.value.passwordConfirm,
      role: this.form.value.role
    };

    this.accountService.createUser(createUser).subscribe(() => 
    { 
      this.dialogCloseBtn.nativeElement.click();
      alert("User was successfuly created.");
    }, err => {
      this.errors = err.error;
    });
  }
  
  getEmailErrors() {
    if (this.form.get('email').hasError('required'))
    {
      return 'Email is required';
    }
    return "Email is invalid";
  }
}
