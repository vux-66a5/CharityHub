import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PaypalService {
  private baseUrl = 'https://localhost:7244/api/NoUserDonation';
  private baseUrlUser = 'https://localhost:7244/api/UserDonation';

  constructor(private http: HttpClient) { }

  createPayment(donationRequest: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Create-PayPal-Donation`, donationRequest);
  }

  executePayment(donationId: string, payerId: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/ExecutePayment?donation_id=${donationId}&payer_id=${payerId}`);
  }

  createUserPayment(id: string, donationRequest: any): Observable<any> {
    return this.http.post(`${this.baseUrlUser}/Create-PayPal-Donation/${id}?addAuth=true`, donationRequest);
  }

  executeUserPayment(donationId: string, payerId: string): Observable<any> {
    return this.http.get(`${this.baseUrlUser}/ExecutePayment?donation_id=${donationId}&payer_id=${payerId}`);
  }

  createVnPayPayment(donationRequest: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Create-VnPay-Donation`, donationRequest);
  }

  createUserVnPayPayment(id: string, donationRequest: any): Observable<any> {
    return this.http.post(`${this.baseUrlUser}/Create-VnPay-Donation/${id}?addAuth=true`, donationRequest);
  }
}
