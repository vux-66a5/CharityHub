import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PaypalService } from '../paypalServices/paypal.service';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent {
  paymentMethod: string = '';
  donationAmount: number = 0;
  isSuccessful: boolean = false;
  donationTime: string = '';
  campaignCode: string | null = '';

  constructor(private route: ActivatedRoute, private paypalService: PaypalService, private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.campaignCode = params.get('campaignCode'); // Lấy giá trị từ route
    });

    this.route.queryParams.subscribe(params => {
      const donationId = params['donation_id'];
      const payerId = params['PayerID'];

      if (donationId && payerId) {
        this.paypalService.executePayment(donationId, payerId).subscribe(response => {
          if (response) {
            this.isSuccessful = true;
            this.donationTime = new Date().toLocaleString();
            this.router.navigate(['http://localhost:4200/donation-list']);
          } else {
            console.error('Payment execution failed.');
          }
        }, error => {
          console.error('Error executing payment:', error);
        });
      }
    });
  }

  onDonate() {
    const donationRequest = {
      CampaignCode: this.campaignCode,
      Amount: this.donationAmount,
      PaymentMethod: this.paymentMethod
    };

    this.paypalService.createPayment(donationRequest).subscribe(response => {
      if (response && response.paymentUrl) {
        window.location.href = response.paymentUrl; // Chuyển hướng tới PayPal
      } else {
        console.error(response);
      }
    }, error => {
      console.error('Error creating PayPal payment:', error);
    });
  }
}
