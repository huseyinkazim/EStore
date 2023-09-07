import { Component } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { RegistrationRequestDto } from '../common/RegistrationRequestDto';
import { ResponseDto } from '../common/responsedto';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

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
