import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthorizationInterceptor implements HttpInterceptor {

    private get authToken(): string | null {
        return localStorage.getItem( 'access_token' );
    }

    constructor() { }

    intercept( req: HttpRequest<any>, next: HttpHandler ): Observable<HttpEvent<any>> {
        
        if ( this.authToken != null ) {
            let authReq: HttpRequest<any> = req.clone( { setHeaders: { Authorization: this.authToken } } );
            
            return next.handle( authReq );
        } else {
            return next.handle( req );
        }
        
    }
}
