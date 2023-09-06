import { Injectable } from '@angular/core';
import jwt_decode from 'jwt-decode';
import { AuthService } from './auth.service';
import { ObjectUtil } from '../common/Extension';
import { TokenInfo } from '../common/TokenInfo';

@Injectable({
  providedIn: 'root'
})
export class JwtdecoderService {

  constructor(private authService: AuthService) { }

  getTokenInfo(): TokenInfo {
    // Token'i çözümle
    var info = new TokenInfo();
    const token = this.authService.getToken();
    if (ObjectUtil.isNullOrUndefinedOrEmpty) {
      const decodedToken: any = jwt_decode(token);
      // Örnek claimlere erişim
      info.email = decodedToken.email;
      info.userId = decodedToken.sub;
      info.userName = decodedToken.name;
      info.roles = decodedToken.role; // Eğer rolleri almak istiyorsanız
    }
    return info;
  }
}
