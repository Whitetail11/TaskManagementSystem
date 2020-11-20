import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ShowUser } from 'src/app/models/showUser';
import { UpdateUser } from 'src/app/models/updateUser';
import { AccountService } from 'src/app/services/account.service';
import { PasswordChangeComponent } from '../password-change/password-change.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  emailConfirmed: boolean = false;
  id: string;
  errors: string[] = [];
  form: FormGroup;

  constructor(private route: ActivatedRoute, 
    private accountService: AccountService,
    private router: Router,
    private toastrService: ToastrService,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email])
    });

    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.setUser(this.id);
    });
  }

  setUser(id: string) {
    this.accountService.getUserById(id).subscribe((data: ShowUser) => {
      this.emailConfirmed = data.emailConfirmed;
      this.form = new FormGroup({
        name: new FormControl(data.name, Validators.required),
        surname: new FormControl(data.surname, Validators.required),
        email: new FormControl(data.email, [Validators.required, Validators.email])
      });
    }, error => {
      if (error.status == 404) {
        this.router.navigate(['not-found'], { skipLocationChange: true });
      }
    });
  }

  sendEmailConfirmationLink(){
    this.accountService.sendEmailConfirmationLink().subscribe(() => {
      this.toastrService.success('Email confirmation link has been sent.', '', {
        timeOut: 5000
      });
    });
  }

  updateProfile() {
    const updateUser: UpdateUser = this.form.value;
    this.accountService.updateUser(this.id, updateUser).subscribe(() => {
      this.dialog.closeAll();
      this.toastrService.success('Your profile has been successfully updated.', '', {
        timeOut: 5000
      });
    }, error => {
      this.errors = error.error;
    });
  }
  
  openPasswordChangeDialog() {
    this.dialog.open(PasswordChangeComponent, {
      width: '500px'
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
