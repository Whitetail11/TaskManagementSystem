import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { TasksComponent } from './components/tasks/tasks.component';
import { AuthenticatedUserService as AuthenticatedUser } from './services/route_guards/authenticated-user.service'
import { UnauthenticatedUserService as UnauthenticatedUser } from './services/route_guards/unauthenticated-user.service'
import { RoleUserService as RoleUser } from './services/route_guards/role-user.service'
import { UsersComponent } from './components/users/users.component';
import { TaskComponent } from './components/task/task.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { UserComponent } from './components/user/user.component';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { AppConstants } from './models/appConstants';

const appRoutes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full'},
  { path: 'login', component: LoginComponent, canActivate: [UnauthenticatedUser] },
  { path: 'signup', component: SignupComponent, canActivate: [UnauthenticatedUser]  },
  { path: 'tasks', component: TasksComponent, canActivate: [AuthenticatedUser] },
  { path: 'tasks/:id', component: TaskComponent, canActivate: [AuthenticatedUser] },
  { path: 'profile/:id', component: UserComponent, canActivate: [AuthenticatedUser] },
  { 
    path: 'users', 
    component: UsersComponent, 
    canActivate: [AuthenticatedUser, RoleUser], 
    data: {
      expectedRole: AppConstants.ADMIN_ROLE_NAME
    }
  },
  { path: 'confirm-email', component: ConfirmEmailComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
