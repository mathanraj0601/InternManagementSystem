import { Component } from '@angular/core';
import { LogService } from '../Services/log.service';
import { LoginService } from '../Services/login.service';
import { HttpResponse } from '@angular/common/http';
import { Log } from '../Models/log.model';
import { LogFilterDTO } from '../Models/logFilterDTO.model';
import { UserDTO } from '../Models/userDTO.model';
import { logIntern } from '../Models/logIntern.model';

@Component({
  selector: 'app-log',
  templateUrl: './log.component.html',
  styleUrls: ['./log.component.css']
})
export class LogComponent {
  logs:Log[];
  btnStatus:boolean;
  logFilter:LogFilterDTO;
  curUser:UserDTO;
  logIntern:logIntern = new logIntern();
  constructor(private logService:LogService,private loginService:LoginService)
  {
     
    this.curUser =  new UserDTO();
    this.curUser.userID = Number(sessionStorage.getItem('Id'));
    this.curUser.role = sessionStorage.getItem('role');
    this.logFilter = new LogFilterDTO();
    this.logs = [];
    if(sessionStorage.getItem('role') == 'intern')
    {
      this.logFilter.date = null;
      this.logFilter.userID = Number(sessionStorage.getItem('Id'));
      this.getAllLogFOrUser();
      
    }
    else{
      this.getAllLog();

    }
    this.btnStatus = false;

  }


  //both admin and intern
  filter()
  {
    if(this.curUser.role.toLowerCase() == 'intern')
    {
      this.logFilter.userID = Number(sessionStorage.getItem('Id'));

    }
    this.getAllLogBasedonUserAndDate(); 
    this.btnStatus = true;
  }

  //both intern and admin
  clear()
  {
    if(sessionStorage.getItem('role')  == 'intern')
    {
      this.getAllLogFOrUser()
    }
    else
    {
      this.getAllLog();
    }
    this.btnStatus = false;
    this.logFilter = new LogFilterDTO();
  }


  getAllLogBasedonUserAndDate()
  {
    console.log(this.logFilter);

    this.logService.getAlllogsbasedOnDateAndUser(this.logFilter).subscribe(
      (response:HttpResponse<any>)=>
      {
        if(response.status == 200)
        {
        
          this.logs = response.body as Log[];
          console.log(this.logs);

        }
        if(response.status == 404)
        {
          this.logs=[];
        }
      },
      error=>
      {
        console.log(error.error);
        this.logs=[];
      }
    )
  }

  getAllLog()
  {
    this.logService.getAlllogs().subscribe(
      (reponse:HttpResponse<any>)=>
      {
        if(reponse.status == 200)
        {
          
          this.logs = reponse.body as Log[];
        }
        else
        {
          this.logs = [];
        }
      }
    
    )
  }

  getAllLogFOrUser()
  {
    this.logIntern.userId = Number(sessionStorage.getItem('Id'));
    this.logService.getAllLogsforUser(this.logIntern).subscribe(
      (response:HttpResponse<any>)=>
      {
        if(response.status == 200)
        {
          this.logs = response.body as Log[];
        }
      }
    )
  }

}
