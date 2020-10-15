import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {

  isExpanded = false;

  constructor(private accountService: AccountService) { }
  
  ngOnInit(): void {
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  public get isLoggedId(): boolean {
    return this.accountService.isAuthenticated();
  }

  public get isAdministrator(): boolean {
    return this.accountService.isAdministrator();
  }

  public get isCustomer(): boolean {
    return this.accountService.isCustomer();
  }

  public get isExecutor(): boolean {
    return this.accountService.isExecutor();
  }

  logout() {
    this.accountService.logout();
  }
}
