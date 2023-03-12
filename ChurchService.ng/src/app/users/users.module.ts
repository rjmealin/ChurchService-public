import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../library/material/material.module';
import { VisitorCardsComponent } from './visitor-cards/visitor-cards.component';
import { PlannedVisitsComponent } from './planned-visits/planned-visits.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { MemberProfileComponent } from './member-profile/member-profile.component';
import { MemberAvailabilityComponent } from './member-availability/member-availability.component';
import { VerifySmsComponent } from './verify-sms/verify-sms.component';



@NgModule({
  declarations: [
    DashboardComponent,
    VisitorCardsComponent,
    PlannedVisitsComponent,
    MemberProfileComponent,
    MemberAvailabilityComponent,
    VerifySmsComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    RouterModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
  ]
})
export class UsersModule { }
