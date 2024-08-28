import { Routes } from '@angular/router';
import { HeaderComponent } from './header/header.component';
import { DonationListComponent } from './donation-list/donation-list.component';
import { UsersComponent } from './Management/users/users.component';
import { CampaignsComponent } from './Management/campaigns/campaigns.component';
import { PaymentComponent } from './payment/payment.component';
import { PartnersComponent } from './partners/partners.component';
import { HomepageComponent } from './homepage/homepage.component';
import { UserUpdateProfileComponent } from './user-update-profile/user-update-profile.component';
import { authGuard } from './auth/guards/auth.guard';
import { CampaignEditComponent } from './Management/campaign-edit/campaign-edit.component';

export const routes: Routes = [
    {
        path: '', component: HomepageComponent
    },
    {
        path: 'donation-list', component: DonationListComponent
    },
    { 
        path: 'payment/:campaignCode', component: PaymentComponent 
    },
    {
        path: 'partners', component:PartnersComponent
    },
    {
        path: 'update-profile', component: UserUpdateProfileComponent
    },
    {
        path: 'users-management', component: UsersComponent,
        canActivate: [authGuard]
    },
    {
        path: 'campaigns-management', component: CampaignsComponent,
        canActivate: [authGuard]
    },
    {
        path: 'admin/edit-campaigns/:campaignId', component: CampaignEditComponent
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
]
