import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MyDonated } from '../Management/Models/campaign.model';
import { environment } from '../../environments/environment';
import { AuthService } from '../auth/services/auth.service';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  constructor(private http: HttpClient, private authService: AuthService, private cookieService: CookieService) { }

  getMyDonated(id?: string): Observable<MyDonated[]> {
    return this.http.get<MyDonated[]>(`${environment.apiBaseUrl}/api/UserViewCampaign/Get-User-Donations/${id}`, {
      headers: {
        'Authorization': this.cookieService.get('Authorization')
      }
    });
  }
}
