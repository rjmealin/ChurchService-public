import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogService } from '../dialog/dialog.service';
import { MaterialModule } from '../library/material/material.module';
import { FormsModule, NgModel } from '@angular/forms';
import { PhoneNumberPipe } from './phone-number.pipe';
import { AppMaskDirective } from './app-mask.directive';
import { PhoneMaskDirective } from './phone-mask.directive';



@NgModule({
  declarations: [
    PhoneNumberPipe,
    AppMaskDirective,
    PhoneMaskDirective
  ],
  exports:[
    PhoneNumberPipe,
    AppMaskDirective,
    PhoneMaskDirective
  ],
  providers:[
    NgModel
  ]
})
export class LibraryModule { }
