import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CampaignViewComponent } from '../campaign-view/campaign-view.component';
import { Campaign, CampaignsCard, CampaignTime } from '../Models/campaign.model';
import { CampaignEditComponent } from '../campaign-edit/campaign-edit.component';
import { CampaignAddComponent } from '../campaign-add/campaign-add.component';
import { CampaignService } from '../services/campaign.service';
import {  Route, Router, RouterLink } from '@angular/router';
import { CampaignEditTimeComponent } from '../campaign-edit-time/campaign-edit-time.component';

@Component({
  selector: 'app-campaigns',
  standalone: true,
  imports: [CommonModule, FormsModule, MatDialogModule, RouterLink],
  templateUrl: './campaigns.component.html',
  styleUrls: ['./campaigns.component.css']
})
export class CampaignsComponent implements OnInit {
  // Replace with actual data fetching
  filteredCampaigns: Campaign[] = [];
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 10;
  totalPages: number = 1;
  campaignsCard: CampaignsCard[] = []; 


  constructor(public dialog: MatDialog, private campaignService: CampaignService, private router: Router) {}

  ngOnInit(): void {
    // Fetch campaigns data here
    //this.campaigns = this.getCampaigns();
    this.campaignService.getViewCampaignsCard()
    .subscribe({
      next: (respone) => {
        this.campaignsCard = respone;
      }
    });
    this.totalPages = Math.ceil(this.campaignsCard.length / this.itemsPerPage);
    this.updateFilteredCampaigns();
  }

  onSearch(query: string) {
    this.campaignService.searchCampaigns(query)
    .subscribe({
      next: (respone) => {
        this.campaignsCard = respone;
      }
    });
  }

  // getCampaigns(): Campaign[] {
  //   // Replace with actual data fetching logic
  //   return [
  //     // Sample data
  //     // { campaignId: '1', campaignCode: '440330', campaignTitle: 'Campaign 1', campaignThumbnail: '', campaignDescription: '', targetAmount: 1000, currentAmount: 500, startDate: '2023-01-01', endDate: '2023-12-31', partnerName: 'Partner A', partnerLogo: '', campaignStatus: 'InProgress', partnerNumber: 'P001' },
  //     // { campaignId: '2', campaignCode: '519942', campaignTitle: 'Campaign 2', campaignThumbnail: '', campaignDescription: '', targetAmount: 1000, currentAmount: 500, startDate: '2023-01-01', endDate: '2023-12-31', partnerName: 'Partner A', partnerLogo: '', campaignStatus: 'New', partnerNumber: 'P001' },
  //     // More sample data...
  //   ];
  // }

  filterCampaigns() {
    this.currentPage = 1;
    this.updateFilteredCampaigns();
  }

  updateFilteredCampaigns() {
    // const filtered = this.campaigns.filter(campaign =>
    //   //campaign.campaignCode.includes(this.searchTerm) ||
    //   campaign.campaignTitle.includes(this.searchTerm) ||
    //   campaign.partnerName.includes(this.searchTerm) ||
    //   //campaign.campaignStatus.includes(this.searchTerm)
    // );
    // this.totalPages = Math.ceil(filtered.length / this.itemsPerPage);
    // this.filteredCampaigns = filtered.slice((this.currentPage - 1) * this.itemsPerPage, this.currentPage * this.itemsPerPage);
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updateFilteredCampaigns();
    }
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updateFilteredCampaigns();
    }
  }

  addNewCampaign() {
    const dialogRef = this.dialog.open(CampaignAddComponent, {
      width: '600px',
      height: '80vh', // Thay đổi chiều cao của modal
      maxHeight: '80vh', // Đặt chiều cao tối đa của modal
    });
    dialogRef.afterClosed()
  }

  viewCampaign(campaign: CampaignsCard) {
    this.dialog.open(CampaignViewComponent, {
      data: campaign,
      width: '600px',
    });
  }

  editCampaign(campaign: CampaignsCard) {
    if (campaign.campaignId == null) {
      console.error('campaignId is null or undefined.');
      return;
    }
  
    const dialogRef = this.dialog.open(CampaignEditComponent, {
      width: '600px',
      height: '80vh',
      maxHeight: '80vh',
      data: { campaignId: campaign.campaignId } // Truyền campaignId vào data
    });
  
    // dialogRef.afterClosed().subscribe(result => {
    //   if (result) {
    //     this.router.navigate(['/admin/edit-campaigns', campaign.campaignId]);
    //   }
    // });
  }

  editTimeCampaign(campaign: CampaignsCard) {
    if (campaign.campaignId == null) {
      console.error('campaignId is null or undefined.');
      return;
    }

    const dialogRef = this.dialog.open(CampaignEditTimeComponent, {
      width: '600px',
      height: '80vh', // Thay đổi chiều cao của modal
      maxHeight: '80vh', // Đặt chiều cao tối đa của modal
      data: { campaignId: campaign.campaignId }
    });
    dialogRef.afterClosed()
  }
  

  deleteCampaign(campaignId: string) {
    // Logic to delete campaign
    //this.campaigns = this.campaigns.filter(campaign => campaign.campaignId !== campaignId);
    this.campaignService.deleteCampaign(campaignId)
    .subscribe({
      next: (response) => {
        this.router.navigateByUrl('campaigns-management');
      }
    })
    window.location.reload();
    this.updateFilteredCampaigns();
  }
  
}
