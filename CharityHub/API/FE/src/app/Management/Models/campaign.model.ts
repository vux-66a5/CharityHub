export interface Campaign {
    campaignTitle: string;
    campaignThumbnail: string;
    campaignDescription: string;
    targetAmount: number;
    partnerName: string;
    partnerLogo: string;
    partnerNumber: string;
  }

export interface EditCampaign {
    campaignTitle: string;
    campaignThumbnail: string;
    campaignDescription: string;
    partnerName: string;
    partnerLogo: string;
    partnerNumber: string;
}

export interface CampaignsCard {
  campaignId: string;
  campaignCode: number;
  campaignTitle: string;
  campaignDescription: string;
  targetAmount: number;
  currentAmount: number;
  startDate: Date;
  endDate: Date;
  partnerName: string;
  partnerNumber: string;
  campaignStatus: string;
}

export interface CampaignTime {
  endDate: string;
  startDate: string;
}
  