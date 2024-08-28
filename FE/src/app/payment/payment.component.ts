import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TransactionComponent } from "../transaction/transaction.component";

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [CommonModule, FormsModule, TransactionComponent],
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent {
  paymentMethod: string = '';
  donationAmount: number = 0;
  isSuccessful: boolean = false;
  donationTime: string = '';
  campaignCode: string | null = '';
  transactions: Array<{
    DonateId: number;
    userId: string;
    CampaignCode: number;
    DisplayName: string;
    DateDonated: string;
    PaymentMethod: string;
    MoneyDonated: string;
  }> = [];
  donateIdCounter: number = 1;

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.campaignCode = params.get('campaignCode');
    });
  }

  onDonate() {
    this.isSuccessful = true;
    this.donationTime = new Date().toLocaleString();

    const userId = 'user123';
    const displayName = 'Nguyen Van A';

    const newTransaction = {
      DonateId: this.donateIdCounter++,
      userId,
      CampaignCode: parseInt(this.campaignCode!, 10),
      DisplayName: displayName,
      DateDonated: this.donationTime,
      PaymentMethod: this.paymentMethod,
      MoneyDonated: `${this.donationAmount} VND`
    };

    // Thêm giao dịch vào danh sách
    this.transactions.push(newTransaction);

    // Điều hướng đến trang /transaction và truyền thông tin giao dịch
    this.router.navigate(['/transaction'], {
      state: { transactions: this.transactions }
    });
  }
}
