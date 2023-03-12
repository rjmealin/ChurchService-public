import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DialogService } from 'src/app/dialog/dialog.service';
import { MemberDetailsModel } from '../church.model';
import { ChurchService } from '../church.service';

@Component({
  selector: 'app-members-edit',
  templateUrl: './members-edit.component.html',
  styleUrls: ['./members-edit.component.css']
})
export class MembersEditComponent implements OnInit{

  public members: MemberDetailsModel[] = [];

  constructor(
    private readonly churchService: ChurchService,
    private readonly snackBar: MatSnackBar,
    private readonly dialogService: DialogService){}



  ngOnInit(): void {
    this.LoadMembers();
  }









  /**
   * Gets a list of all the members attached to a given church
   */
  public LoadMembers(): void {
    this.churchService.GetChurchMembers().subscribe(members => {
      this.members = members;
    });
  }









  /**
   * Opens a dialog for the user to add a member
   */
  public AddMember(): void {
    this.dialogService.ShowAddMemberDialog().subscribe( model => {
      if (model != null){
        this.churchService.AddChurchMember(model).subscribe( success => {
          if (success){
            this.LoadMembers();
            this.snackBar.open('Member Added!', 'Dismiss', {duration: 3000})
          }
        })
      }
    })
  }










  /**
   *  Shows a dialog to confirm removing a member, if confirmed the member is removed 
   */
  public RemoveMember(userId:string, memberName:string): void {
    this.dialogService.ShowConfirmDialog(`Are you sure you would like to remove ${memberName}`,'Remove Member').subscribe( remove => {
      if (remove === true){
        this.churchService.RemoveChurchMember(userId).subscribe( success => {
          if (success){
            this.LoadMembers();
            this.snackBar.open('Member Removed', 'Dismiss', {duration:3000})
          }
        })
      }
    })
  }


}
