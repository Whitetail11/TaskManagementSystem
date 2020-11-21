import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-user-delete',
  templateUrl: './user-delete.component.html',
  styleUrls: ['./user-delete.component.scss']
})
export class UserDeleteComponent implements OnInit {

  constructor(private accountService: AccountService,
    private dialog: MatDialog,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
  }

  deleteUser() {
    this.accountService.deleteUser().subscribe(() => {
      this.dialog.closeAll();
      this.toastrService.success('Your account has been successfully deleted.', '', {
        timeOut: 5000
      });
      this.accountService.logout();      
    });
  }
}
