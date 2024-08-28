import { CommonModule, CurrencyPipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';


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

  ngOnInit(): void {
    // Dữ liệu giả lập cho top donate (có thể có nhiều hơn 10 người)
    const allTopDonations = [
      { name: 'Nguyen Van A', amount: 1000000 },
      { name: 'Tran Thi B', amount: 950000 },
      { name: 'Le Van C', amount: 900000 },
      { name: 'Pham Thi D', amount: 850000 },
      { name: 'Hoang Van E', amount: 800000 },
      { name: 'Do Thi F', amount: 750000 },
      { name: 'Nguyen Van G', amount: 700000 },
      { name: 'Tran Thi H', amount: 650000 },
      { name: 'Le Van I', amount: 600000 },
      { name: 'Pham Thi K', amount: 550000 },
      { name: 'Nguyen Van L', amount: 500000 }, // Người thứ 11
      { name: 'Tran Thi M', amount: 450000 }   // Người thứ 12
    ];

    // Dữ liệu giả lập cho recent donate (có thể có nhiều hơn 10 người)
    const allRecentDonations = [
      { name: 'Nguyen Van L', amount: 500000 },
      { name: 'Tran Thi M', amount: 450000 },
      { name: 'Le Van N', amount: 400000 },
      { name: 'Pham Thi O', amount: 350000 },
      { name: 'Hoang Van P', amount: 300000 },
      { name: 'Do Thi Q', amount: 250000 },
      { name: 'Nguyen Van R', amount: 200000 },
      { name: 'Tran Thi S', amount: 150000 },
      { name: 'Le Van T', amount: 100000 },
      { name: 'Pham Thi U', amount: 50000 },
      { name: 'Nguyen Van X', amount: 100000 }, // Người thứ 11
      { name: 'Tran Thi Y', amount: 50000 }    // Người thứ 12
    ];

    // Chỉ lấy tối đa 10 người từ mỗi danh sách
    this.topDonations = allTopDonations.slice(0, 10);
    this.recentDonations = allRecentDonations.slice(0, 10);
  }

  constructor(private router: Router) {}

  navigateToPayment() {
    this.router.navigate(['/payment', this.card.campaignCode]).then(() => {
      location.reload();
    });
  }
}
