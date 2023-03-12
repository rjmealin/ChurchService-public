import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { DialogService } from '../dialog/dialog.service';
import { BaseHttpService } from '../services/base-http.service';
import { PasswordResetModel, PasswordChangeModel } from './account.model';

import { AuthorizationService } from '../security/authorization.service';


@Injectable( {
    providedIn: 'root',
} )


export class AccountService extends BaseHttpService {



    constructor( private readonly router: Router, private readonly dialogService: DialogService, private readonly httpClient: HttpClient, private readonly authService: AuthorizationService ) {
        super( router, dialogService );
    }


    /**
     * Verifies the user email with a verification id 
     */
    public VerifyEmail( id: string ): Observable<boolean> {
        return this.httpClient.get<boolean>( `${environment.apiUrl}/users/verify-email/${id}`, { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }










    /**
     * Sends a new email verification to the user email 
     */
    public SendNewEmailVerification(): Observable<boolean> {
        return this.httpClient.post<boolean>( `${environment.apiUrl}/users/send-email-verification`, null, { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }







    


    /**
     * Sends a password reset request to a supplied email 
     */
    public SendPasswordReset( email: string ): Observable<boolean> {
        return this.httpClient.post( `${environment.apiUrl}/users/password-reset/email?email=${encodeURIComponent( email )}`, null, { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }









    /**
     * Attempts to use a Password reset id to reset a user password
     */
    public ResetUserPassword( model: PasswordResetModel ): Observable<boolean> {
        return this.httpClient.post<boolean>( `${environment.apiUrl}/users/reset-password`, model, { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }










    /**
     *Attempts to reset the user password with the reset model 
     */
    public ChangeUserPassword( model: PasswordChangeModel ): Observable<boolean> {
        return this.httpClient.post<boolean>( `${environment.apiUrl}/users/change-password`, model, { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }
}

