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
 
export interface User {
  id: string;
  userName: string;
  email: string;
  displayName: string;
  phoneNumber: string;
  dateCreated: string;
  isActive: boolean;
  lastLoginDate: string
}

export interface MyDonated {
  campaignTitle: string;
  amount: number;
  dateDonated: Date;
}

export interface EditProfile {
  displayName: string;
  phoneNumber: string;
}

export interface Profile {
  userName: string;
  displayName: string;
  phoneNumber: string;
}

export interface EditPass {
  id: string;
  currentPassword: string;
  newPassword: string;
  confirmPasword: string;
}