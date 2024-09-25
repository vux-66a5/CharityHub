import { HttpClient, HttpHeaders } from '@angular/common/http';
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


  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}/api/User/Login`, {
      username: request.userName,
      password: request.password
    });
  }

  // loginWithFacebook(credentials: string): Observable<any> {
  //   // Thực hiện chuyển hướng người dùng tới API Facebook login
  //   const header = new HttpHeaders().set('Content-type', 'application/json');
  //   // window.location.href = `${environment.apiBaseUrl}/api/User/login-facebook`;
  //   return this.http.post(environment.apiBaseUrl + "LoginWithFacebook", JSON.stringify(credentials), {headers: header, withCredentials: true});
  // }
  
  loginWithFacebook(facebookAccessToken: string): Observable<any> {
    const url = 'https://localhost:7244/api/User/LoginWithFacebook';
    return this.http.post<any>(url, { credential: facebookAccessToken });
  }
  
  googleLogin(idToken: string) {
    return this.http.post<{ jwtToken: string, id: string, userName: string, role: string}>(`${environment.apiBaseUrl}/api/User/google-login`, {
      idToken:idToken,
    })
  }

  facebookLogin(authToken: string) {
    return this.http.post<{ jwtToken: string, id: string, userName: string, role: string}>(`${environment.apiBaseUrl}/api/User/facebook-login`, {
      authToken:authToken,
    })
  }
  
  loginFacebook(): Observable<any> {
    return this.http.get(`${environment.apiBaseUrl}/api/User/login-facebook`, { withCredentials: true });
  }

  setUser(user: User): void {
    this.$user.next(user);
    localStorage.setItem('user-id', user.id);
    localStorage.setItem('user-name', user.userName);
    localStorage.setItem('user-role', user.role);
  }

  user() : Observable<User | undefined> {
    return this.$user.asObservable();
  }

  getUser(): User | undefined {
    const username = localStorage.getItem('user-name');
    const role = localStorage.getItem('user-role');
    const iduser = localStorage.getItem('user-id');

    if (username && role && iduser) {
      const user: User = {
        id: iduser,
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