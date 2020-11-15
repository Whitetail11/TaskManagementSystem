import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ShowUser } from 'src/app/models/showUser';
import { AccountService } from 'src/app/services/account.service';
import { PasswordChangeComponent } from '../password-change/password-change.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  constructor(private route: ActivatedRoute, 
    private accountService: AccountService,
    private router: Router,
    private toastrService: ToastrService,
    private dialog: MatDialog) { }

  user: ShowUser;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.setUser(params['id']);
    });
  }

  setUser(id: string) {
    this.accountService.getUserById(id).subscribe((data) => {
      this.user = data;
    }, (error) => {
      if (error.status == 404) {
        this.router.navigate(['not-found']);
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

  openPasswordChangeModal() {
    this.dialog.open(PasswordChangeComponent, {
      width: '500px'
    });
  }
}
