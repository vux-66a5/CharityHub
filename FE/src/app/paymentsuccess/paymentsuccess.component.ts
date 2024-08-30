import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
declare var window: any;

@Component({
  selector: 'app-payment-success',
  templateUrl: './paymentsuccess.component.html',
  styleUrls: ['./paymentsuccess.component.css']
})


export class PaymentsuccessComponent implements OnInit {
  donationId: string | null = null;
  amount: string | null = null;
  campaignCode: number | null = null; // Add property for campaign code

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient // Inject HttpClient
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.donationId = params['donation_id'] || 'Không có thông tin';
      this.amount = params['amount'] ? `${params['amount']} VND` : 'Không có thông tin';

      // Fetch campaign code based on donationId
      if (this.donationId) {
        this.fetchCampaignCode(this.donationId);
      }

      const modal = new window.bootstrap.Modal(document.getElementById('paymentSuccessModal'));
      modal.show();
    });
  }

  private fetchCampaignCode(donationId: string): void {
    const apiUrl = `https://localhost:7244/api/ViewDonationAndCampaign/GetCampaignCodeByDonationId/${donationId}`;

    this.http.get<any>(apiUrl).subscribe({
      next: (response) => {
        console.log(response.campaignCode);
        this.campaignCode = response.campaignCode || 'Không có thông tin';
      },
      error: (error) => {
        console.error('Error fetching campaign code:', error);
        this.campaignCode = -1;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/']).then(() => {
      window.location.reload(); // Tải lại trang sau khi điều hướng
    });
  }
}