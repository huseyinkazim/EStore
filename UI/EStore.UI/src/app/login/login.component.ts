import { Component } from '@angular/core';
import { LoginRequestDto } from 'src/app/common/LoginRequestDto';
import { AuthService } from 'src/app/service/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginRequest: LoginRequestDto = new LoginRequestDto ();

  constructor(private authService: AuthService) { }

  onLoginSubmit() {
    this.authService.login(this.loginRequest);
  }
}
