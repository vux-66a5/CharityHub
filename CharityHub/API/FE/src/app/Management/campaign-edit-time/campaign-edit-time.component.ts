import { CommonModule } from '@angular/common';
import { Component, Inject, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { CampaignService } from '../services/campaign.service';
import { CampaignTime } from '../Models/campaign.model';
import { Subscription } from 'rxjs';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-campaign-edit-time',
  standalone: true,
  imports: [CommonModule, FormsModule, NgbModule],
  templateUrl: './campaign-edit-time.component.html',
  styleUrl: './campaign-edit-time.component.css'
})
export class CampaignEditTimeComponent implements OnDestroy {
  campaign: CampaignTime;
  campaignId?: string;
  editCampaignSubscription?: Subscription;

  constructor(
    public dialogRef: MatDialogRef<CampaignEditTimeComponent>, private route: ActivatedRoute, private campaignService: CampaignService, private router: Router,
    @Inject(MAT_DIALOG_DATA) public data: { campaignId: string } & CampaignTime
  ) {
    console.log('Dialog data:', data);
    this.campaign = { ...data };
    this.campaignId = data.campaignId;
  }

  onSave() {
    const editCampaignRequest: CampaignTime = {
      startDate: this.campaign?.startDate ?? '',
      endDate: this.campaign?.endDate ?? '',
    }

    console.log(editCampaignRequest);

    if (this.campaignId) {
      this.editCampaignSubscription = this.campaignService.editDateCampaign(this.campaignId, editCampaignRequest)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('campaigns-management');
          }
        })
    }
    this.dialogRef.close(this.campaign);
  }

  onClose() {
    this.dialogRef.close();
  }

  ngOnDestroy(): void {
    this.editCampaignSubscription?.unsubscribe();
  }
}
