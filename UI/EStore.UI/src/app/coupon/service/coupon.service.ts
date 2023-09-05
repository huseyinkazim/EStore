import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from 'src/app/service/api.service';
import { HttpMethod } from 'src/app/common/HttpMethod';

@Injectable({
  providedIn: 'root',
})
export class CouponService {
  private baseUrl = 'http://localhost:5108/api/Coupon'; // Replace with your API URL

  constructor(private http: HttpClient,
    private apiService: ApiService) { }

  getAll(): Observable<any> {
    return this.apiService.sendRequest(this.baseUrl, HttpMethod.GET);
  }

  getById(id: number): Observable<any> {
    return this.apiService.sendRequest(`${this.baseUrl}/${id}`, HttpMethod.GET);
  }

  getByCode(code: string): Observable<any> {
    return this.apiService.sendRequest(`${this.baseUrl}/GetByCode/${code}`, HttpMethod.GET);
  }

  create(coupon: any): Observable<any> {
    return this.apiService.sendRequest(`${this.baseUrl}`, HttpMethod.POST, coupon);
  }

  update(coupon: any): Observable<any> {
    return this.apiService.sendRequest(`${this.baseUrl}`, HttpMethod.PUT, coupon);
  }

  delete(id: number): Observable<any> {
    return this.apiService.sendRequest(`${this.baseUrl}/${id}`, HttpMethod.DELETE);
  }
}
