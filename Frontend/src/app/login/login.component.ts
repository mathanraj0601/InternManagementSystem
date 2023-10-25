import { Component, EventEmitter, Output } from '@angular/core';
import { LoginService } from '../Services/login.service';
import { UserDTO } from '../Models/userDTO.model';
import { HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { LogService } from '../Services/log.service';
import { Log } from '../Models/log.model';
import { error } from '../Models/error.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  user:UserDTO;
  message:string="";
  constructor(private loginService:LoginService,private router:Router,private logService:LogService)
  {
    this.user = new UserDTO();
  }

  register()
  {
    this.router.navigateByUrl('register');
  }
  Login()
  {
    this.loginService.login(this.user).subscribe(
      (response:HttpResponse<any>)=>
      {
        if(response.status == 200)
        {
          sessionStorage.setItem('Id',response.body.userID)
          sessionStorage.setItem('role',response.body.role)
          sessionStorage.setItem("token",response.body.token)
          this.router.navigateByUrl('main')
          this.Clear();
          this.addlog();
        }
      },
      error=>
      {
       if(error.status == 400)
       {
        this.message =  "invalid username and password"
        if(error.body != null)
        {
          var e = error.body as error
          this.message = e.errorMessage
        }
       }
      }
    )
  }

  Clear()
  {
    this.user = new UserDTO();
  }

  addlog()
  {
    if(sessionStorage.getItem('role').toLowerCase() == 'intern')
      {
        var log = new Log();
        log.date = new Date();
        log.logInTime = new Date();
        log.userID = Number(sessionStorage.getItem('Id'))
        this.logService.AddLog(log).subscribe(
          (response:HttpResponse<any>)=>
          {
            if(response.status == 201)
            {
              var ex =  response.body as Log;
              sessionStorage.setItem('logId',ex.logId.toString())
              console.log(sessionStorage.getItem('logId'))
            }
          }
        );
        
      }
  }
}
