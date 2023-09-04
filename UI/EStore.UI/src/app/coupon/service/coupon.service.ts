import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CouponService {
  private baseUrl = 'http://localhost:5108/api/Coupon'; // Replace with your API URL

  constructor(private http: HttpClient) {}

  getAll(): Observable<any> {
    return this.http.get(`${this.baseUrl}`);
  }

  getById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`);
  }

  getByCode(code: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetByCode/${code}`);
  }

  create(coupon: any): Observable<any> {
    return this.http.post(`${this.baseUrl}`, coupon);
  }

  update(coupon: any): Observable<any> {
    return this.http.put(`${this.baseUrl}`, coupon);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
