import { Injectable } from '@angular/core';
import { Campaign, CampaignsCard, CampaignTime, EditCampaign } from '../Models/campaign.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CampaignService {

  constructor(private http: HttpClient) { }

  addCampaign(campaign: Campaign): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/AdminCampaign/Create-Campaign?addAuth=true`, campaign)
  }

  getViewCampaignsCard(): Observable<CampaignsCard[]> {
    return this.http.get<CampaignsCard[]>(`${environment.apiBaseUrl}/api/AdminCampaign/Get-Campaigns-View-Card?addAuth=true`);
  }

  editCampaignById(campaignId: string, editCampaignRequest: EditCampaign): Observable<CampaignsCard> {
    return this.http.put<CampaignsCard>(`${environment.apiBaseUrl}/api/AdminCampaign/Update-Campaign/${campaignId}?addAuth=true`, editCampaignRequest);
  }

  deleteCampaign(campaignId: string): Observable<CampaignsCard> {
    return this.http.delete<CampaignsCard>(`${environment.apiBaseUrl}/api/AdminCampaign/Delete-Campaign/${campaignId}?addAuth=true`);
  }

  searchCampaigns(query?: string): Observable<CampaignsCard[]> {
    let params = new HttpParams();

    if (query) {
      params = params.set('query', query);
    }

    return this.http.get<CampaignsCard[]>(`${environment.apiBaseUrl}/api/AdminCampaign/Search-Campaigns-By-Code-Status-Name-Phone?addAuth=true`, { params: params });
  }

  editDateCampaign(campaignId: string, editTime: CampaignTime): Observable<CampaignsCard> {
    return this.http.put<CampaignsCard>(`${environment.apiBaseUrl}/api/AdminCampaign/Update-Start-And-End-Date/${campaignId}?addAuth=true`, editTime);
  }
}
