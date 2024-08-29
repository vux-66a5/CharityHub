import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { Campaign } from '../Models/campaign.model';

@Component({
  selector: 'app-campaign-add',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './campaign-add.component.html',
  styleUrl: './campaign-add.component.css'
})
export class CampaignAddComponent {
  campaign: Campaign = {
    campaignId: '',
    campaignCode: '',
    campaignTitle: '',
    campaignThumbnail: '',
    campaignDescription: '',
    targetAmount: 0,
    currentAmount: 0,
    startDate: '',
    endDate: '',
    partnerName: '',
    partnerLogo: '',
    campaignStatus: 'New', // Default status
    partnerNumber: ''
  };

  constructor(public dialogRef: MatDialogRef<CampaignAddComponent>) { }

  onSave() {
    this.campaign.campaignStatus = 'New'; // Set status to 'New'
    this.dialogRef.close(this.campaign);
  }

  onClose() {
    this.dialogRef.close();
  }
}
