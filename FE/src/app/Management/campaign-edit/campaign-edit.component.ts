import { Component, Inject, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogContent, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { Campaign } from '../../Models/campaign.model';
import { MatFormField, MatLabel } from '@angular/material/form-field';

@Component({
  selector: 'app-campaign-edit',
  standalone: true,
  imports: [FormsModule, MatDialogModule, MatFormField, MatLabel],
  templateUrl: './campaign-edit.component.html',
  styleUrl: './campaign-edit.component.css'
})
export class CampaignEditComponent {
  campaign: Campaign;

  constructor(
    public dialogRef: MatDialogRef<CampaignEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Campaign
  ) {
    this.campaign = { ...data };
  }

  onSave() {
    this.campaign.campaignStatus = 'InProgress'; // Set status to 'InProgress'
    this.dialogRef.close(this.campaign);
  }

  onClose() {
    this.dialogRef.close();
  }
}
