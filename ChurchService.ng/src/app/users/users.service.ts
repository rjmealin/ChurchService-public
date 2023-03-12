import { HttpClient, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { catchError, map, Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { DialogService } from "../dialog/dialog.service";
import { AuthorizationService } from "../security/authorization.service";
import { BaseHttpService } from "../services/base-http.service";
import { DateModel, EmailBlastModel, MemberProfileModel, PlannedVisitModel, VisitorCardModel } from "./users.model";

@Injectable( {
    providedIn: 'root',
} )


export class UsersService extends BaseHttpService {



    constructor( private readonly router: Router, private readonly dialogService: DialogService, private readonly httpClient: HttpClient, private readonly authService: AuthorizationService ) {
        super( router, dialogService );
    }






    /**
     * sends the model to the database to be saved
     */
    public SaveVisitorCard(model:VisitorCardModel): Observable<Boolean>{
        return this.httpClient.post<boolean>(`${environment.apiUrl}/church/visitor-card`, model , { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }








    /**
     *  Gets a list of all visitor cards that belong to a church
     */
    public GetVisitorCards(): Observable<VisitorCardModel[]>{
        return this.httpClient.get<VisitorCardModel[]>(`${environment.apiUrl}/church/visitor-cards`).pipe(
            catchError( ( err: any ) => this.handleError( err ) ) );
    }









    /**
     *Gets teh details of a visitor card base off it database id 
     */
    public GetVisitorCardDetails(cardId:string):Observable<VisitorCardModel>{
        return this.httpClient.get<VisitorCardModel>(`${environment.apiUrl}/church/visitor-card/${cardId}`).pipe(
            catchError( ( err: any ) => this.handleError( err ) ) );
    }




    





    /**
     * Gets the count of unassigned visits
     */
    public GetUnassignedVisits(): Observable<number> {
        return this.httpClient.get<number>(`${environment.apiUrl}/church/unassigned-visits`).pipe(
            catchError( ( err: any ) => this.handleError( err ) ));
    }










    
    /**
     *  Gets all teh planned visits for a given church 
     */
    public GetPlannedVisits(): Observable<PlannedVisitModel[]>{
        return this.httpClient.get<PlannedVisitModel[]>(`${environment.apiUrl}/church/planned-visits`).pipe(
            catchError( ( err: any ) => this.handleError( err ) ));
    }











    /**
     *  Gets all the dates unavailable for a given user 
     */
    public GetUnavailableDates(): Observable<Date[]>{
        return this.httpClient.get<Date[]>(`${environment.apiUrl}/users/get-unavailable-dates`).pipe(
            catchError( ( err: any ) => this.handleError( err ) ));
    }








    
    /**
     *  Adds a new date unavailable for the given user 
     */
    public AddUnavaiableDate(date: DateModel): Observable<boolean> {
        return this.httpClient.post<boolean>(`${environment.apiUrl}/users/add-unavailable-date`, date , { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }










    /**
     *  Removes the date unavaiable 
     */
    public RemoveUnavaiableDate(date: DateModel): Observable<boolean>{
        return this.httpClient.post<boolean>(`${environment.apiUrl}/users/remove-unavailable-date`, date , { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }









    /**
     *  Updates the member profile with the outgoing model 
     */
    public UpdateMemberProfile(model:MemberProfileModel): Observable<boolean>{
        return this.httpClient.patch<boolean>(`${environment.apiUrl}/users/update-profile`, model , { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }











    /**
     *  Gets the member profile 
     */
    public GetMemberProfile():Observable<MemberProfileModel>{
        return this.httpClient.get<MemberProfileModel>(`${environment.apiUrl}/users/get-profile`).pipe(
            catchError( ( err: any ) => this.handleError( err ) ));
    }











    /**
     *  Sends the user an sms code to verify thier phone number 
     */
    public SendSmsCode():Observable<boolean>{
        return this.httpClient.post<boolean>(`${environment.apiUrl}/users/send-sms-verification`, null , { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }










    
    /**
     *  attempts to verify the phone with the sms code 
     */
    public VerifySms(code: number): Observable<boolean>{
        return this.httpClient.patch<boolean>(`${environment.apiUrl}/users/verify-sms/${code}`, null , { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }










    /**
     *  Sends all the visitors emails 
     */
    public SendVisitorsEmails( blast: EmailBlastModel): Observable<boolean>{
        return this.httpClient.post<boolean>(`${environment.apiUrl}/users/send-emails`, blast , { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }









    
    /**
     * Removes a visitor card attached to a given id
     */
    public RemoveVisitorCard(cardId: string):Observable<boolean>{
        return this.httpClient.delete<boolean>(`${environment.apiUrl}/church/remove-card/${cardId}`, { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }
}