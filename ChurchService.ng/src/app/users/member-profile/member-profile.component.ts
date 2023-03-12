import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { DialogService } from 'src/app/dialog/dialog.service';
import { environment } from 'src/environments/environment';
import { MemberProfileModel } from '../users.model';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-member-profile',
  templateUrl: './member-profile.component.html',
  styleUrls: ['./member-profile.component.css']
})
export class MemberProfileComponent implements OnInit{

  constructor(
    private readonly userService:UsersService,
    private readonly snackBar:MatSnackBar,
    private readonly router:Router
  ){}

  public profile:MemberProfileModel = new MemberProfileModel();

  ngOnInit(): void {
    this.LoadData();
  }


  /**
   * Gets the member profile
   */
  public LoadData():void {
    this.userService.GetMemberProfile().subscribe( profile => {
      if(profile != null){
        this.profile = profile;
      }
    })
  }








  /**
   * Update the Member profile with the model
   */
  public UpdateMemberProfile():void {
    this.userService.UpdateMemberProfile(this.profile).subscribe( success => {
      if(success){
        //this.LoadData();
        this.snackBar.open('You have updated your profile!', 'Dismiss', {duration:3000});
      }
    })
  }










  /**
   * Sends an sms code to the users phone number
   */
  public SendSmsCode():void {
    this.userService.SendSmsCode().subscribe( success => {
      if(success){
        this.router.navigateByUrl('member/verify-sms');
      }
    })
  }










        /**
     *  Processes the Image when uploaded and converts it to a dataURl 
     */
        onImageSelected( e: Event ): void {
          let files: FileList | null = ( e.target as HTMLInputElement ).files;
  
          if ( files !== null && files?.length > 0 ) {
              for ( let i = 0; i < files.length; i++ ) {
                  const file = files[i];
  
                  if ( file.size > environment.maxFileSize ) {
                    this.snackBar.open( `${file.name} was too large! Images must be less than ${( environment.maxFileSize / 1048576 ).toFixed( 1 )}MB`, 'dismiss', { duration: 3000 } );
                    return;
                  }

                  if ( /\.(jpe?g|png|gif|svg)$/i.test( file.name ) ) {
                      this.profile.ProfileImageMime = file.type;
                      let reader = new FileReader();
                      reader.addEventListener( 'load', () => {
                          this.profile.ProfileImageDataUrl = reader.result as string;
                      } );
                      reader.readAsDataURL( file );
                  }
              }
          }
      }
}
