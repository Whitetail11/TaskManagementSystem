import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { ACCESS_TOKEN_KEY } from './services/account.service';
import { JwtModule } from '@auth0/angular-jwt'
import { FormsModule} from '@angular/forms'
import { HttpClientModule } from '@angular/common/http';
import { TasksComponent } from './components/tasks/tasks.component';
import { SignupComponent } from './components/signup/signup.component';
import { environment } from 'src/environments/environment';
import { API_URL } from './app-injection-token';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProfileComponent } from './components/profile/profile.component';
import { UserCreatingComponent } from './components/user-creating/user-creating.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { TaskCreateComponent } from './components/task-create/task-create.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DialogElement } from './components/task-create/task-create.component';
import {MatInputModule} from '@angular/material/input';
import {MatStepperModule} from '@angular/material/stepper';
import {MatFormFieldModule} from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { UsersComponent } from './components/users/users.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import { LoginComponent } from './components/login/login.component';

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
    ProfileComponent,
    UserCreatingComponent,
    TaskCreateComponent,
    DialogElement,
    UsersComponent
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
    MatSelectModule
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
