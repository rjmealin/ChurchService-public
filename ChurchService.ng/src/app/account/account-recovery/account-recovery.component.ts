import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UsersService } from 'src/app/users/users.service';
import { AccountRecoverModel } from '../account.model';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-account-recovery',
  templateUrl: './account-recovery.component.html',
  styleUrls: ['./account-recovery.component.css']
})
export class AccountRecoveryComponent implements OnInit{

  constructor(
    private accountService:AccountService,
    private snackBar:MatSnackBar
  ){}

  public model: AccountRecoverModel = new AccountRecoverModel();
  public email: string

  ngOnInit():void {}








  
  /**
   * Sends a password reset request to the user
   */
  public RequestReset():void {
    this.accountService.SendPasswordReset(this.email).subscribe( success => {
      if(success){
        this.snackBar.open(`the password reset has been sent to ${this.email}`, 'Dismiss', {duration:4000});
      }
      
    })

  }
}
