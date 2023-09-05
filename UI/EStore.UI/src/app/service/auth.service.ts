import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginRequestDto } from '../common/LoginRequestDto';
import { LoginResponseDto } from '../common/LoginResponseDto';
import { RegistrationRequestDto } from '../common/RegistrationRequestDto';
import { UserDto } from '../common/UserDto';
import { AssignRoleRequestDto } from '../common/AssignRoleRequestDto';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isAuthenticated: boolean = false;
  private apiUrl = 'http://localhost:5257/api/Auth'; // API URL'sini buraya ekleyin
  private baseUrl = 'http://localhost:5108/api/Coupon'; // Replace with your API URL

  constructor(private http: HttpClient,
    private toastr: ToastrService) { }
 // Kullanıcı girişi
 test() {
  this.http.get(`${this.apiUrl}/test`).subscribe(
    (data) => {
      console.log(data);
      this.toastr.success('Kupon başarılı oluşturuldu', 'Oluşturma Onay');
    },
    (error) => {
      console.log(error);
      this.toastr.error('Kupon oluşturulurken beklenmedik hata!', 'Oluşturma Hatası');
    }
  );

  this.http.get(`${this.baseUrl}`).subscribe(
    (data) => {
      console.log(data);
      this.toastr.success('Kupon başarılı oluşturuldu', 'Oluşturma Onay');
    },
    (error) => {
      console.log(error);
      this.toastr.error('Kupon oluşturulurken beklenmedik hata!', 'Oluşturma Hatası');
    }
  );
}
  // Kullanıcı girişi
  login(loginRequest: LoginRequestDto) {
    this.http.post(`${this.apiUrl}/Login`, loginRequest).subscribe(
      (data) => {
        console.log(data);
        this.toastr.success('Kupon başarılı oluşturuldu', 'Oluşturma Onay');
      },
      (error) => {
        console.log(error);
        this.toastr.error('Kupon oluşturulurken beklenmedik hata!', 'Oluşturma Hatası');
      }
    );
  }

  // Kullanıcı kaydı
  register(registrationRequest: RegistrationRequestDto) {
    this.http.post(`${this.apiUrl}/register`, registrationRequest).subscribe(
      (data) => {
        console.log(data);
        this.toastr.success('Kupon başarılı oluşturuldu', 'Oluşturma Onay');
      },
      (error) => {
        console.log(error);
        this.toastr.error('Kupon oluşturulurken beklenmedik hata!', 'Oluşturma Hatası');
      }
    );
  }

  // Kullanıcı rol atama
  assignRole(assignRoleRequest: AssignRoleRequestDto) {
    return this.http.post(`${this.apiUrl}/assignRole`, assignRoleRequest).subscribe(
      (data) => {
        console.log(data);
        this.toastr.success('Kupon başarılı oluşturuldu', 'Oluşturma Onay');
      },
      (error) => {
        console.log(error);
        this.toastr.error('Kupon oluşturulurken beklenmedik hata!', 'Oluşturma Hatası');
      }
    );
  }

  isUserLoggedIn() {
    return this.isAuthenticated;
  }

}
