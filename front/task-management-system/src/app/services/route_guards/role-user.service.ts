import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AccountService } from '../account.service';

@Injectable({
  providedIn: 'root'
})
export class RoleUserService implements CanActivate {

  constructor(private accountService: AccountService, private router: Router) { }
  
  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRole = route.data.expectedRole;

    if (this.accountService.getUserRole() != expectedRole) {
      this.router.navigate(['not-found'], { skipLocationChange: true });
      return false;
    }

    return true;
  }
}
