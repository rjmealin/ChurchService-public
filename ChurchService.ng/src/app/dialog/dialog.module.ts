import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertDialogComponent } from './alert/alert.component';
import { MaterialModule } from '../library/material/material.module';
import { AddVisitorCardComponent } from './add-visitor-card/add-visitor-card.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AssignMemberComponent } from './assign-member/assign-member.component';
import { AddMemberComponent } from './add-member/add-member.component';
import { ConfirmComponent } from './confirm/confirm.component';
import { ChurchService } from '../church/church.service';
import { ChurchModule } from '../church/church.module';
import { EmailBlastComponent } from './email-blast/email-blast.component';
import { AppMaskDirective } from '../library/app-mask.directive';
import { LibraryModule } from '../library/library.module';
import { NgxMaskDirective } from 'ngx-mask/lib/ngx-mask.directive';
import { provideNgxMask } from 'ngx-mask/lib/ngx-mask.providers';
import { VisitorCardDetailsComponent } from './visitor-card-details/visitor-card-details.component';



@NgModule({
  declarations: [
    AlertDialogComponent,
    AddVisitorCardComponent,
    AssignMemberComponent,
    AddMemberComponent,
    ConfirmComponent,
    EmailBlastComponent,
    VisitorCardDetailsComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ChurchModule,
    LibraryModule
  ],
  providers: [
    ChurchService,
    AppMaskDirective
  ]
})
export class DialogModule { }
