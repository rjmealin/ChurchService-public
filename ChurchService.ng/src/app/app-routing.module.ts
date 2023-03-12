import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountRecoveryComponent } from './account/account-recovery/account-recovery.component';
import { LoginComponent } from './account/login/login.component';
import { ResetPasswordComponent } from './account/reset-password/reset-password.component';
import { CreateChurchComponent } from './admin/create-church/create-church.component';
import { ChurchProfileComponent } from './church/church-profile/church-profile.component';
import { MembersEditComponent } from './church/members-edit/members-edit.component';
import { AdminGuard } from './security/admin-guard.injectable';
import { LoginGuard } from './security/login-guard.injectable';
import { DashboardComponent } from './users/dashboard/dashboard.component';
import { MemberAvailabilityComponent } from './users/member-availability/member-availability.component';
import { MemberProfileComponent } from './users/member-profile/member-profile.component';
import { PlannedVisitsComponent } from './users/planned-visits/planned-visits.component';
import { VerifySmsComponent } from './users/verify-sms/verify-sms.component';
import { VisitorCardsComponent } from './users/visitor-cards/visitor-cards.component';

const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'account-recovery', component:AccountRecoveryComponent},
  {path: 'reset-password/:id', component:ResetPasswordComponent},
  {
    path: 'member', 
    canActivate:[LoginGuard], 
    children: [
      {path: 'dashboard', component:DashboardComponent},
      {path: 'verify-sms', component:VerifySmsComponent},
      {path: 'profile', component:MemberProfileComponent},
      {path: 'planned-visits', component:PlannedVisitsComponent},
      {path: 'visitor-cards', component:VisitorCardsComponent},
      {path: 'member-availability', component:MemberAvailabilityComponent},
    ]},
  {
    path:'admin', 
    canActivate:[AdminGuard], 
    children:[
      {path: 'members-edit', component:MembersEditComponent},
      {path: 'church-profile', component:ChurchProfileComponent}
    ]},
];



@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers:[
    LoginGuard,
    AdminGuard
  ]
})
export class AppRoutingModule { }
