import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../service/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    if (this.authService.isUserLoggedIn()) {
      return true; // Kullanıcı oturumu geçerliyse sayfaya erişime izin ver
    } else {
      this.router.navigate(['/login']); // Kullanıcı oturumu geçerli değilse login sayfasına yönlendir
      return false; // Sayfaya erişime izin verme
    }
  }
}