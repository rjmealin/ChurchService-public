import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { UsersService } from 'src/app/users/users.service';

@Component({
  selector: 'app-verify-sms',
  templateUrl: './verify-sms.component.html',
  styleUrls: ['./verify-sms.component.css']
})
export class VerifySmsComponent implements OnInit {

  constructor(
    private readonly userService:UsersService,
    private readonly router:Router,
    private readonly snackBar:MatSnackBar
  ){}
  
  public code:number;

  public isLoading:boolean = false;

  ngOnInit(): void {
  }






  /**
   * Attempts to verify the sms with a given code
   */
  public AttemptVerify():void {
    this.isLoading = true;
    this.userService.VerifySms(this.code).pipe(finalize( () => this.isLoading = false)).subscribe( success => {
      if (success){
        this.snackBar.open('Phone verified!', 'Dismiss', {duration:3000});
        this.isLoading = false;
        void this.router.navigateByUrl('member/dashboard');
      }
    })
  }









  
  /**
   * Closes the dialog with no action
   */
  public Close():void {
    this.router.navigateByUrl('member/dashboard');
  }
}
