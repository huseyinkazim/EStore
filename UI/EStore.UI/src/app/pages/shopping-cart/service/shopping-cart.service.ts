import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {
  private apiUrl = 'http://localhost:5239/api/ShoppingCart';

  constructor(private http: HttpClient) {}

  // Sepete ürün eklemek için API isteği
  addToCart(cartData: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/CartInsert`, cartData);
  }

  // Sepeti güncellemek için API isteği
  updateCart(cartData: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/CartUpdate`, cartData);
  }

  // Sepetten ürün silmek için API isteği
  removeFromCart(cartDetailsId: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/RemoveCart/${cartDetailsId}`);
  }

  // Sepeti getirmek için API isteği
  getCart(userId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/GetCart/${userId}`);
  }
}
