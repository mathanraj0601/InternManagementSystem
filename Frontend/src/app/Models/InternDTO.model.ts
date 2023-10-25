import { Intern } from "./intern.model";
import { user } from "./user.model";
export class InternDTO extends Intern
{
    constructor(
         internId:number = 0,
         name:string ="",
         age:number=10,
         gender:string ="male",
         phone:string="",
         email:string="",
         dateOfBirth:Date= new Date(),
         user:user=null,
         public passwordClear:string=""
    )
    {
        super(internId,name,age,gender,phone,email,dateOfBirth,user)
    }
}