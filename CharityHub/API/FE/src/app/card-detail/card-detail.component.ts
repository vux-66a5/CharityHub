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
    this.router.navigate(['/payment', this.card.campaignCode]);
  }
}
