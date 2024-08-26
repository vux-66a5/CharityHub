import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn = new BehaviorSubject<boolean>(false);
  private user = new BehaviorSubject<any>(null);

  isLoggedIn$ = this.loggedIn.asObservable();
  user$ = this.user.asObservable();

  login(user: any) {
    this.loggedIn.next(true);
    this.user.next(user);
  }

  logout() {
    this.loggedIn.next(false);
    this.user.next(null);
  }
}
