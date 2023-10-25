import { Injectable } from '@angular/core';
import { Ticket } from '../Models/ticket.model';
import { Solution } from '../Models/solution.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TicketFilterDTO } from '../Models/ticketFilterDTO.model';

@Injectable()
export class TicketService {
  constructor(private http:HttpClient) { 
    

  }

  GetAllTicketAndSolution()
  {const headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
    return this.http.get("http://localhost:5067/api/Ticket/GetAllTicketAndSolution",{headers,observe : 'response'});
    
  }

  GetAllUnAnsweredTicket()
  {const headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
    return this.http.get('http://localhost:5067/api/Ticket/GetAllUnAnsweredTicket',{headers,observe:'response'})
  }

  GetAllUnAnsweredTicketBasedOnDateandUser(ticketFilter:TicketFilterDTO)
  {const headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
    return this.http.post('http://localhost:5067/api/Ticket/GetAllUnAnsweredTicketBasedOnDateandUser',ticketFilter,{headers,observe:'response'})
  }

  GetAllTicketAndSolutionBasedOnDateandUser(ticketFilter:TicketFilterDTO)
  {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
    return this.http.post('http://localhost:5067/api/Ticket/GetAllTicketAndSolutionBasedOnDateandUser',ticketFilter,{headers,observe:'response'})
  }

  RaiseTicket(ticket:Ticket)
  {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
    return this.http.post('http://localhost:5067/api/Ticket/RaiseTicket',ticket,{headers,observe:'response'})
  }

  SolveTicket(solution:Solution)
  {const headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        });
    return this.http.post('http://localhost:5067/api/Ticket/AddSolution',solution,{headers,observe:'response'})
    
  }

}