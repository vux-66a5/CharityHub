import { Component, Input } from '@angular/core';
import { CardDetailComponent } from "../card-detail/card-detail.component";

@Component({
  selector: 'app-donation-card',
  standalone: true,
  imports: [CardDetailComponent],
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

  get progressBarWidth() {
    return `${this.card.achievedPercentage}%`;
  }
}
