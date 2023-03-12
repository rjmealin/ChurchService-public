import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AddMemberModel } from 'src/app/church/church.model';

@Component({
  selector: 'app-add-member',
  templateUrl: './add-member.component.html',
  styleUrls: ['./add-member.component.css']
})
export class AddMemberComponent {

  public model:AddMemberModel = new AddMemberModel();
  public memberForm = new FormGroup({
    FirstName: new FormControl('', Validators.required),
    LastName: new FormControl('', Validators.required), 
    Email: new FormControl('', Validators.required), 
    PhoneNumber: new FormControl('', Validators.required), 
    Address: new FormControl('', Validators.required), 
    City: new FormControl('', Validators.required), 
    State: new FormControl('', Validators.required), 
    Zip: new FormControl('', Validators.required), 
    Password: new FormControl('', Validators.required), 
  })

  constructor(public dialogRef: MatDialogRef<AddMemberComponent>){}

  
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
