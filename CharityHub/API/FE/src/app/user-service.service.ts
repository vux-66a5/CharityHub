import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Profile } from './Management/Models/campaign.model';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

  constructor(private http: HttpClient) { }

  editProfileById(id: string, profile: Profile): Observable<Profile> {
    return this.http.put<Profile>(`${environment.apiBaseUrl}/api/User/Update-Profile/${id}?addAuth=true`, profile);
  }
}
