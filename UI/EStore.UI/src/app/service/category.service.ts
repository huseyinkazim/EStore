import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpMethod } from '../common/HttpMethod';
import { Observable, catchError, throwError } from 'rxjs';
import { Category } from '../common/Category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private categoryUrl = 'http://localhost:5083/Categories';

  constructor(private apiService: ApiService) { }
  private handleError(error: any) {
    console.error(error);
    return throwError(error);
  }
  getCategories(): Observable<Category[]> {
    return this.apiService.sendRequest<Category[]>(this.categoryUrl, HttpMethod.GET).pipe(
      catchError(this.handleError)
    );
  }
}
