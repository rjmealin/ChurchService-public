import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { DialogService } from '../dialog/dialog.service';
import { BaseHttpService } from '../services/base-http.service';

import { AuthorizationService } from '../security/authorization.service';
import { AddChurchModel } from './admin.model';


@Injectable( {
    providedIn: 'root',
} )


export class AdminService extends BaseHttpService {



    constructor( private readonly router: Router, private readonly dialogService: DialogService, private readonly httpClient: HttpClient, private readonly authService: AuthorizationService ) {
        super( router, dialogService );
    }



    public AddChurch(model: AddChurchModel ) : Observable<boolean> {
        return this.httpClient.post(`${environment.apiUrl}/church/add-church`, model,{ observe: 'response', responseType: 'json' } ).pipe(
            map<HttpResponse<Object>, boolean>( value => value.status === 204 ),
            catchError( ( err: any ) => this.handleError( err ) ) );
    }
}