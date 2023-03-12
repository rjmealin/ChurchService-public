import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';
import { environment } from '../../environments/environment';
import { JsonWebTokenModel } from './security.model';

@Injectable()
export class AuthorizationService {

    private token: JsonWebTokenModel | undefined;

    private readonly isEmailVerifiedObservers: Array<Observer<boolean>> = [];

    private readonly isLoggedinObservers: Array<Observer<boolean>> = [];

    private readonly isAdminObservers: Array<Observer<boolean>>= [];

    constructor(
        private readonly httpClient: HttpClient,
        private readonly router: Router,
    ) {
        this.parseJsonWebToken();
    }










    /**
     * Logs out the current user and redirects to the login page.
     */
    public Logout(): void {
        localStorage.removeItem( 'access_token' );
        this.token = undefined;
        void this.router.navigate( [ '' ] );
    }
    









    /**
     * Attempt user login
     * @param email user email as login identity
     * @param password user's password
     */
    public Login( email: string, password: string, rememberMe: boolean ): Observable<boolean> {
        let obs: Observable<boolean> = new Observable<boolean>( observer => {
            
            let httpOptions : object = {
                observe: 'response',
                responseType: 'text',
            } 

            this.httpClient
                .post<any>( `${environment.apiUrl}/users/login`, { Email: email, Password: password }, httpOptions )
                .subscribe( {
                    next: ( res: any ) => {

                        localStorage.removeItem( 'access_token' );
                        this.token = undefined;

                        localStorage.setItem( 'access_token', res.body );
                        // force read of the JWT and save it. we can't mess with the token,
                        // but we're going to want to both change state AND have it remember
                        // if the user goes directly to a bookmarked page.

                        if ( this.jsonWebToken != null ) {
                            this.jsonWebToken.rememberMe = rememberMe;
                        }
                        
                        if ( this.jsonWebToken?.isAdmin != null && this.jsonWebToken?.isAdmin ) {
                            this.jsonWebToken.isAdmin = true;
                            
                            observer.next( true );

                        } else if ( this.jsonWebToken !== undefined ) {
                            observer.next( true );

                        } else {
                            observer.error( 'No JWT returned or No Role Defined!' );
                            
                        }

                        observer.complete();
                        // last step before we wind this up

                    },
                    error:
                        err => {
                            if ( err.status === 401 || err.status === 404 ) {
                                observer.error( 'Unauthorized' );
                            } else if ( err.status === 422 ) {
                                observer.error( 'EmailUnverified' );
                                // Do nothing
                            } else { observer.error( err.error.ExceptionMessage ); }
                        },
                } );

        } );

        return obs;
    }










    /**
     * Gets the church logo attached to a given church
     */
    public GetChurchLogo(): Observable<string> {

        let obs: Observable<string> = new Observable<string>( observer => {
            let httpOptions : object = {
                observe: 'response',
                responseType: 'text',
            }
            
            this.httpClient.get<string>(`${environment.apiUrl}/church/logo`, httpOptions).subscribe({ next: (res: any) => {
                observer.next(res.body);
                observer.complete();
            }} );
        });

        return obs;
    }





    











    /**
     * Parses the local storage access_token and populates the token property.
     */
    private parseJsonWebToken(): void {

        let accessToken: string | null = localStorage.getItem( 'access_token' );
        
        if ( accessToken != null ) {
            let payload: string = accessToken.split( /\./g )[1];
            let parsed: any = JSON.parse( window.atob( payload ) );

            // this.token = Object.assign( new JsonWebTokenModel(), parsed );
            this.token = new JsonWebTokenModel();
            this.token.userId = parsed.userId;
            this.token.churchId = parsed.churchId;
            this.token.emailVerified = parsed.emailVerified === 'true';
            this.token.fullName = parsed.fullName;
            this.token.expires = new Date( parsed.exp * 1000 );
            this.token.isAdmin = parsed.isAdmin === 'true';

            console.log(`user id: ${this.token.userId}`);
            console.log(`church id: ${this.token.churchId}`);
            // Update observers
            this.isEmailVerifiedObservers.forEach( observer => observer.next( this.token?.emailVerified === true ) );
            this.isLoggedinObservers.forEach( observer => observer.next( this.token != null ) );
        }
    }
    









    /**
     * Returns the JWT in local storage
     */
    public get jsonWebToken(): JsonWebTokenModel | undefined {

        if ( this.token === undefined || this.token === null ) {

            this.parseJsonWebToken();
            
        }

        return this.token;
    }








    

    /**
     * Sets and parses the JWT obtained from the api
     */
    public SetToken( authToken: string ): void {

        localStorage.setItem( 'access_token', authToken );
        
        this.parseJsonWebToken();
    }


    







    /**
     * Get the current User Id
     */
    public GetUserId(): string | undefined {
        return this.jsonWebToken?.userId;
    }
























    


    /**
     * Creates an observable to read the JWT property emailVerified
     */
    public get emailVerified(): Observable<boolean> {
        let obs: Observable<boolean> = new Observable<boolean>( ( observer: Observer<boolean> ) => {
            
            this.isEmailVerifiedObservers.push( observer );

            if ( this.token != null ) {
                observer.next( this.token.emailVerified );
            }
            
        } );
        
        return obs;
    }















    
    /**
     * Creates an observable and determines if there is a user logged in or not
     */
    public get isLoggedIn(): Observable<boolean> {
        let obs: Observable<boolean> = new Observable<boolean>( ( observer: Observer<boolean> ) => {
            this.isLoggedinObservers.push( observer );

            observer.next( this.jsonWebToken != null );

        } );
        return obs;
    }












    /**
     * Creates an observable that determines if a user is an admin or not
     */
    public get isAdmin():Observable<boolean>{
        let obs: Observable<boolean> = new Observable<boolean>((observer: Observer<boolean>) => {
            this.isAdminObservers.push(observer);

            let isAdmin: boolean = false;

            if(this.jsonWebToken != null){
                isAdmin = this.jsonWebToken.isAdmin
            }

            observer.next(isAdmin);
        });
        return obs;
    }









    /**
     * Sets the remember me property in the JWT
     */
    public set rememberMe( rememberMe: boolean ) {
        if ( this.token != null ) {
            this.token.rememberMe = rememberMe;
        }
    }









    
    /**
     * Gets the rememberMe property in the jwt
     */
    public get rememberMe(): boolean {
        if ( this.token != null ) {
            return this.token.rememberMe;
        } else {
            return false;
        }
    }

    
}
