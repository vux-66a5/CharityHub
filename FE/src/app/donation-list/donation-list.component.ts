import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DonationCardComponent } from "../donation-card/donation-card.component";
import { CommonModule } from '@angular/common';

interface Card {
  cardId: number;
  cardImage: string;
  cardTitle: string;
  cardPartner: string;
  cardDayLeft: number;
  currentAmount: number;
  targetAmount: number;
  contributionCount: number;
  achievedPercentage: number;
  cardDetail: string;
  campaignCode: number;
  cardStatus: string;
}

@Component({
  selector: 'app-donation-list',
  standalone: true,
  imports: [DonationCardComponent, CommonModule],
  templateUrl: './donation-list.component.html',
  styleUrl: './donation-list.component.css'
})
export class DonationListComponent implements OnInit {
  cards: Card[] = [];
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.fetchCards();
  }

  fetchCards(): void {
    const apiUrl = 'https://localhost:7244/api/ViewDonationAndCampaign/GetAllCampaignsExceptNew';

    this.http.get<any[]>(apiUrl).subscribe(apiCards => {
      this.cards = apiCards.map(apiCard => this.transformApiCard(apiCard));
    });
  }

  private transformApiCard(apiCard: any): Card {
    return {
      cardId: apiCard.campaignCode,
      cardImage: apiCard.campaignThumbnail,
      cardTitle: apiCard.campaignTitle,
      cardPartner: apiCard.partnerName,
      cardStatus: apiCard.campaignStatus || 'Pending',
      cardDayLeft: this.calculateDaysLeft(apiCard.endDate),
      currentAmount: apiCard.currentAmount,
      targetAmount: apiCard.targetAmount,
      contributionCount: apiCard.confirmedDonationCount,
      achievedPercentage: parseFloat(((apiCard.currentAmount / apiCard.targetAmount) * 100).toFixed(2)),
      cardDetail: apiCard.campaignDescription,
      campaignCode: apiCard.campaignCode

    };
  }

  private calculateDaysLeft(endDate: string): number {
    const start = new Date();
    const end = new Date(endDate);
    if (end.getTime() - start.getTime() <= 0) {
      return 0;
    }
    const diffTime = Math.abs(end.getTime() - start.getTime());
    return Math.ceil(diffTime / (1000 * 60 * 60 * 24));
  }
}