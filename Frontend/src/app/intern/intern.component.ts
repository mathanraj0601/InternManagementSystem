import { Component } from '@angular/core';
import { Intern } from '../Models/intern.model';
import { LoginService } from '../Services/login.service';
import { HttpResponse } from '@angular/common/http';
import { InternFilterDTO } from '../Models/InternFilterDTO.Model';
import { user } from '../Models/user.model';

@Component({
  selector: 'app-intern',
  templateUrl: './intern.component.html',
  styleUrls: ['./intern.component.css']
})
export class InternComponent {
 interns:Intern[];
 intern:Intern[];
 internFilter:InternFilterDTO;
 optionNUmber:string;
 constructor(private internService:LoginService)
 {
  this.optionNUmber="1";
    this.internFilter = new InternFilterDTO();
    this.interns = [];
    this.getAllIntern();
 }

 selectFilter(num:string)
 {
  this.optionNUmber = num;
  switch(num)
  {
    case "1": this.getAllIntern(); break;
    case "2": this.unApproveIntern(); break;
    case "3": this.approvedIntern(); break;

  }
  
 }

 getAllIntern()
{
  this.internService.getAllIntern().subscribe(
    (response:HttpResponse<any>)=>
    {
      if(response.status==200)
      {
        this.interns = response.body as Intern[];

      }
    }
  )
}

approve(index:number)
{
  var User = new user(this.interns[index].internId,true,this.interns[index].user.role);
  this.internService.changeStatus(User).subscribe(
    (reponse:HttpResponse<any>)=>
    {
      if(reponse.status ==202)
      {
  
        this.interns[index].user.status = true;
        console.log(this.optionNUmber)
        this.selectFilter(this.optionNUmber);
      }
    }
  )
}


unApprove(index:number)
{
  var User = new user(this.interns[index].internId,false,this.interns[index].user.role);
  this.internService.changeStatus(User).subscribe(
    (reponse:HttpResponse<any>)=>
    {
      if(reponse.status ==202)
      {
        console.log(this.optionNUmber)
  
        this.interns[index].user.status = false;
        this.selectFilter(this.optionNUmber);
      
      }
    }
  )
}
approvedIntern()
{
  this.internFilter.status = true
   this.internService.getAllInternBasedOnStatus(this.internFilter).subscribe(
    (reponse:HttpResponse<any>)=>
    {
      if(reponse.status ==200)
      {

        this.interns = reponse.body as Intern[];
      }
    }
   )
}

unApproveIntern()
{
  this.internFilter.status = false
   this.internService.getAllInternBasedOnStatus(this.internFilter).subscribe(
    (reponse:HttpResponse<any>)=>
    {
      if(reponse.status ==200)
      {
        this.interns = reponse.body as Intern[];
      }
    }
   )
}
}
