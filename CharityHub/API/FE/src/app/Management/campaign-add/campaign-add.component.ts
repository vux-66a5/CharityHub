import { Component, OnDestroy } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { Campaign } from '../Models/campaign.model';
import { CampaignService } from '../services/campaign.service';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-campaign-add',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './campaign-add.component.html',
  styleUrl: './campaign-add.component.css'
})
export class CampaignAddComponent implements OnDestroy{
  campaign: Campaign;
  private addCampaignSubscribtion?: Subscription;

  constructor(public dialogRef: MatDialogRef<CampaignAddComponent>, private addCampaign: CampaignService) {
    this.campaign = {
      campaignTitle: '',
      campaignThumbnail: '',
      campaignDescription: '',
      targetAmount: 0,
      partnerName: '',
      partnerLogo: '',
      partnerNumber: ''
    }
  }

  onSave() {
    this.addCampaignSubscribtion = this.addCampaign.addCampaign(this.campaign)
    .subscribe({
      next: (response) => {
        console.log('This was successful!')
      }
    });
    window.location.reload();
    this.dialogRef.close(this.campaign);
  }

  onClose() {
    this.dialogRef.close();
  }

  ngOnDestroy(): void {
      this.addCampaignSubscribtion?.unsubscribe();
  }
}
