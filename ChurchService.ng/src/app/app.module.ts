import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './library/material/material.module';
import { AccountModule } from './account/account.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { DialogModule } from './dialog/dialog.module';
import { AdminModule } from './admin/admin.module';
import { UsersModule } from './users/users.module';
import { AuthorizationInterceptor } from './security/authorization.inteceptor';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { ChurchModule } from './church/church.module';
import { LibraryModule } from './library/library.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    AccountModule,
    DialogModule,
    AdminModule,
    UsersModule,
    ChurchModule,
    LibraryModule,
    CalendarModule.forRoot({ provide: DateAdapter, useFactory: adapterFactory })
  ],

  providers: [
    AuthorizationInterceptor,
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizationInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
