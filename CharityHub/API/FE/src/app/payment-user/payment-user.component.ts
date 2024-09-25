import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PaypalService } from '../paypalServices/paypal.service';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './payment-user.component.html',
  styleUrls: ['./payment-user.component.css']
})
export class PaymentUserComponent {
  paymentMethod: string = '';
  donationAmount: number = 0;
  isSuccessful: boolean = false;
  donationTime: string = '';
  campaignCode: string | null = '';
  userId: string | null = '';

  constructor(private route: ActivatedRoute, private paypalService: PaypalService, private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.campaignCode = params.get('campaignCode');
      this.userId = params.get('userId');
    });

    this.route.queryParams.subscribe(params => {
      const donationId = params['donation_id'];
      const payerId = params['PayerID'];

      if (donationId && payerId) {
        this.paypalService.executeUserPayment(donationId, payerId).subscribe(response => {
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
    if (this.userId) {
      const donationRequest = {
        CampaignCode: this.campaignCode,
        Amount: this.donationAmount,
        PaymentMethod: this.paymentMethod
      };
      
      this.paypalService.createUserPayment(this.userId, donationRequest).subscribe(response => {
        if (response && response.paymentUrl) {
          window.location.href = response.paymentUrl; // Chuyển hướng tới PayPal
        } else {
          console.error(response);
        }
      }, error => {
        console.error('Error creating PayPal payment:', error);
      });
    } else {
      console.error('User ID is missing.');
    }
  }
  
  onVnPayDonate() {
    if (this.userId) {
      const donationRequest = {
        CampaignCode: this.campaignCode,
        Amount: this.donationAmount,
        PaymentMethod: this.paymentMethod
      };
      
      this.paypalService.createUserVnPayPayment(this.userId, donationRequest).subscribe(response => {
        if (response && response.paymentUrl) {
          window.location.href = response.paymentUrl; // Chuyển hướng tới VnPay
        } else {
          console.error(response);
        }
      }, error => {
        console.error('Error creating PayPal payment:', error);
      });
    } else {
      console.error('User ID is missing.');
    }
  }
}
