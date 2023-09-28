import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ProductDto } from 'src/app/common/ProductDto';
import { ApiService } from 'src/app/service/api.service';
import { HttpMethod } from 'src/app/common/HttpMethod';
import { ResponseDto } from 'src/app/common/responsedto';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = 'http://localhost:5083/api/Product';

  constructor(private apiService: ApiService) { }

  private handleError(error: any) {
    console.error(error);
    return throwError(error);
  }

  getProducts(): Observable<ResponseDto> {
    return this.apiService.sendRequest<ResponseDto>(this.baseUrl, HttpMethod.GET).pipe(
      catchError(this.handleError)
    );
  }

  getProductById(id: number): Observable<ResponseDto> {
    const url = `${this.baseUrl}/${id}`;

    return this.apiService.sendRequest<ResponseDto>(url, HttpMethod.GET).pipe(
      catchError(this.handleError)
    );
  }

  createProduct(product: ProductDto): Observable<ResponseDto> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.apiService.sendRequest<ResponseDto>(this.baseUrl, HttpMethod.POST, product, headers).pipe(
      catchError(this.handleError)
    );
  }

  updateProduct(product: ProductDto): Observable<ResponseDto> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.apiService.sendRequest<ResponseDto>(this.baseUrl, HttpMethod.PUT, product, headers).pipe(
      catchError(this.handleError)
    );
  }

  deleteProduct(id: number): Observable<void> {
    const url = `${this.baseUrl}/${id}`;
    return this.apiService.sendRequest<void>(url, HttpMethod.DELETE).pipe(
      catchError(this.handleError)
      );
  }
}
