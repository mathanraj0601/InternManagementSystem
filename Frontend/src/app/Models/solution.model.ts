import { Ticket } from "./ticket.model";

export class Solution{
    constructor(
        public solutionID:number = 0,
        public userID:number = 0,
        public ticketID:number = 0,
        public ticket:Ticket= new Ticket(),
        public solutionProvided:string  ="",
        public solutionDate:Date = new Date()

    )
    {

    }

}