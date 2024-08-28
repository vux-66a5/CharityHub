import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent {
  // @Input() campaignCode!: number;
  paymentMethod: string = '';
  donationAmount: number = 0;
  isSuccessful: boolean = false;
  donationTime: string = '';

  campaignCode: string | null = '';

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.campaignCode = params.get('campaignCode'); // Lấy giá trị từ route
    });
  }

  onDonate() {
    this.isSuccessful = true;
    this.donationTime = new Date().toLocaleString();
  }
}
