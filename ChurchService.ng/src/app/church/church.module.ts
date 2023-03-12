import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MembersEditComponent } from './members-edit/members-edit.component';
import { ChurchProfileComponent } from './church-profile/church-profile.component';
import { ChurchService } from './church.service';
import { DialogService } from '../dialog/dialog.service';
import { MaterialModule } from '../library/material/material.module';
import { FormsModule } from '@angular/forms';
import { LibraryModule } from '../library/library.module';



@NgModule({
  declarations: [
    MembersEditComponent,
    ChurchProfileComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    LibraryModule
  ],
  providers:[    
    ChurchService,
    DialogService
  ]
})
export class ChurchModule { }
