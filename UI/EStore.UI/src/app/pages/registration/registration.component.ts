import { Component } from '@angular/core';
import { RegistrationRequestDto } from 'src/app/common/RegistrationRequestDto';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  registrationData: RegistrationRequestDto = new RegistrationRequestDto();

  constructor(private authService: AuthService) { }

  onRegisterSubmit(): void {
    if (this.registrationData.password !== this.registrationData.confirmPassword) {
      // Passwords don't match, handle this case
      return;
    }

    this.authService.register(this.registrationData);
  }
}
