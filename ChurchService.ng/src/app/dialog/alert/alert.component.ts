import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertDialogComponent implements OnInit {
  public messages: string[];
  public title: string = '';
  public showError: boolean = false;
  public showWarning: boolean = false;

  constructor(
      public dialogRef: MatDialogRef<AlertDialogComponent>,
      @Inject( MAT_DIALOG_DATA ) public data: any ) { }

  ngOnInit(): void {
      this.messages = [];
      this.showError = this.data.showError === true;
      this.showWarning = this.data.showWarning === true;
      
      if ( this.data.message instanceof Array ) {
          this.data.message.forEach( ( msg: any ) => this.messages.push( msg ) );
      } else {
          this.messages.push( this.data.message != null ? this.data.message : 'Unknown error' );
      }

      this.title = this.data.title != null ? this.data.title : 'Message';
  }
}
