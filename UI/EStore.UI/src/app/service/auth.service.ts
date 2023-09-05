import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, takeUntil } from 'rxjs';
import { LoginRequestDto } from '../common/LoginRequestDto';
import { LoginResponseDto } from '../common/LoginResponseDto';
import { HttpMethod } from '../common/HttpMethod';
import { RegistrationRequestDto } from '../common/RegistrationRequestDto';
import { UserDto } from '../common/UserDto';
import { AssignRoleRequestDto } from '../common/AssignRoleRequestDto';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { ApiService } from './api.service';
import { ResponseDto } from '../common/responsedto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5257/api/Auth'; // API URL'sini buraya ekleyin
  private _unsubscribe$: Subject<void> = new Subject<void>();
  private isAuthenticated: boolean = false;
  private tokenKey = 'authToken'; // Çerez anahtarı (key)

  constructor(private http: HttpClient,
    private toastr: ToastrService,
    private router: Router,
    private cookieService: CookieService,
    private apiService: ApiService) { }
  isNullOrUndefined(obj: any) {
    return typeof obj === "undefined" || obj === null;
  }
  // Kullanıcı girişi
  login(loginRequest: LoginRequestDto) {
    this.apiService.sendRequest<ResponseDto>(`${this.apiUrl}/Login`, HttpMethod.POST, loginRequest)
      .subscribe((data) => {
        if (this.isNullOrUndefined(data) || !data.isSuccess) {
          this.toastr.warning('Kullanıcı adı ya da şifreyi yanlış girdiniz.', 'Login Başarsız');
        }
        else {
          var response = data.result as LoginResponseDto;
          this.toastr.success('Login işleminiz başarılı anasayfaya yönlendirileceksiniz.', 'Login Başarılı');
          this.router.navigate(['/coupons']);

        }

      },
        (error: any) => {
          console.log(error);
          this.toastr.error(error.error.message, 'Login Hatası');
        }
      );
  }

  // Kullanıcı kaydı
  register(registrationRequest: RegistrationRequestDto) {
    this.apiService.sendRequest(`${this.apiUrl}/register`, HttpMethod.POST, registrationRequest)
      .subscribe(
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
    this.apiService.sendRequest(`${this.apiUrl}/assignRole`, HttpMethod.POST, assignRoleRequest)
      .subscribe(
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
  // Token'ı çereze kaydetme işlemi
  saveToken(token: string): void {
    this.cookieService.set(this.tokenKey, token);
  }

  // Kaydedilmiş token'ı çerezden alma işlemi
  getToken(): string | null {
    return this.cookieService.get(this.tokenKey) || null;
  }

  // Token'ı çerezden silme işlemi
  removeToken(): void {
    this.cookieService.delete(this.tokenKey);
  }
}
