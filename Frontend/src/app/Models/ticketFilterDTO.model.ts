export class TicketFilterDTO
{
    constructor(
        public userID: number=0,
        public ticketID:number=0,
        public date:Date= new Date()
    )
    {

    }
}