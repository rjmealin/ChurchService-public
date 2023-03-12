import { Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { AuthorizationService } from "./authorization.service";

@Injectable()
export class LoginGuard implements CanActivate {

    constructor( private readonly router: Router, private readonly authService: AuthorizationService ) { }

    public canActivate( route: ActivatedRouteSnapshot, state: RouterStateSnapshot ): boolean {
        let tokenValid:boolean = false;
        let today = new Date();

        if(this.authService.jsonWebToken?.expires != null){
            tokenValid = this.authService.jsonWebToken?.expires > today;
        }

        return tokenValid;
    }

}