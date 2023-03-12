import { Component, OnDestroy, OnInit } from '@angular/core';
import { ChildrenOutletContexts } from '@angular/router';
import { Observable } from 'rxjs';
import { ChurchService } from './church/church.service';
import { AuthorizationService } from './security/authorization.service';
import { UsersService } from './users/users.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'ChurchService.ng';
  constructor(private contexts: ChildrenOutletContexts, private authService:AuthorizationService){}
  getRouteAnimationData() {
    return this.contexts.getContext('primary')?.route?.snapshot?.data?.['animation'];
  }


  public get isAdmin(): Observable<boolean> {
    return this.authService.isAdmin;
  };

  public get loggedIn(): Observable<boolean> {
    return this.authService.isLoggedIn;
  }

  public logoUrl: string;

  ngOnInit():void{

    this.loggedIn.subscribe( loggedIn => {
      if(loggedIn && this.logoUrl == null){
        this.authService.GetChurchLogo().subscribe( url => {
          if( url != null){
            this.logoUrl = url;
          }
        });
      }
    });
    
  }

  ngOnDestroy(): void {
    this.authService.Logout();
  }

  public Logout():void {
    this.authService.Logout();
  }
  


}
