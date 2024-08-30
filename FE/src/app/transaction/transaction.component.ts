import { CommonModule } from '@angular/common';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { MyDonated } from '../Management/Models/campaign.model';
import { TransactionService } from '../transaction-services/transaction.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-transaction',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './transaction.component.html',
  styleUrl: './transaction.component.css'
})
export class TransactionComponent implements OnInit, OnDestroy {
  errorMessage: string | null = null;
  currentPage: number = 1;
  itemsPerPage: number = 10;
  donations: MyDonated[] = [];
  id: string | null = null;
  paramsSubscription?: Subscription;

  constructor(private router: Router, private transactionService: TransactionService, private activateRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.paramsSubscription = this.activateRoute.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if (this.id) {
          this.transactionService.getMyDonated(this.id)
            .subscribe({
            next: (respone) => {
            this.donations = respone;
        }
      })
        }
      }
    })
    

    const navigation = this.router.getCurrentNavigation();
    this.donations = navigation?.extras.state?.['transactions'] || [];
    this.updatePagedTransactions();
  }

  updatePagedTransactions() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.donations = this.donations.slice(startIndex, endIndex);
  }

  nextPage() {
    if (this.currentPage * this.itemsPerPage < this.donations.length) {
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

  ngOnDestroy(): void {
      this.paramsSubscription?.unsubscribe();
  }
}
