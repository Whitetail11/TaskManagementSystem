import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { TasksComponent } from './components/tasks/tasks.component';
import { AuthenticatedUserService as AuthenticatedUser } from './services/authenticated-user.service'
import { UnauthenticatedUserService as UnauthenticatedUser } from './services/unauthenticated-user.service'
import { ProfileComponent } from './components/profile/profile.component';
import { UsersComponent } from './components/users/users.component';
import { TaskComponent } from './components/task/task.component';

const appRoutes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full'},
  { path: 'login', component: LoginComponent, canActivate: [UnauthenticatedUser] },
  { path: 'signup', component: SignupComponent, canActivate: [UnauthenticatedUser]  },
  { path: 'tasks', component: TasksComponent, canActivate: [AuthenticatedUser] },
  { path: 'tasks/:id', component: TaskComponent, canActivate: [AuthenticatedUser] },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthenticatedUser] },
  { path: 'users', component: UsersComponent, canActivate: [AuthenticatedUser] }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
