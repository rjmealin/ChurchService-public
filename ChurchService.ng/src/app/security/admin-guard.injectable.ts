import { Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { AuthorizationService } from "./authorization.service";

@Injectable()
export class AdminGuard implements CanActivate {

    constructor( private readonly router: Router, private readonly authService: AuthorizationService ) { }

    public canActivate( route: ActivatedRouteSnapshot, state: RouterStateSnapshot ): boolean {

        let isAdmin: boolean = false;

        if(this.authService.jsonWebToken != null){
            isAdmin = this.authService.jsonWebToken.isAdmin
        }

        return isAdmin;
    }

}