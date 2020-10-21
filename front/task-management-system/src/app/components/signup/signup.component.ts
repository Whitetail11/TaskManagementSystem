import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterModel } from 'src/app/models/registerModel';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  constructor(private accountService: AccountService, private router: Router) { }

  @ViewChild("form", { static: false }) form: NgForm;
  errors: string[] = [];

  ngOnInit(): void {
  }

  signup() {
    this.errors = [];

    const registerModel: RegisterModel = 
    { 
      name: this.form.value.name,
      surname: this.form.value.surname,
      email: this.form.value.email, 
      password: this.form.value.password,
      passwordConfirm: this.form.value.passwordConfirm
    };

    this.accountService.register(registerModel).subscribe(() => 
    { 
      alert("You successfully signed up.");
      this.router.navigate(['tasks']);
    }, err => {
      this.errors = err.error;
      console.log(this.errors);
    });
  }
}