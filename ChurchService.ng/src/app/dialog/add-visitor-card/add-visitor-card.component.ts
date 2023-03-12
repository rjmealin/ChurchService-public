import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { VisitorCardModel } from 'src/app/users/users.model';

@Component({
  selector: 'app-add-visitor-card',
  templateUrl: './add-visitor-card.component.html',
  styleUrls: ['./add-visitor-card.component.css']
})
export class AddVisitorCardComponent {

  constructor(public dialogRef: MatDialogRef<VisitorCardModel>) { }


  public model:VisitorCardModel = new VisitorCardModel();


  /**
  * Confirms the data and closes this dialog.
  */
  public confirm(): void {
    this.dialogRef.close( this.model );
  }









  
  /**
   * Cancels this dialog.
   */
  public cancel(): void {
      this.dialogRef.close( null );
  }


}
