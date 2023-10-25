import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { InternComponent } from './intern/intern.component';
import { LogComponent } from './log/log.component';
import { Ticket } from './Models/ticket.model';
import { MainComponent } from './main/main.component';
import { TicketComponent } from './ticket/ticket.component';
import { ApprovalComponent } from './approval/approval.component';
import { UpdatePasswordComponent } from './update-password/update-password.component';


const routes: Routes = [
  {path:'' , redirectTo:'login', pathMatch:"full"},
  {path:"register" , component:RegisterComponent},
  {path:"login" , component:LoginComponent},
  // {path:"intern" , component:InternComponent},
  // {path:"logs" , component:LogComponent},
  // {path:"ticket" , component:TicketComponent},
  {path:'approval',component:ApprovalComponent},
  {path:"main" , component:MainComponent,children:
  [
    {path:"logs" , component:LogComponent},
    {path:"ticket" , component:TicketComponent},
    {path:"intern" , component:InternComponent},
    {path:"update" , component:UpdatePasswordComponent}

  ]
},


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
