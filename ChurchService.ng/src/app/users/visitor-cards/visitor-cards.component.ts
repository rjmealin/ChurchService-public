import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DialogService } from 'src/app/dialog/dialog.service';
import { VisitorCardModel } from '../users.model';
import { UsersService } from '../users.service';
import { isBefore } from 'date-fns';
import * as _ from 'lodash';
import { Observable } from 'rxjs';
import { AuthorizationService } from 'src/app/security/authorization.service';

@Component({
  selector: 'app-visitor-cards',
  templateUrl: './visitor-cards.component.html',
  styleUrls: ['./visitor-cards.component.css']
})
export class VisitorCardsComponent implements OnInit {


  constructor(
    private readonly dialogService: DialogService,
    private readonly usersService: UsersService,
    private readonly snackBar: MatSnackBar,
    private readonly authService: AuthorizationService
  ){}

  public get isAdmin():Observable<boolean>{
    return this.authService.isAdmin;  
  }

  public visitorCards: VisitorCardModel[] = [];
  public filteredCards: VisitorCardModel[] = [];
  public dataSource = new MatTableDataSource(this.visitorCards);
  public displayedColumns: string[] = ['VisitorName', 'VisitorEmail', 'VisitorPhone', 'ReferalType', 'DateOfAttendance'];
  public referralType:number | null;
  public start:Date | null;
  public end:Date | null;
  public searchString:string | null;
  public ExistingChristian:boolean = true;
  public Returning:boolean = true;
  public NewToArea:boolean = true;
  public FirstTime:boolean = true;

  @ViewChild(MatSort) sort: MatSort;

  ngOnInit(): void {

    this.GetCards();
    
  }
  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }













  /**
   * Gets all the visitor cards from the users serbice.
   */
  public GetCards(): void {

    this.usersService.GetVisitorCards().subscribe(cards => {

      this.visitorCards = cards;

      this.filteredCards = _.cloneDeep(this.visitorCards);

      //this.dataSource = new MatTableDataSource(cards);

    });
  }









  /**
   * Opens a dialog to enter info about a visitor card and send it to the db
   */
  public AddVisitorCard(): void {

    this.dialogService.ShowVisitorCardDialog().subscribe( model => {

      if(model != null){

        this.usersService.SaveVisitorCard(model).subscribe( success => {

          if(success){

            this.snackBar.open('The visitor card has been saved.', 'Dismiss', {duration:3000});

            this.GetCards();

          }
        })
      }
    })
  }











  /**
   * Resets all filters
   */
  public ResetFilters():void {
    this.end = null;
    this.start = null;
    this.searchString = null;
    this.referralType = null;

    this.filteredCards = _.cloneDeep(this.visitorCards);
  }










  /**
   * Filters through all existing visitor cards
   */
  public FilterCards():void {

    let cards = _.cloneDeep(this.visitorCards);
    
    if(this.Returning){
      cards = cards.filter(c => c.IsReturningVisitor === this.Returning);
    }
    if(this.FirstTime){
      cards = cards.filter( c => c.IsFirstTimeGuest === this.FirstTime);
    }
    if(this.ExistingChristian){
      cards = cards.filter( c => c.IsExistingChristian === this.ExistingChristian);
    }
    if(this.NewToArea){
      cards = cards.filter( c => c.IsNewToArea === this.NewToArea);
    }

    
    if( this.referralType != null ) {
      cards = cards.filter( c => c.ReferalType === this.referralType);
    }
    
    if (this.searchString != null){
      let string = this.searchString
      cards = cards.filter( c => c.Comments?.includes(string) || c.ReasonForAttendance?.includes(string));
    }

    if( this.start != null){
      let start = new Date(this.start);
      
      for(let i = cards.length - 1; i >= 0; i--){
        if(cards[i].DateOfAttendance != null){

          let doa = new Date(cards[i].DateOfAttendance as Date);

          if(doa < start){
            cards.splice(i, 1);
          } else {
            continue;
          }
        } else {
          cards.splice(i, 1);
        }
      }
    }

    if (this.end != null){
      let end = new Date(this.end);
      for(let i = cards.length - 1; i >= 0; i--){
        if(cards[i].DateOfAttendance != null){

          let doa = new Date(cards[i].DateOfAttendance as Date);
          if(doa > end){
            cards.splice(i, 1);
          } else {
            continue;
          }
        } else {
          cards.splice(i, 1);
        }
      }
    }

    this.filteredCards = cards;

  }










  /**
   * Sends all the visitors emails
   */
  public SendVisitorsEmails():void {
    this.dialogService.ShowEmailBlastDialog().subscribe( model => {
      if (model != null){

        let emails:string[] = [];

        this.filteredCards.forEach( c => {
          if (c.VisitorEmail != null){
            emails.push(c.VisitorEmail);
          }
        })
        model.Emails = emails;
        this.usersService.SendVisitorsEmails(model).subscribe( success => {
          if(success){
            this.snackBar.open('Emails sent!', 'Dismiss', {duration: 3000});
          }
          
        })
      }
    })
  }










  /**
   *Removes a visitor card
   */
  public RemoveCard(cardId:string):void{
    this.dialogService.ShowConfirmDialog('Are you certain you want to delete this card?', 'Delete card').subscribe(confirmed => {
      if(confirmed){
        this.usersService.RemoveVisitorCard(cardId).subscribe( success => {
          if(success){
            this.GetCards();
          }
        });
      }
    });

  }










  /**
   *  Views all the details pertaining to a given visitors card 
   */
  public ViewCardDetails(model:VisitorCardModel):void {
    this.dialogService.ShowVisitorCardDetailsDialog(model).subscribe();
  }
}
