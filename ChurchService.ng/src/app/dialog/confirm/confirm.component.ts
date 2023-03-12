import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.css']
})
export class ConfirmComponent implements OnInit {

  public title: string;
  public message: string;


  constructor(
    public dialogRef: MatDialogRef<ConfirmComponent>,
    @Inject( MAT_DIALOG_DATA ) public data: any ) { }

    ngOnInit(): void {  
      this.title = this.data.Title;
      this.message = this.data.Message;
      
    }




    /**
     * Confirms the action
     */
    public confirm(): void {
      this.dialogRef.close(true);
    }








    
    /**
     * Ignores the actiuon
     */
    public cancel(): void {
      this.dialogRef.close(false);
    }
}
