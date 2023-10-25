export class LogFilterDTO
{
    constructor(
        public userID:number =0 ,
        public date:Date = new Date(),
        public logID:number =0,

    )
    {

    }
}