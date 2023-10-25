export class UserDTO
{
    constructor(
        public userID:number=0,
        public password:string ="",
        public newPassword:string="",
        public role:string="",
        public token:string=""
    )
    {

    }
}