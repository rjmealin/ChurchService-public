import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../library/material/material.module';
import { CreateChurchComponent } from './create-church/create-church.component';
import { AdminService } from './admin.service';
import { DialogService } from '../dialog/dialog.service';



@NgModule({
  declarations: [
    CreateChurchComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    RouterModule
  ],
  providers: [
    AdminService,
    DialogService
  ]
})
export class AdminModule { }
