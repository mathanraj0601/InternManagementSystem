import { Component } from '@angular/core';
import { Ticket } from '../Models/ticket.model';
import { Solution } from '../Models/solution.model';
import { user } from '../Models/user.model';
import { TicketService } from '../Services/ticket.service';
import { HttpResponse } from '@angular/common/http';
import { TicketFilterDTO } from '../Models/ticketFilterDTO.model';
import { InternFilterDTO } from '../Models/InternFilterDTO.Model';
import { LoginService } from '../Services/login.service';
import { UserDTO } from '../Models/userDTO.model';

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent {
  ticket:Ticket;
  currentUser:UserDTO;
  public tickets:Ticket[];
  btnStatus: boolean;
  combineFilter:number;
  soluitonInput:string[];
  ticketFilterDTO: TicketFilterDTO;
  optionNumber:string;
  constructor(private ticketService:TicketService ,private loginService:LoginService)
  {
    this.ticketFilterDTO = new TicketFilterDTO();
    this.tickets=[];
    this.currentUser = new UserDTO() ;
    this.currentUser.userID = Number(sessionStorage.getItem('Id'));
    this.currentUser.role = sessionStorage.getItem('role');
  
    if(this.currentUser.role == 'admin')
    {
      this.optionNumber="1";
      this.soluitonInput = [];
      this.selectFilter(this.optionNumber);
      this.combineFilter = 0;
    }
    else{
      this.ticket = new Ticket();
      this.ticket.userID = this.currentUser.userID;
      this.ticketFilterDTO.date= null;
      this.ticketFilterDTO.userID = this.currentUser.userID;
      console.log(this.ticket);
      this.GetAllTicketAndSolutionBasedOnDateandUser();
    }
  
  }

  //admin
  selectFilter(num:string)
  {
    this.optionNumber = num;
    console.log(this.combineFilter);
    console.log(this.optionNumber);
    if(this.combineFilter ==1)
    {
      this.filter();
    }
    else{
      console.log(num);
      switch(num)
      {
        case "1": this.GetAllTicketAndSolution(); break;
        case "2": this.GetAllUnasweredTicket(); break;
      }
    }
    
  }

  //admin and intern
  GetAllTicketAndSolutionBasedOnDateandUser()
  {
    this.ticketService.GetAllTicketAndSolutionBasedOnDateandUser(this.ticketFilterDTO).subscribe(
      (reponse:HttpResponse<any>)=>
      {
        if(reponse.status == 200)
        {
          this.tickets = reponse.body as Ticket[];
          console.log(this.tickets)

        }
      },
      error=>
      {
        this.tickets=[];
      }
    )
  }

  //admin
  GetAllUnasweredTicketBasedODateandUser()
  {
    this.ticketService.GetAllUnAnsweredTicketBasedOnDateandUser(this.ticketFilterDTO).subscribe(
      (reponse:HttpResponse<any>)=>
      {
        if(reponse.status == 200)
        {
          this.tickets = reponse.body as Ticket[];
          console.log(this.tickets)

        }
      },
      error=>
      {
        this.tickets=[];
      }
    )
  }

  //admin
  GetAllUnasweredTicket()
  {
    this.ticketService.GetAllUnAnsweredTicket().subscribe(
      (reponse:HttpResponse<any>)=>
      {
        if(reponse.status == 200)
        {
          this.tickets = reponse.body as Ticket[];
          console.log(this.tickets)

        }
      },
      error=>
      {
        this.tickets=[];
      }
    )
  }

  //admin
  GetAllTicketAndSolution()
  { 
    this.ticketService.GetAllTicketAndSolution().subscribe(
      (reponse:HttpResponse<any>)=>
      {
        if(reponse.status == 200)
        {
          this.tickets = reponse.body as Ticket[];
          console.log(this.tickets)

        }
      },
      error=>
      {
        this.tickets=[];
      }
    )
  }

  //intern
  reset()
  {
    this.ticket.issueDetails = ""
    this.ticket.issueTitle=""
  }

  //intern
  raise()
  {
    this.ticket.userID = this.currentUser.userID;
    this.ticket.issuedDate = new Date();
    this.ticketService.RaiseTicket(this.ticket).subscribe(
      (reponse:HttpResponse<any>)=>
      {
        if(reponse.status == 200)
          {
            console.log(reponse.body);
            this.filter();
          }
      }
      )
      this.reset();
  }

  //admin
  clearTextArea(i:number)
  {
    this.soluitonInput[i]= '';
  }

  //admin
  answerTextArea(tick:Ticket,i:number)
  {
    var solution =  new Solution();
    solution.solutionDate = new Date();
    solution.ticketID = tick.ticketID;
    solution.solutionProvided = this.soluitonInput[i];
    solution.userID = this.currentUser.userID;
    this.ticketService.SolveTicket(solution).subscribe(
      (reponse:HttpResponse<any>)=>
      {
        if(reponse.status == 200)
          {
            console.log(reponse.body);
            console.log(this.optionNumber);
            this.selectFilter(this.optionNumber);
            this.clearTextArea(i);
          }
      }
     
    )
  }

  //intern and admin
  filter()
  {
    if(this.currentUser.role == 'intern')
    {
      this.GetAllTicketAndSolutionBasedOnDateandUser();
    }
    else{
      this.combineFilter=1;
      switch(this.optionNumber)
      {
        case "1":this.GetAllTicketAndSolutionBasedOnDateandUser(); 
                console.log(this.ticketFilterDTO);
                break;
        case "2":this.GetAllUnasweredTicketBasedODateandUser();
              console.log(this.ticketFilterDTO);
      }
    }
   
    this.btnStatus = true;
  }

  clear()
  {
    if(this.currentUser.role == 'intern')
    {
      this.ticketFilterDTO.date = null;
      this.GetAllTicketAndSolutionBasedOnDateandUser();
    }
    else
    {
      this.ticketFilterDTO = new TicketFilterDTO();
      this.combineFilter=0;
      this.selectFilter(this.optionNumber);

    }
    this.btnStatus = false;
   
  }

  
}
