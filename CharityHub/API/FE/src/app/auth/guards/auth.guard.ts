import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../services/auth.service';
import jwt_decode from 'jwt-decode';

export const authGuard: CanActivateFn = (route, state) => {
  const cookieService = inject(CookieService);
  const authService = inject(AuthService);
  const router = inject(Router);
  const user = authService.getUser();

  let jwtToken = cookieService.get('Authorization');

  if (jwtToken && user) {
    jwtToken = jwtToken.replace('Bearer ', '')
    const decodedToken: any = jwt_decode(jwtToken);

    const expirationDate = decodedToken.exp * 1000;
    const currentTime = new Date().getTime();

    if (expirationDate < currentTime) {
      authService.logout();
      return router.createUrlTree(['/Login'], { queryParams: { returnUrl: state.url } })
    } else {

      if (user.role.includes('Admin')) {
        return true;
      } else {
        alert('Unauthorized');
        return false;
      }
    }
  } else {
    authService.logout();
    return router.createUrlTree(['/Login'], { queryParams: { returnUrl: state.url } })
  }
};
