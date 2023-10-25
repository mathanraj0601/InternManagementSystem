import { Component, HostListener, inject } from '@angular/core';
import { Route, Router } from '@angular/router';
import { LogService } from '../Services/log.service';
import { Log } from '../Models/log.model';
import { HttpRequest, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {
  word:boolean = true;
  userRole:string;
  @HostListener('window:resize', ['$event'])
  onWindowResize(event: Event) {
    if(window.outerWidth < 700)
      this.word=  false
    else
      this.word = true;
  }

  @HostListener('window:unload')
  onUnload() {
    this.logout();
  }
  


  constructor(private router:Router, private logService:LogService)
  {

    this.userRole = sessionStorage.getItem('role');
    this.router.navigate(['main/logs']);
    if(sessionStorage.getItem('token') == null)
    {
        alert("User session time out");
        this.router.navigateByUrl('login');
    }
    
  }
  log()
  {
    this.router.navigate(['main/logs']);
  }
  ticket()
  {
    this.router.navigate(['main/ticket']);
  }
  intern()
  {
    this.router.navigate(['main/intern']);
  }

  update()
  {
    this.router.navigate(['main/update']);
  }

  logout()
  {
    if(sessionStorage.getItem('role').toLowerCase() == 'intern')
    {
      var log = new Log();
      log.logId = Number(sessionStorage.getItem('logId'));
      log.userID = Number(sessionStorage.getItem('Id'))
      console.log(sessionStorage.getItem('logId'))
      log.logOutTime = new Date();
      this.logService.update(log).subscribe(
        (response:HttpResponse<any>)=>
        {
          if(response.status == 202)
          { 
            this.router.navigateByUrl('login');
          }
        }
      )
    }
    else
    {
      this.router.navigateByUrl('login');
    } 
  }






}
