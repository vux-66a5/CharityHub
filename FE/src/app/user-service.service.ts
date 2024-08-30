import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EditPass, EditProfile, Profile } from './Management/Models/campaign.model';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

  constructor(private http: HttpClient) { }

  editProfileById(id: string, profile: EditProfile): Observable<Profile> {
    return this.http.put<Profile>(`${environment.apiBaseUrl}/api/User/Update-Profile/${id}?addAuth=true`, profile);
  }

  getProfile(id: string): Observable<Profile> {
    return this.http.get<Profile>(`${environment.apiBaseUrl}/api/User/Get-Profile/${id}?addAuth=true`);
  }

  editPassword(editPass: EditPass): Observable<any> {
    return this.http.put<any>(`${environment.apiBaseUrl}/api/User/Change-Password?addAuth=true`, editPass);
  }
  
}
