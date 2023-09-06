import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './service/auth.service';
import { JwtdecoderService } from './service/jwtdecoder.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  pageTitle = 'EStore.UI';
  currentYear: number = new Date().getFullYear();
  constructor(private router: Router,
    private authService: AuthService,
    private jwtdecoderService:JwtdecoderService) { }

  isLoggedIn() {
    return this.authService.isUserLoggedIn();
  }

  redirectToLogin() {
    this.router.navigate(['/login']);
  }

  redirectToRegister() {
    this.router.navigate(['/register']);
  }
  logout() {
    return this.authService.removeToken();
  }
  getUserName():string{
    return this.jwtdecoderService.getTokenInfo().userName;
  }
}
