import { Component } from '@angular/core';
import { Intern } from '../Models/intern.model';
import { LoginService } from '../Services/login.service';
import { HttpResponse } from '@angular/common/http';
import { InternComponent } from '../intern/intern.component';
import { InternDTO } from '../Models/InternDTO.model';
import { user } from '../Models/user.model';
import { UserDTO } from '../Models/userDTO.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  intern:InternDTO;
  constructor(private registerService:LoginService,private router:Router)
  {
    this.intern= new InternDTO();
    this.intern.user = new user();
    this.intern.user.role = 'intern';
    this.intern.user.status=false;
    this.getAge(new Date())
    
  }
  IsValidAge():boolean
  {
    return this.getAge(new Date(this.intern.dateOfBirth))<18
  }
  getAge(d1:Date)
  {
    var d2= new Date()
    var age = d2.getFullYear() - d1.getFullYear();
    if (d2.getMonth() <d1.getMonth() || 
        (d2.getMonth() === d1.getMonth() && d2.getDate() < d1.getDate())) {
      age--;
    }
    console.log(age);
    return age;
  }

  Register()
  {
    this.intern.age = this.getAge(new Date(this.intern.dateOfBirth))
    console.log(this.intern)
    this.registerService.register(this.intern).subscribe(
      (response:HttpResponse<any>)=>
      {
        if(response.status == 202)
        {
          var exUse = new UserDTO();
          exUse= response.body as UserDTO
          console.log(exUse)
          this.router.navigateByUrl('approval')
        }
      }

    )
  }


  login()
  {
    this.router.navigateByUrl('login');
  }
  Clear()
  {
    this.intern = new InternDTO();
  }
}
