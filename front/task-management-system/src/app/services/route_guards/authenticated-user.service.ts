import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AccountService } from '../account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticatedUserService implements CanActivate {

  constructor(private acccountService: AccountService, private router: Router) { }
  
  canActivate(): boolean {
    if (!this.acccountService.isAuthenticated())
    {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }
}
