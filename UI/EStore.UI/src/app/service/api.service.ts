import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpMethod } from '../common/HttpMethod';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private tokenKey = 'authToken';
  constructor(private http: HttpClient,
    private cookieService: CookieService) { }

  sendRequest<T = any>(apiUrl: string, method: HttpMethod, body?: any): Observable<T> {
    let token = this.cookieService.get(this.tokenKey);

    let response: Observable<T>;
    let headers: HttpHeaders;
    if (token) {
      headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`
      });
    } else {
      headers = new HttpHeaders({});
    }
    switch (method) {
      case HttpMethod.GET:
        response = this.http.get<T>(`${apiUrl}`, { headers });
        break;
      case HttpMethod.POST:
        response = this.http.post<T>(`${apiUrl}`, body, { headers });
        break;
      case HttpMethod.PUT:
        response = this.http.put<T>(`${apiUrl}`, body, { headers });
        break;
      case HttpMethod.DELETE:
        response = this.http.delete<T>(`${apiUrl}`, { headers });
        break;
      default:
        throw new Error('Unsupported HTTP method');
    }

    return response;
  }
}
