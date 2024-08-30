import { InjectionToken } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

export const COOKIE_SERVICE_TOKEN = new InjectionToken<CookieService>('CookieService');
