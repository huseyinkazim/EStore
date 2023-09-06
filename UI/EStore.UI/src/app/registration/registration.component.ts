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

  constructor(private authService: AuthService,
    private toastr: ToastrService,
    private router: Router) { }

  onRegisterSubmit(): void {
    if (this.registrationData.password !== this.registrationData.confirmPassword) {
      // Passwords don't match, handle this case
      return;
    }

    this.authService.register(this.registrationData).subscribe(
      (response: ResponseDto) => {

        // Registration successful, you can navigate to another page or show a success message
        console.log('Registration successful:', response);
        if (response.isSuccess) {
          this.toastr.success("Uyelik işleminiz başarılı 'Login' sayfasına yönlendireleceksiniz", 'Uyelik Başarılı');
          this.router.navigate(['/login']);
        }
        else{
          this.toastr.warning(response.message, 'Uyelik Başarısız');
        }
      },
      (error: any) => {
        this.toastr.error("Uyelik işleminiz başarısız", 'Uyelik Başarısız');

        // Registration failed, handle the error (e.g., display an error message)
        console.error('Registration failed:', error);
      }
    );
  }
}
