import { Solution } from "./solution.model";

export class Ticket {
    constructor(
        public ticketID:number= 0,
        public userID:number = 0,
        public issueTitle:string ="",
        public issueDetails:String="" ,
        public issuedDate: Date = new Date(),
        public solutions:Solution[] = []
    )
    {

    }
}