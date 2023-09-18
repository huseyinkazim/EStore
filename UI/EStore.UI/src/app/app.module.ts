import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { CouponComponent } from './pages/coupon/coupon/coupon.component';
import { LoginComponent } from './pages/login/login.component';
import { RegistrationComponent } from './pages/registration/registration.component';
import { HomeComponent } from './pages/home/home.component';
import { ProductComponent } from './pages/product/product.component';

@NgModule({
  declarations: [
    AppComponent,
    CouponComponent,
    LoginComponent,
    RegistrationComponent,
    HomeComponent,
    ProductComponent    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule, 
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(),// ToastrModule added
  ],
  exports:[RouterModule],
  providers: [CookieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
