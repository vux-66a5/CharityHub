import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';


declare var bootstrap: any;
@Component({
  selector: 'app-card-detail',
  standalone: true,
  imports: [],
  templateUrl: './card-detail.component.html',
  styleUrl: './card-detail.component.css'
})
export class CardDetailComponent {
  @Input() card: any;
  @Input() modalId!: string;

  constructor(private router: Router) {}

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
