import { Component, Inject, NgModule, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogContent, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { Campaign, CampaignsCard, EditCampaign } from '../Models/campaign.model';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CampaignService } from '../services/campaign.service';

@Component({
  selector: 'app-campaign-edit',
  standalone: true,
  imports: [FormsModule, MatDialogModule, MatFormField, MatLabel],
  templateUrl: './campaign-edit.component.html',
  styleUrl: './campaign-edit.component.css'
})
export class CampaignEditComponent implements OnDestroy {
  campaign: EditCampaign;
  campaignId?: string;
  paramsSubscription?: Subscription;
  editCampaignSubscription?: Subscription;

  constructor(
    public dialogRef: MatDialogRef<CampaignEditComponent>, private route: ActivatedRoute, private campaignService: CampaignService, private router: Router,
    @Inject(MAT_DIALOG_DATA) public data: {campaignId: string} & EditCampaign
  ) {
    console.log('Dialog data:', data);
    this.campaign = { ...data };
    this.campaignId = data.campaignId;
  }

  // ngOnInit(): void {
  //     // this.paramsSubscription = this.route.paramMap.subscribe({
  //     //   next: (params) => {

  //     //     if (this.campaignId) {
            
  //     //     }
  //     //   }
  //     // });
  // }


  onSave() {
    //this.campaign.campaignStatus = 'InProgress'; // Set status to 'InProgress'
    const editCampaignRequest: EditCampaign = {
      campaignTitle: this.campaign?.campaignTitle ?? '',
      campaignThumbnail: this.campaign?.campaignThumbnail ?? '',
      campaignDescription: this.campaign?.campaignDescription ?? '',
      partnerName: this.campaign?.partnerName ?? '',
      partnerLogo: this.campaign?.partnerLogo ?? '',
      partnerNumber: this.campaign?.partnerNumber ?? '',
    }

    if (this.campaignId) {
      this.editCampaignSubscription = this.campaignService.editCampaignById(this.campaignId, editCampaignRequest)
    .subscribe({
      next: (response) => {
        this.router.navigateByUrl('campaigns-management');
      }
    })
    window.location.reload();
    }
    this.dialogRef.close(this.campaign);
  }

  onClose() {
    this.dialogRef.close();
  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editCampaignSubscription?.unsubscribe();
  }
}
