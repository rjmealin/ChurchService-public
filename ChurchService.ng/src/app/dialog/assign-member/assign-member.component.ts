import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { ChurchService } from 'src/app/church/church.service';
import { AuthorizationService } from 'src/app/security/authorization.service';
import { DatabaseOptionModel, PlannedVisitModel } from 'src/app/users/users.model';

@Component({
  selector: 'app-assign-member',
  templateUrl: './assign-member.component.html',
  styleUrls: ['./assign-member.component.css']
})
export class AssignMemberComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<AssignMemberComponent>,
    @Inject( MAT_DIALOG_DATA ) public data: any,
    private readonly churchService:ChurchService,
    private readonly snackBar: MatSnackBar,
    private readonly authService:AuthorizationService
  ) { }

  public availableMembers:DatabaseOptionModel[] = [];

  public selectedMemberId:string;

  public visitId:string;

  public visit: PlannedVisitModel;

  public dateString: string;

  public get isAdmin(): Observable<boolean>{
    return this.authService.isAdmin;
  }


  ngOnInit(): void {

    let date = new Date(this.data.start);

    let year = this.data.start.getFullYear();
    let month = date.getMonth() + 1
    let day = date.getDate()

    this.dateString = `${year}-${month}-${day}`

    this.visitId = this.data.id;



    this.churchService.GetVisitDetails(this.visitId).subscribe( visit => {
      if (visit != null){
        this.visit = visit;
        if(visit.AssignedUserId == null){
          this.GetAllAvailableMembers();
        }
      }
    });
  }









  
  /**
   * Gets all the available members on a given date
   */
  public GetAllAvailableMembers():void {
    this.churchService.GetAllAvailableMembers(this.dateString).subscribe(members => {
      this.availableMembers = members
    })
  }






  /**
   * Assigns a member to a planned visit
   */
  public Assign(): void {
    this.churchService.AssignVisit(this.visitId, this.selectedMemberId).subscribe( success => {
      if(success){
        this.dialogRef.close(true);
      }
    })
  }











  /**
   * Cancels the member assignment process
   */
  public Cancel(): void{
    this.dialogRef.close(null);
  }




}
