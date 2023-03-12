import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AddChurchModel } from '../admin.model';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-create-church',
  templateUrl: './create-church.component.html',
  styleUrls: ['./create-church.component.css']
})
export class CreateChurchComponent implements OnInit {



  constructor(
    private readonly adminService: AdminService,
    private readonly snackBar: MatSnackBar
    ){
    
  }

  public model: AddChurchModel;

  ngOnInit(): void {
    this.model = new AddChurchModel();
  }


  public AddChurch():void {
    this.adminService.AddChurch(this.model).subscribe( success => {
      if (success){
        this.model = new AddChurchModel();
        this.snackBar.open('Church added!', 'Dismiss', {duration: 3000});
      } else {
        this.snackBar.open('Adding church failed!', 'Dismiss', {duration: 3000});
      }
    })
  }





}
