import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.css']
})
export class BaseComponent {
  isLoggedIn: boolean = false;
  isAdmin: boolean = false;
  isUser: boolean = false;
  constructor(protected authService: AuthService) {
    this.isLoggedIn = this.authService.isUserLoggedIn();
    this.isAdmin = this.authService.getTokenInfo()?.roles?.some(i => i == "admin") == true ? true : false;
    this.isUser = this.authService.getTokenInfo()?.roles?.some(i => i == "user") == true ? true : false;
  }

}
