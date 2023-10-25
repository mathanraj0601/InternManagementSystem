import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { MainComponent } from './main/main.component';
import { InternComponent } from './intern/intern.component';
import { LogComponent } from './log/log.component';
import { TicketComponent } from './ticket/ticket.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TicketService } from './Services/ticket.service';
import { LogService } from './Services/log.service';
import { LoginService } from './Services/login.service';
import { ApprovalComponent } from './approval/approval.component';
import { UpdatePasswordComponent } from './update-password/update-password.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    MainComponent,
    InternComponent,
    LogComponent,
    TicketComponent,
    ApprovalComponent,
    UpdatePasswordComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [TicketService,LogService,LoginService],
  bootstrap: [AppComponent]
})
export class AppModule { }
