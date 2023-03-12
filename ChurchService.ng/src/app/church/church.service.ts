import { HttpClient, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { catchError, map, Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { DialogService } from "../dialog/dialog.service";
import { AuthorizationService } from "../security/authorization.service";
import { BaseHttpService } from "../services/base-http.service";
import { DatabaseOptionModel, PlannedVisitModel } from "../users/users.model";
import { AddMemberModel, ChurchModel, MemberDetailsModel } from "./church.model";

@Injectable( {
    providedIn: 'root',
} )


export class ChurchService extends BaseHttpService {



    constructor( private readonly router: Router, private readonly dialogService: DialogService, private readonly httpClient: HttpClient, private readonly authService: AuthorizationService ) {
        super( router, dialogService );
    }






    /**
     * Gets all the members attached a given church
     */
    public GetChurchMembers(): Observable<MemberDetailsModel[]>{
        return this.httpClient.get<MemberDetailsModel[]>(`${environment.apiUrl}/church/members`).pipe(
            catchError( ( err: any ) => this.handleError( err ) ) );
    }











    /**
     * Adds a church member for a given church 
     */
    public AddChurchMember(model:AddMemberModel): Observable<boolean>{
        return this.httpClient.post<boolean>(`${environment.apiUrl}/church/add-member`, model , { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );

    }










    /**
     *  Removes a church member based off their userId 
     */
    public RemoveChurchMember(memberId: string): Observable<boolean>{
        return this.httpClient.patch<boolean>(`${environment.apiUrl}/church/remove-member/${memberId}`, null , { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );

    }









    /**
     *  Assigns a given member to a visit based off its id 
     */
    public AssignVisit(visitId: string, memberId:string): Observable<boolean>{
        return this.httpClient.patch<boolean>(`${environment.apiUrl}/church/assign-visit/${visitId}/${memberId}`, null , { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }









    /**
     * Upgrades a member to admin status
     */
    public UpgradeMemberToAdmin(memberId: string): Observable<boolean>{
        return this.httpClient.patch<boolean>(`${environment.apiUrl}/church/upgrade-member/${memberId}`, null , { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }









    
    /**
     * Gets all members avaiable on a given date
     */
    public GetAllAvailableMembers(date: string):Observable<DatabaseOptionModel[]>{
        return this.httpClient.get<DatabaseOptionModel[]>(`${environment.apiUrl}/church/get-available-members/${date}`).pipe(
            catchError( ( err: any ) => this.handleError( err ) ) );
    }









    
    /**
     *  Updates the church's profile with the out going model 
     */
    public UpdateChurchProfile(model: ChurchModel):Observable<boolean>{
        return this.httpClient.put<boolean>(`${environment.apiUrl}/church/update-church`, model , { observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }









    
    /**
     *  Gets the profile for a given church 
     */
    public GetChurchProfile():Observable<ChurchModel>{
        return this.httpClient.get<ChurchModel>(`${environment.apiUrl}/church/get-church-profile`).pipe(
            catchError( ( err: any ) => this.handleError( err ) ) );
    }










    
    /**
     *  Gets the details for a specific visit 
     */
    public GetVisitDetails(visitId: string):Observable<PlannedVisitModel>{
        return this.httpClient.get<PlannedVisitModel>(`${environment.apiUrl}/church/get-visit/${visitId}`).pipe(
            catchError( ( err: any ) => this.handleError( err ) ) );
    }
}