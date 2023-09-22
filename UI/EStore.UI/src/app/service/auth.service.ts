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
import { ObjectUtil } from '../common/Extension';
import { TokenInfo } from '../common/TokenInfo';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5257/api/Auth'; // API URL'sini buraya ekleyin
  private _unsubscribe$: Subject<void> = new Subject<void>();
  private tokenKey = 'authToken'; // Çerez anahtarı (key)

  constructor(private http: HttpClient,
    private toastr: ToastrService,
    private router: Router,
    private cookieService: CookieService,
    private apiService: ApiService) { }

  // Kullanıcı girişi
  login(loginRequest: LoginRequestDto) {
    this.apiService.sendRequest<ResponseDto>(`${this.apiUrl}/Login`, HttpMethod.POST, loginRequest)
      .subscribe((data) => {
        if (ObjectUtil.isNullOrUndefined(data) || !data.isSuccess) {
          this.toastr.warning('Kullanıcı adı ya da şifreyi yanlış girdiniz.', 'Login Başarsız');
        }
        else {
          var response = data.result as LoginResponseDto;
          this.saveToken(response.token);
          this.toastr.success('Login işleminiz başarılı anasayfaya yönlendirileceksiniz.', 'Login Başarılı');
          this.router.navigate(['/home']);
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
    return this.apiService.sendRequest<ResponseDto>(`${this.apiUrl}/register`, HttpMethod.POST, registrationRequest)
      .subscribe(
        (data) => {
          console.log(data);
          var userDto = data.result as UserDto;
          if (!data.isSuccess)
            this.toastr.warning(data.message, 'Kayıt Uyarısı');
          else
            this.toastr.success('Kayıt işlemi başarılı', 'Kayıt Onay');
        },
        (error) => {
          console.log(error);
          this.toastr.error('Kayıt işlemi sırasında beklenmedik hata!', 'Kayıt Hatası');
        }
      );
  }

  // Kullanıcı rol atama
  assignRole(assignRoleRequest: AssignRoleRequestDto) {
    this.apiService.sendRequest(`${this.apiUrl}/assignRole`, HttpMethod.POST, assignRoleRequest)
      .subscribe(
        (data) => {
          console.log(data);
          this.toastr.success('Rol ataması başarılı', 'Rol Onay');
        },
        (error) => {
          console.log(error);
          this.toastr.error('Rol ataması sırasında beklenmedik hata!', 'Rol Hatası');
        }
      );
  }

  isUserLoggedIn() {
    if (ObjectUtil.isNullOrUndefinedOrEmpty(this.getToken()))
      return false;
    return true;;
  }
  // Token'ı çereze kaydetme işlemi
  saveToken(token: string): void {
    const expirationDate = new Date();
    expirationDate.setDate(expirationDate.getDate() + 7);
    this.cookieService.set(this.tokenKey, token, expirationDate);
  }

  // Kaydedilmiş token'ı çerezden alma işlemi
  getToken(): string | null {
    return this.cookieService.get(this.tokenKey) || null;
  }

  // Token'ı çerezden silme işlemi
  removeToken(): void {
    this.cookieService.delete(this.tokenKey);
  }
  getTokenInfo(): TokenInfo {
    // Token'i çözümle
    var info = new TokenInfo();

    if (this.isUserLoggedIn()) {
      const token = this.getToken();
      if (ObjectUtil.isNullOrUndefinedOrEmpty) {
        const decodedToken: any = jwt_decode(token);
        // Örnek claimlere erişim
        info.email = decodedToken.email;
        info.userId = decodedToken.sub;
        info.userName = decodedToken.name;
        info.roles = decodedToken.role; // Eğer rolleri almak istiyorsanız
      }
    }
    return info;
  }
}
