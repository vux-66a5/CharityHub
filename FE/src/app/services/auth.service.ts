import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn = new BehaviorSubject<boolean>(false);
  private user = new BehaviorSubject<any>(null);
  private role = new BehaviorSubject<string>('user'); // Thêm dòng này

  isLoggedIn$ = this.loggedIn.asObservable();
  user$ = this.user.asObservable();
  role$ = this.role.asObservable(); // Thêm dòng này

  login(user: any, role: string) { // Thêm tham số role
    this.loggedIn.next(true);
    this.user.next(user);
    this.role.next(role); // Cập nhật role
  }

  logout() {
    this.loggedIn.next(false);
    this.user.next(null);
    this.role.next('user'); // Đặt lại role mặc định
  }
}
