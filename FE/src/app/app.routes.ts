import { Routes } from '@angular/router';
import { DonationListComponent } from './donation-list/donation-list.component';
import { PaymentComponent } from './payment/payment.component';
import { PartnersComponent } from './partners/partners.component';
import { HomepageComponent } from './homepage/homepage.component';
import { UserUpdateProfileComponent } from './user-update-profile/user-update-profile.component';
import { CampaignsComponent } from './Management/campaigns/campaigns.component';
import { UsersComponent } from './Management/users/users.component';
import { TransactionComponent } from './transaction/transaction.component';

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
        path: 'profile', component: UserUpdateProfileComponent
    },
    {
        path: 'users-management', component: UsersComponent // Thêm route này cho trang quản lý người dùng
    },
    {
        path: 'campaigns-management', component: CampaignsComponent // Thêm route này cho trang quản lý chiến dịch
    },
    {
        path: 'transaction', component: TransactionComponent
    }

]
