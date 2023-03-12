import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { PasswordResetModel } from '../account.model';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit{

  constructor(
    private readonly accountService:AccountService,
    private readonly snackBar:MatSnackBar,
    private readonly route:ActivatedRoute,
    private readonly router:Router,
  ){}


  public model:PasswordResetModel = new PasswordResetModel();
  hide: boolean = true;
  hideRepeat: boolean = true;
  repeatPassword: string = '';

  ngOnInit(): void {
    this.route.params.subscribe( param => {
      if(param['id'] != null){
        this.model.PasswordResetId = param['id'];
      }
    })
  }



  /**
   * Attempts to reset a user password
   */
  AttemptReset():void {
    this.accountService.ResetUserPassword(this.model).subscribe( success => {
      if(success){
        this.snackBar.open('Password reset!', 'Dismiss', {duration:3000});
        void this.router.navigateByUrl('');
      }
    })
  }
}
