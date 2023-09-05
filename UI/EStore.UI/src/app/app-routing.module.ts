import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CouponComponent } from './coupon/coupon/coupon.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' }, // Ana sayfayı "/coupons" sayfasına yönlendirir.
  { path: 'coupons', component: CouponComponent }, // /coupons URL'i CouponComponent'e yönlendirir.
  { path: 'login', component: LoginComponent }, // /coupons URL'i CouponComponent'e yönlendirir.
  // Diğer yönlendirmeleri buraya ekleyebilirsiniz.
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
