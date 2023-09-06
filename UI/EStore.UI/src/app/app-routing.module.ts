import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CouponComponent } from './coupon/coupon/coupon.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { CallerInformationGuard } from './guards/callerInformation.guard';
import { AuthGuard } from './guards/authentication.guard';
import { HomeComponent } from './home/home.component';
export const CAN_ACTIVATE_GUARDS: any[] =
  [
    AuthGuard,
    CallerInformationGuard
  ];
const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' }, // Ana sayfayı "/coupons" sayfasına yönlendirir.
  { path: 'coupons', component: CouponComponent, canActivate: CAN_ACTIVATE_GUARDS }, 
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent }, 
  { path: 'register', component: RegistrationComponent }, 
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
