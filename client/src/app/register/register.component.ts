import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() usersFromHomeComponent: any;
  @Output() cancelRegister = new EventEmitter();
  model : any = {};
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    console.log(this.model);
  }
 
  cancel(){
    this.cancelRegister.emit(false);
  }
  register(){
    this.accountService.register(this.model).subscribe(response => {
      console.log(response);
      this.cancel()
    },err => console.log(err));
  }

}
