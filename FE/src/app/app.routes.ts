import { Routes } from '@angular/router';
import { DonationListComponent } from './donation-list/donation-list.component';
import { PaymentComponent } from './payment/payment.component';
import { PartnersComponent } from './partners/partners.component';
import { HomepageComponent } from './homepage/homepage.component';
import { UserUpdateProfileComponent } from './user-update-profile/user-update-profile.component';

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
    }
]
