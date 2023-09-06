import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from "@angular/router";
import { ObjectUtil } from "../common/Extension";

@Injectable({ providedIn: 'root' })
export class CallerInformationGuard implements CanActivate {


    public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let callerPage: string = route.component!['callerPage' as keyof typeof route.component];
        if (!ObjectUtil.isNullOrWhiteSpace(callerPage))
            console.log("component: " + callerPage);
        else
            'undefined';

        return true;
    }
}