import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Log } from '../Models/log.model';
import { logIntern } from '../Models/logIntern.model';
import { LogFilterDTO } from '../Models/logFilterDTO.model';

@Injectable({
  providedIn: 'root'
})
export class LogService {
  token:string;
  constructor(private http:HttpClient) {
    // this.token='eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNjg2NzU1MTY4LCJleHAiOjE2ODY4NDE1NjgsImlhdCI6MTY4Njc1NTE2OH0.SzTudHO0KFpJ7FPGJtd38tNAIkb3iCd5ZkOB1lo5CHU'
   }
   AddLog(log:Log)
   {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
     return this.http.post('http://localhost:5252/api/Log/AddLog',log,{headers,observe:'response'});
   }

   update(log:Log)
   {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
    return this.http.put('http://localhost:5252/api/Log/EditLog',log,{headers,observe:'response'});
   }
   getAllLogsforUser(logFilter:logIntern)
   {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
    return this.http.post('http://localhost:5252/api/Log/GetLogBasedOnUserandDate',logFilter,{headers,observe:'response'})
   }
   getAlllogs()
   {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
     return this.http.get('http://localhost:5252/api/Log/GetAllLog',{headers,observe:'response'});
   }

   getAlllogsbasedOnDateAndUser(logFilter:LogFilterDTO)
   {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
     return this.http.post('http://localhost:5252/api/Log/GetAllLogsBasedonUserAndDate',logFilter,{headers,observe:'response'});
   }
}
