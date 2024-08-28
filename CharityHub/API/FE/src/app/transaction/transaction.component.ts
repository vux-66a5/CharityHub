import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-transaction',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './transaction.component.html',
  styleUrl: './transaction.component.css'
})
export class TransactionComponent {
  transactions: Array<{
    DonateId: number;
    userId: string;
    CampaignCode: number;
    DisplayName: string;
    DateDonated: string;
    PaymentMethod: string;
    MoneyDonated: string;
  }> = [];

  currentPage: number = 1;
  itemsPerPage: number = 10;
  pagedTransactions: Array<{
    DonateId: number;
    userId: string;
    CampaignCode: number;
    DisplayName: string;
    DateDonated: string;
    PaymentMethod: string;
    MoneyDonated: string;
  }> = [];

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.transactions = navigation?.extras.state?.['transactions'] || [];
    this.updatePagedTransactions();
  }

  updatePagedTransactions() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.pagedTransactions = this.transactions.slice(startIndex, endIndex);
  }

  nextPage() {
    if (this.currentPage * this.itemsPerPage < this.transactions.length) {
      this.currentPage++;
      this.updatePagedTransactions();
    }
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePagedTransactions();
    }
  }
}
