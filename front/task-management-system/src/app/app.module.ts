import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './login/login.component';
import { ACCESS_TOKEN_KEY } from './services/account.service';
import { JwtModule } from '@auth0/angular-jwt'
import { FormsModule} from '@angular/forms'
import { HttpClientModule } from '@angular/common/http';
import { TasksComponent } from './tasks/tasks.component';
import { SignupComponent } from './signup/signup.component';
import { environment } from 'src/environments/environment';
import { API_URL } from './app-injection-token';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
 
export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    TasksComponent,
    SignupComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    JwtModule.forRoot({
      config: {
        tokenGetter
      }
    }),
    FormsModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [{
    provide: API_URL,
    useValue: environment.apiUrl
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
