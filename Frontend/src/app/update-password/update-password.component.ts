import { Component } from '@angular/core';
import { UserDTO } from '../Models/userDTO.model';
import { LoginService } from '../Services/login.service';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-update-password',
  templateUrl: './update-password.component.html',
  styleUrls: ['./update-password.component.css']
})
export class UpdatePasswordComponent {
  curUser:UserDTO;
  message:String = "Update your password"
  constructor(private loginService:LoginService)
  {
    this.curUser = new UserDTO();
    this.curUser.role = sessionStorage.getItem('role')
    this.curUser.userID =Number(sessionStorage.getItem('Id'));
    console.log(this.curUser)

  }

  clear()
  {
    this.curUser.newPassword = ""
  }

  updatePassword()
  {
    this.loginService.updatePassword(this.curUser).subscribe(
      (response:HttpResponse<any>)=>
      {
        if(response.status == 202)
        {
          this.message = "password updated sucessfully";
        }
      },
      error =>
      {
        this.message = "Unable to update retry";
      }
      
    )
  }
}
