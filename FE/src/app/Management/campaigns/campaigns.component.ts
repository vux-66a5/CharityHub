import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CampaignViewComponent } from '../campaign-view/campaign-view.component';
import { Campaign } from '../Models/campaign.model';
import { CampaignEditComponent } from '../campaign-edit/campaign-edit.component';
import { CampaignAddComponent } from '../campaign-add/campaign-add.component';

@Component({
  selector: 'app-campaigns',
  standalone: true,
  imports: [CommonModule, FormsModule, MatDialogModule],
  templateUrl: './campaigns.component.html',
  styleUrls: ['./campaigns.component.css']
})
export class CampaignsComponent implements OnInit {
  campaigns: Campaign[] = []; // Replace with actual data fetching
  filteredCampaigns: Campaign[] = [];
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 10;
  totalPages: number = 1;

  constructor(public dialog: MatDialog) {}

  ngOnInit() {
    // Fetch campaigns data here
    this.campaigns = this.getCampaigns();
    this.totalPages = Math.ceil(this.campaigns.length / this.itemsPerPage);
    this.updateFilteredCampaigns();
  }

  getCampaigns(): Campaign[] {
    // Replace with actual data fetching logic
    return [
      // Sample data
      { campaignId: '1', campaignCode: '440330', campaignTitle: 'Campaign 1', campaignThumbnail: '', campaignDescription: '', targetAmount: 1000, currentAmount: 500, startDate: '2023-01-01', endDate: '2023-12-31', partnerName: 'Partner A', partnerLogo: '', campaignStatus: 'InProgress', partnerNumber: 'P001' },
      { campaignId: '2', campaignCode: '519942', campaignTitle: 'Campaign 2', campaignThumbnail: '', campaignDescription: '', targetAmount: 1000, currentAmount: 500, startDate: '2023-01-01', endDate: '2023-12-31', partnerName: 'Partner A', partnerLogo: '', campaignStatus: 'New', partnerNumber: 'P001' },
      // More sample data...
    ];
  }

  filterCampaigns() {
    this.currentPage = 1;
    this.updateFilteredCampaigns();
  }

  updateFilteredCampaigns() {
    const filtered = this.campaigns.filter(campaign =>
      campaign.campaignCode.includes(this.searchTerm) ||
      campaign.campaignTitle.includes(this.searchTerm) ||
      campaign.partnerName.includes(this.searchTerm) ||
      campaign.campaignStatus.includes(this.searchTerm)
    );
    this.totalPages = Math.ceil(filtered.length / this.itemsPerPage);
    this.filteredCampaigns = filtered.slice((this.currentPage - 1) * this.itemsPerPage, this.currentPage * this.itemsPerPage);
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

  viewCampaign(campaign: Campaign) {
    this.dialog.open(CampaignViewComponent, {
      data: campaign,
      width: '600px',
    });
  }

  editCampaign(campaign: Campaign) {
    // Logic to edit campaign details
    // Open the Edit modal or navigate to the Edit page
    const dialogRef = this.dialog.open(CampaignEditComponent, {
      width: '600px',
      height: '80vh', // Thay đổi chiều cao của modal
      maxHeight: '80vh', // Đặt chiều cao tối đa của modal
    });
  }

  deleteCampaign(campaignId: string) {
    // Logic to delete campaign
    this.campaigns = this.campaigns.filter(campaign => campaign.campaignId !== campaignId);
    this.updateFilteredCampaigns();
  }
}
