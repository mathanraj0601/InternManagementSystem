import { user } from "./user.model";

export class Intern{
    constructor(
        public internId:number = 0,
        public name:string ="",
        public age:number=10,
        public gender:string ="",
        public phone:string="",
        public email:string="",
        public dateOfBirth:Date= new Date(),
        public user:user = null
    )
    {

    }
}