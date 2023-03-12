import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ChurchModel } from '../church.model';
import { ChurchService } from '../church.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-church-profile',
  templateUrl: './church-profile.component.html',
  styleUrls: ['./church-profile.component.css']
})
export class ChurchProfileComponent implements OnInit{

  constructor(
    private readonly churchService:ChurchService,
    private readonly snackBar:MatSnackBar
  ){}

  public model:ChurchModel;

  ngOnInit(): void {
    this.churchService.GetChurchProfile().subscribe( profile => {
      if (profile != null){
        this.model = profile;
      }
    })
  }










  /**
   * Updates the church's profile with the given model
   */
  public UpdateChurchProfile():void {
    this.churchService.UpdateChurchProfile(this.model).subscribe( success => {
      if(success){
        this.snackBar.open('Profile successfully updated!', 'Dismiss', {duration: 3000});
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
                    this.model.ChurchLogoMime = file.type;
                    let reader = new FileReader();
                    reader.addEventListener( 'load', () => {
                        this.model.ChurchLogoDataUrl = reader.result as string;
                    } );
                    reader.readAsDataURL( file );
                }
            }
        }
    }



}
