import { CommonModule, CurrencyPipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

declare var bootstrap: any;
@Component({
  selector: 'app-card-detail',
  standalone: true,
  imports: [CommonModule, CurrencyPipe],
  templateUrl: './card-detail.component.html',
  styleUrl: './card-detail.component.css'
})
export class CardDetailComponent implements OnInit {
  @Input() card: any;
  @Input() modalId!: string;

  topDonations: any[] = [];
  recentDonations: any[] = [];

  private apiUrl_1 = 'https://localhost:7244/api/NoUserViewCampaign/Get-Donors-By-CampaignCode';
  private apiUrl_2 = 'https://localhost:7244/api/ViewDonationAndCampaign/Get-Donations-By-Campaign-Code';
  constructor(private router: Router, private http: HttpClient) {}

  ngOnInit(): void {
    this.loadTopDonations();
    this.loadNewDonations();
  }

  loadTopDonations(): void {
    if (this.card && this.card.campaignCode) {
      this.http.get<any[]>(`${this.apiUrl_1}/${this.card.campaignCode}`).subscribe(
        (donors) => {
          // Chỉ lấy tối đa 10 người từ danh sách
          this.topDonations = donors.slice(0, 10);
        },
        (error) => {
          console.error('Error fetching donors:', error);
        }
      );
    }
  }

  loadNewDonations(): void {
    if (this.card && this.card.campaignCode) {
      this.http.get<any[]>(`${this.apiUrl_2}/${this.card.campaignCode}`).subscribe(
        (donors) => {
          // Chỉ lấy tối đa 10 người từ danh sách
          this.recentDonations = donors.slice(0, 10);
        },
        (error) => {
          console.error('Error fetching donors:', error);
        }
      );
    }
  }

  navigateToPayment() {
    const userId = localStorage.getItem('user-id');

    if (userId) {
      // Nếu có userId, chuyển hướng tới /payment-user kèm với campaignCode và userId
      this.router.navigate(['/payment-user', this.card.campaignCode, userId]).then(() => {
        // Làm mới trang sau khi điều hướng
        location.reload();
      });
    } else {
      // Nếu không có userId, chuyển hướng tới /payment kèm với campaignCode
      this.router.navigate(['/payment', this.card.campaignCode]).then(() => {
        // Làm mới trang sau khi điều hướng
        location.reload();
      });
    }
  }
}