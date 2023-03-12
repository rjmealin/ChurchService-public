import { Component, OnInit } from '@angular/core';
import { Router, TitleStrategy } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorizationService } from 'src/app/security/authorization.service';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit{


  constructor(
    private readonly router:Router,
    private readonly userService: UsersService,
    private readonly authService:AuthorizationService
  ){}


  public unassignedVisitCount: number;

  public get isAdmin(): Observable<boolean>{
    return this.authService.isAdmin;
  }

  ngOnInit(): void {
    this.userService.GetUnassignedVisits().subscribe( count => {
      this.unassignedVisitCount = count;
    })  
  }










  /**
   * Takes the user to a route
   */
  public RouteToComponent(route:string):void {

    this.router.navigateByUrl(route);

  }

}
