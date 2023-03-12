import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginModel } from '../account.model';
import { AuthorizationService } from 'src/app/security/authorization.service';
import { AccountService } from '../account.service';
import { finalize } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  constructor( 
    private readonly accountService: AccountService, 
    private readonly router: Router, 
    private readonly authService: AuthorizationService, 
    ) { }
    
  public serverError: string = '';
  public rememberMe: boolean = false;
  public failedLogin: boolean = false;
  public emailVerified: boolean = false;
  public model: LoginModel;

  ngOnInit(): void {
      this.model = new LoginModel();

      this.authService.isLoggedIn.subscribe( loggedIn => {
        if(loggedIn){
          void this.router.navigate( [ 'member/dashboard' ] );
        }
      })
  }










      /**
     * attempts a login with the login model
     */
      public UserLogin(): void {

        this.failedLogin = false;
        this.serverError = '';


        this.authService.Login( this.model.Email, this.model.Password, this.rememberMe ).pipe( finalize( () => {
          this.authService.rememberMe = this.rememberMe;
      } ) ).subscribe( {
          next: success => {
            if(success){
              void this.router.navigate(['member/dashboard'])
            }
          },
          error: () => {
              this.failedLogin = true;
          },
      } );


    }

}
