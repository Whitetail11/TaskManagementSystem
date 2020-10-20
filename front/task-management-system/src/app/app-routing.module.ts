import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { TasksComponent } from './tasks/tasks.component';
import { AuthenticatedUserService as AuthenticatedUser } from './services/authenticated-user.service'
import { UnauthenticatedUserService as UnauthenticatedUser } from './services/unauthenticated-user.service'
import { ProfileComponent } from './profile/profile.component';
import { UserCreatingComponent } from './user-creating/user-creating.component';
import { UsersComponent } from './users/users.component';

const appRoutes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full'},
  { path: 'login', component: LoginComponent, canActivate: [UnauthenticatedUser] },
  { path: 'signup', component: SignupComponent, canActivate: [UnauthenticatedUser]  },
  { path: 'tasks', component: TasksComponent, canActivate: [AuthenticatedUser] },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthenticatedUser] },
  { path: 'users', component: UsersComponent, canActivate: [AuthenticatedUser] }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
