import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Company } from './Company';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUrl = "https://localhost:7266/api/Organization"
  constructor(private http: HttpClient) {}

  get(): Observable<any>{
    return this.http.get(this.apiUrl);
  }

  getById(id: number):Observable<any>{
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  addOrg(company: Company):Observable<any>{
    return this.http.post<any>(`${this.apiUrl}`,company);
  }

  updateOrg(id:number,company : Company):Observable<any>{
    return this.http.put<any>(`${this.apiUrl}/${id}`,company);
  }

  deleteOrg(id: number):Observable<any>{
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
