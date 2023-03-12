import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { throwError as observableThrowError, Observable } from 'rxjs';

import { DialogService } from '../dialog/dialog.service';

export class BaseHttpService {

    constructor( private readonly _router: Router, private readonly _dialogService: DialogService ) { }



    /**
     * Handles errors in the http object.
     */
    protected handleError( errResponse: Response | any ): Observable<any> {
        
        try {
            
            if ( errResponse.status === 401 || errResponse.status === 403 ) {
                void this._router.navigate( [ '/login' ] );
            
            } else {
                
                let message: any = '';
                let exceptionMessage: string = '';
        
                if ( errResponse instanceof Response ) {
        
                    let body: any = errResponse.json() != null ? errResponse.json() : {};
                    let err: any = body.error != null ? body.Error : JSON.stringify( body );
                    console.log( err );
                    if ( err.Message != null ) {
                        message = err.Message.toString();
                    } else if ( body.Message != null ) {
                        message = body.Message.toString();
                    } else {
                        message = err.toString();
                    }
        
        
                } else if ( errResponse instanceof HttpErrorResponse && errResponse.error != null ) {
        
                    message = errResponse.error != null ? errResponse.error : 'Something went wrong!';

                }
                this._dialogService.showAlertDialog( 'something is wrong', message, true, false );
            }
            
            return observableThrowError( errResponse );
    
        } catch ( err ) {
            
            return observableThrowError( errResponse );
    
        }
    
    }









    
    /**
     * Creates an xhr and uploads the file to the server.
     * 
     * @param url The url to upload the file to.
     * @param files The files to upload.
     * @param onProgress The on progress function.
     */
    protected postFile<T>( url: string, file: any, onProgress?: Function ): Observable<T> {
        let formData: FormData = new FormData();
    
        formData.append( 'file', file );
            
        let obs: Observable<T> = new Observable( observer => {
                
            let xhr: XMLHttpRequest = new XMLHttpRequest();
                    
            xhr.onreadystatechange = () => {
                    
                if ( xhr.readyState === 4 ) {
                    if ( xhr.status === 200 ) {
                            
                        // The response will be a list of strings with the temp filenames, parse that value and pass it to the observer.
                        if ( xhr.response != null ) {
                            let result: T = JSON.parse( xhr.response ) as T;
                            observer.next( result );
                            observer.complete();
                        } else {
                            observer.next( undefined );
                            observer.complete();
                        }
                            
                    } else {
                            
                        // Anything other than a 200 will be handled as an error and pass the response to the observer.
                        observer.error( new Error( xhr.response ) );
                        observer.complete();
                            
                    }
                }
                    
            };
                
            xhr.upload.onprogress = ( event: ProgressEvent ) => {
                let percentComplete: number = Math.round( event.loaded / event.total );
                    
                if ( onProgress != null ) {
                    onProgress( percentComplete );
                }
    
            };
                
            xhr.open( 'POST', url, true );
            xhr.setRequestHeader( 'X-Requested-With', 'XMLHttpRequest' );
            xhr.setRequestHeader( 'Authorization', localStorage.getItem( 'access_token' ) ?? '' );
            xhr.send( formData );
                
        } );
    
        return obs;
    }









    
    /**
         * Saves the response body (blob) as a new file from the server.
         * 
         * @param response The response object from the server.
         * @param defaultFilename The default filename, used if one isn't given in the Content-Disposition header.
         */
    protected saveBlob( response: HttpResponse<Blob>, defaultFilename: string, mimeType: string ): boolean {
    
        let contentDisposition = response.headers.get( 'Content-Disposition' );
        let cdMatch = /^attachment; filename=(?<filename>.+),*$/gi.exec( contentDisposition != null ? contentDisposition : '' );
        let filename = cdMatch?.groups?.['filename'] ?? defaultFilename;
        
        let blob: Blob = new Blob( [ response.body != null ? response.body : '' ], { type: mimeType } );
        
        let downloadURL = window.URL.createObjectURL( blob );
        let link = document.createElement( 'a' );
    
        link.href = downloadURL;
        link.download = filename;
    
        link.click();
            
        return response.status === 200;
    }
}
