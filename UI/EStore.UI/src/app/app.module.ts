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
import { ShoppingCartComponent } from './pages/shopping-cart/shopping-cart/shopping-cart.component';
import { BaseComponent } from './pages/base/base/base.component';
import { OpenablebutonComponent } from './core/openablebuton/openablebuton.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatListModule } from '@angular/material/list';

@NgModule({
  declarations: [
    AppComponent,
    CouponComponent,
    LoginComponent,
    RegistrationComponent,
    HomeComponent,
    ProductComponent,
    ShoppingCartComponent,
    BaseComponent,
    OpenablebutonComponent    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule, 
    MatExpansionModule,
    MatListModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(),// ToastrModule added
  ],
  exports:[RouterModule],
  providers: [CookieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
