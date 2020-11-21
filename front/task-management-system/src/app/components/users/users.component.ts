import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ShowListUser } from 'src/app/models/showListUser';
import { AccountService } from 'src/app/services/account.service';
import { UserCreatingComponent } from '../user-creating/user-creating.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  pageUsers: ShowListUser[] = [];
  page = { size: 20, number: 1 };
  userCount: number = 0;

  constructor(private accountService: AccountService,
     private dialog: MatDialog) { }

  ngOnInit(): void {
    this.setUserCount();
    this.setUsers();
  }

  setUsers() {
    this.accountService.getForPage(this.page).subscribe((data: ShowListUser[]) => {
      this.pageUsers = data;
    });
  }

  setUserCount() {
    this.accountService.getUserCount().subscribe((data: number) => {
      this.userCount = data;
    });
  }

  onPageChange(pageNumber) {
    this.page.number = pageNumber;
    this.setUsers()
  }

  onPageSizeChange(pageSize) {
    this.page.number = 1;
    this.page.size = pageSize;
    this.setUsers();
  }

  createUser() {
    const dialogRef = this.dialog.open(UserCreatingComponent, {
      width: "500px"
    });
    dialogRef.componentInstance.userCreate.subscribe(() => {
      this.setUserCount();
      this.setUsers();
    });
  }

  public get isAdministrator(): boolean {
    return this.accountService.isAdministrator();
  }
}
