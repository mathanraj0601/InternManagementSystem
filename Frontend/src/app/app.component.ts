import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { LogService } from './Services/log.service';
import { Log } from './Models/log.model';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'InternManagementSystem';
 
  

}
