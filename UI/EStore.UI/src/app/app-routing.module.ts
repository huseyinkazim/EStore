import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CouponComponent } from './coupon/coupon/coupon.component';

const routes: Routes = [
  { path: '', redirectTo: '/coupons', pathMatch: 'full' }, // Ana sayfayı "/coupons" sayfasına yönlendirir.
  { path: 'coupons', component: CouponComponent }, // /coupons URL'i CouponComponent'e yönlendirir.
  // Diğer yönlendirmeleri buraya ekleyebilirsiniz.
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
