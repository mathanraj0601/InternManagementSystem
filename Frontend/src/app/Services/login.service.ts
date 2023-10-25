import { Injectable } from '@angular/core';
import { user } from '../Models/user.model';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { InternFilterDTO } from '../Models/InternFilterDTO.Model';
import { Intern } from '../Models/intern.model';
import { UserDTO } from '../Models/userDTO.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  // private _currentUser: UserDTO;
  // public get currentUser(): UserDTO {
  //   return this._currentUser;
  // }
  // public set currentUser(value: UserDTO) {
  //   this._currentUser = value;
  // }
  constructor(private http:HttpClient) { 
    // this.currentUser = new UserDTO();
  }

  
  getAllIntern()
  {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
    return this.http.get('http://localhost:5032/api/User/GetAllIntern',{headers,observe:'response'},)
  }

  getAllInternBasedOnStatus(InternFilterDTO:InternFilterDTO)
  {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
    return this.http.post('http://localhost:5032/api/User/GetApprovedInternBasedOnStatus',InternFilterDTO,{headers,observe:'response'})
  }

  changeStatus(user:user)
  {const headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
    return this.http.post('http://localhost:5032/api/User/ChangeInternStatus',user,{headers,observe:'response'})
  }

  login(user:UserDTO)
  {
    return this.http.post('http://localhost:5032/api/User/Login',user,{observe:'response'})
  }

  register(intern:Intern)
  {
    
    return this.http.post('http://localhost:5032/api/User/Register',intern,{observe:'response'})
  }

  updatePassword(user:UserDTO)
  {
    console.log(user)
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
    return this.http.put('http://localhost:5032/api/User/UpdatePassword',user,{headers,observe:'response'})
  }
}
