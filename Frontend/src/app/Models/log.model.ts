export class Log{
    constructor(
        public logId:number =0,
        public userID:number=0,
        public logInTime:Date =new Date(),
        public logOutTime:Date=new Date(),
        public date:Date=new Date()
    )
    {

    }
}