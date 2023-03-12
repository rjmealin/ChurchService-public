import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { VisitorCardModel } from 'src/app/users/users.model';

@Component({
  selector: 'app-visitor-card-details',
  templateUrl: './visitor-card-details.component.html',
  styleUrls: ['./visitor-card-details.component.css']
})
export class VisitorCardDetailsComponent {

  constructor(    
    public dialogRef: MatDialogRef<VisitorCardDetailsComponent>,
    @Inject( MAT_DIALOG_DATA ) public data: VisitorCardModel
    ){}
}
