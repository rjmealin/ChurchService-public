import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { EmailVerifyComponent } from './email-verify/email-verify.component';
import { LoginComponent } from './login/login.component';
import { AccountRecoveryComponent } from './account-recovery/account-recovery.component';
import { MaterialModule } from '../library/material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthorizationService } from '../security/authorization.service';
import { DialogService } from '../dialog/dialog.service';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { AccountService } from './account.service';
import { ResetPasswordComponent } from './reset-password/reset-password.component';



@NgModule({
  declarations: [
    ChangePasswordComponent,
    EmailVerifyComponent,
    LoginComponent,
    AccountRecoveryComponent,
    ResetPasswordComponent
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
    AuthorizationService,
    AccountService,
    DialogService,
],
})
export class AccountModule { }
