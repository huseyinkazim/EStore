import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './service/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  pageTitle = 'EStore.UI';
  currentYear: number = new Date().getFullYear();
  constructor(private router: Router,
    private authService: AuthService) {     }

  isLoggedIn() {
    return this.authService.isUserLoggedIn(); 
  }

  redirectToLogin() {
    this.router.navigate(['/login']);
  }

  redirectToRegister() {
    this.router.navigate(['/register']);
  }

}
