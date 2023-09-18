import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CallerInformationGuard } from './guards/callerInformation.guard';
import { AuthGuard } from './guards/authentication.guard';
import { CouponComponent } from './pages/coupon/coupon/coupon.component';
import { LoginComponent } from './pages/login/login.component';
import { HomeComponent } from './pages/home/home.component';
import { RegistrationComponent } from './pages/registration/registration.component';
import { ProductComponent } from './pages/product/product.component';
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
  { path: 'product', component: ProductComponent }, 
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
