import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DateModel } from '../users.model';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-member-availability',
  templateUrl: './member-availability.component.html',
  styleUrls: ['./member-availability.component.css']
})
export class MemberAvailabilityComponent implements OnInit{

  constructor(
    private readonly userService:UsersService,
    private readonly snackBar:MatSnackBar
  ){}

  public unaviableDates: Date[] = [];
  public date:Date;


  ngOnInit(): void {
    this.LoadDates();
  }


  /**
   * Loads the dates unavaible attached to a given user
   */
  public LoadDates(): void {

    this.unaviableDates = [];

    this.userService.GetUnavailableDates().subscribe( dates => {
      dates.forEach( d=> {
        let date = new Date(d);
        this.unaviableDates.push(date);
      })
    })
  }










  /**
   *  Adds a date unavaibel for a given user 
   */
  public AddUnavaibleDate(): void {

    let date = new DateModel();
    
    date.Date = this.date;
  
    this.userService.AddUnavaiableDate(date).subscribe( success => {
      if(success){
        this.LoadDates();        
      }
    })
  }









  /**
   *  removes the date from the database 
   */
  public RemoveDate(date:Date):void {

    let model = new DateModel();

    model.Date = date;
    
    this.userService.RemoveUnavaiableDate(model).subscribe( success => {
      if (success){
        this.LoadDates();
      }
    })

  }
}
