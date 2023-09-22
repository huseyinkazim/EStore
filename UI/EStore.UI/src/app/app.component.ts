import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './service/auth.service';
import { BaseComponent } from './pages/base/base/base.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent extends BaseComponent {
  pageTitle = 'EStore.UI';
  currentYear: number = new Date().getFullYear();
  constructor(private router: Router,
    protected override authService: AuthService) {
    super(authService);
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
  getUserName(): string {
    return this.authService.getTokenInfo().userName;
  }
  getUserId() {
    return this.authService.getTokenInfo().userId;
  }
}
