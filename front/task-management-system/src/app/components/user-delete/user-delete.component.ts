import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-user-delete',
  templateUrl: './user-delete.component.html',
  styleUrls: ['./user-delete.component.scss']
})
export class UserDeleteComponent implements OnInit {

  form: FormGroup;
  errors: string[] = [];

  constructor(private accountService: AccountService,
    private dialog: MatDialog,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      password: new FormControl('', Validators.required)
    });
  }

  deleteUser() {
    this.accountService.deleteUser(this.form.get('password').value).subscribe(() => {
      this.dialog.closeAll();
      this.toastrService.success('Your account has been successfully deleted.', '', {
        timeOut: 5000
      });
      this.accountService.logout();      
    }, error => {
      this.errors = error.error;
    });
  }
}
