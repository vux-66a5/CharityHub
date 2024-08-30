import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../Models/campaign.model';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getViewUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${environment.apiBaseUrl}/api/Admin/Get-All-Users?addAuth=true`);
  }

  editActive(userId: string, isActive: boolean): Observable<User> {
    return this.http.put<User>(`${environment.apiBaseUrl}/api/Admin/Activate-User/${userId}?addAuth=true`, isActive);
  }

  // searchUsers(query?: string) : Observable<User[]> {
  //   let params = new HttpParams();

  //   if (query) {
  //     params = params.set('query', query);
  //   }

  //   return this.http.get<User[]>(`${environment.apiBaseUrl}/api/Admin/Search-User?addAuth=true`, {params: params});
  // }

  searchUsers(emailOrPhone?: string): Observable<User[]> {
    let params = new HttpParams();

    if (emailOrPhone) {
      params = params.set('query', emailOrPhone);
    }

    return this.http.get<User[]>(`${environment.apiBaseUrl}/api/Admin/Search-User?addAuth=true`, { params: params });
  }
}
