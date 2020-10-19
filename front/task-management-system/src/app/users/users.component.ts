import { Component, OnInit, ViewChild } from '@angular/core';
import { UserCreatingComponent } from '../user-creating/user-creating.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  createUser(userCreatingComponent: UserCreatingComponent) {
    userCreatingComponent.showModal();
  }
}
