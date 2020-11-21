import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { JwtModule } from '@auth0/angular-jwt'
import { FormsModule} from '@angular/forms'
import { HttpClientModule } from '@angular/common/http';
import { MatMenuModule } from '@angular/material/menu';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { ToastrModule } from 'ngx-toastr';
import { NgxFileDropModule } from 'ngx-file-drop';


import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { TasksComponent } from './components/tasks/tasks.component';
import { SignupComponent } from './components/signup/signup.component';
import { environment } from 'src/environments/environment';
import { UserCreatingComponent } from './components/user-creating/user-creating.component';
import { TaskCreateComponent } from './components/task-create/task-create.component';
import { UsersComponent } from './components/users/users.component';
import { LoginComponent } from './components/login/login.component';
import { TaskEditingComponent } from './components/task-editing/task-editing.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { TaskComponent } from './components/task/task.component';
import { CommentCreateComponent } from './components/comment-create/comment-create.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { UserComponent } from './components/user/user.component';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';

import { DialogElement } from './components/task-create/task-create.component';

import { ACCESS_TOKEN_KEY } from './services/account.service';
import { API_URL } from './app-injection-token';
import { TaskMenuComponent } from './components/task-menu/task-menu.component';
import { StatusMenuComponent } from './components/status-menu/status-menu.component';

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    TasksComponent,
    SignupComponent,
    UserCreatingComponent,
    TaskCreateComponent,
    DialogElement,
    UsersComponent,
    TaskEditingComponent,
    PaginationComponent,
    TaskComponent,
    CommentCreateComponent,
    NotFoundComponent,
    UserComponent,
    ConfirmEmailComponent,
    ForgotPasswordComponent,
    TaskMenuComponent,
    StatusMenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: environment.tokenAllowedDomains
      }
    }),
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    NgbModule,
    MatButtonModule,
    MatDialogModule,
    MatInputModule,
    MatStepperModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatSelectModule,
    MatMenuModule,
    NgxFileDropModule,
    ToastrModule.forRoot()
  ],
  providers: [{
    provide: API_URL,
    useValue: environment.apiUrl
  },
  MatDatepickerModule,
  MatNativeDateModule 
],
  bootstrap: [AppComponent]
})
export class AppModule { }
