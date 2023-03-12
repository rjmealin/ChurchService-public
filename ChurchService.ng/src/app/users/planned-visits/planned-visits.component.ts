import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CalendarView, CalendarEvent } from 'angular-calendar';
import { DialogService } from 'src/app/dialog/dialog.service';
import { PlannedVisitModel } from '../users.model';
import { UsersService } from '../users.service';
import { EventColor } from 'calendar-utils';
import { addMonths } from 'date-fns';

const colors: Record<string, EventColor> = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3',
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF',
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA',
  },
};


@Component({
  selector: 'app-planned-visits',
  templateUrl: './planned-visits.component.html',
  styleUrls: ['./planned-visits.component.css']
})
export class PlannedVisitsComponent implements OnInit {
  constructor(
    private readonly dialogService: DialogService,
    private readonly usersService: UsersService,
    private readonly snackBar: MatSnackBar
  ){}

  
  public plannedVisits: PlannedVisitModel[] = [];

  activeDayIsOpen: boolean = false;

  viewDate: Date = new Date();
  
  view: CalendarView = CalendarView.Month;

  events: CalendarEvent[] = [];



  ngOnInit(){
    this.LoadVisits();
  }









  
  /**
   * Calls the api to get all the planned visits
   */
  public LoadVisits():void {
    this.events = [];
    this.usersService.GetPlannedVisits().subscribe( visits => {

      visits.forEach( visit => {

        this.events = [
          ...this.events, {
            id:visit.PlannedVisitId,
            start: new Date(visit.VisitDate),
            end: new Date(visit.VisitDate),
            title: visit.VisitorFirstName + ' ' + visit.VisitorLastName,
            color: visit.AssignedUserId == null ? colors['red'] : colors['blue']
          },
        ]
        });
    });
  }










  /**
   *  Shows a dialog to assign a member 
   */
  eventClicked({ event }: { event: CalendarEvent }): void {
    this.dialogService.ShowAssignMemberDialog(event).subscribe( success => {
      if(success){
        this.snackBar.open('Visit Assigned!', 'Dismiss', {duration:3000});
        this.LoadVisits();
      }
    })
  }










  /**
   * Moves the calendar view to the next month
   */
  public NextMonth(): void {
    this.viewDate = addMonths(this.viewDate, 1);
  }









  /**
   * Moves back the
   */
  public PrevMonth():void {
    this.viewDate = addMonths(this.viewDate, -1);
  }

}
