import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { EmailBlastModel } from 'src/app/users/users.model';
import { ConfirmComponent } from '../confirm/confirm.component';

@Component({
  selector: 'app-email-blast',
  templateUrl: './email-blast.component.html',
  styleUrls: ['./email-blast.component.css']
})
export class EmailBlastComponent {

  constructor(
    public dialogRef: MatDialogRef<ConfirmComponent>) { }
  public model: EmailBlastModel = new EmailBlastModel();

  public Ok():void {
    this.dialogRef.close(this.model);
  }
  public Cancel():void {
    this.dialogRef.close(null);
  }
}
