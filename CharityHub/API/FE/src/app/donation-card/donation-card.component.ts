import { Component, Input } from '@angular/core';
import { CardDetailComponent } from "../card-detail/card-detail.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-donation-card',
  standalone: true,
  imports: [CardDetailComponent, CommonModule],
  templateUrl: './donation-card.component.html',
  styleUrls: ['./donation-card.component.css']
})
export class DonationCardComponent {
  @Input() card: any;

  get formattedCurrentAmount() {
    return new Intl.NumberFormat().format(this.card.currentAmount);
  }

  get formattedTargetAmount() {
    return new Intl.NumberFormat().format(this.card.targetAmount);
  }

  get formattedStatus() {
    switch (this.card.cardStatus) {
      case 'inprogress':
        return 'Quyên góp';
      case 'close':
        return 'Đạt Mục Tiêu';
      default:
        return 'Status Unknown';
    }
  }

  get progressBarWidth() {
    return `${this.card.achievedPercentage}%`;
  }



  ngOnInit(): void {
    console.log(this.card); // Inspect the card object
  }
}
