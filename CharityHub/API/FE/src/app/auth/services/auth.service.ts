import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { LoginRequest } from '../models/login-request.model';
import { LoginResponse } from '../models/login-response.model';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  $user = new BehaviorSubject<User | undefined>(undefined);

  constructor(private http: HttpClient, private cookieService: CookieService) {
  }

  // login(user: any) {
  //   this.loggedIn.next(true);
  //   this.user.next(user);
  // }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}/api/User/Login`, {
      username: request.userName,
      password: request.password
    });
  }

  setUser(user: User): void {
    this.$user.next(user);
    localStorage.setItem('user-name', user.userName);
    localStorage.setItem('user-role', user.role);
  }

  user() : Observable<User | undefined> {
    return this.$user.asObservable();
  }

  getUser(): User | undefined {
    const username = localStorage.getItem('user-name');
    const role = localStorage.getItem('user-role');

    if (username && role) {
      const user: User = {
        userName: username,
        role: role
      }

      return user;
    }

    return undefined;
  }

  logout(): void {
    localStorage.clear();
    this.cookieService.delete('Authorization', '/');
    this.$user.next(undefined);
  }
}
