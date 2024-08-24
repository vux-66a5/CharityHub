import { Routes } from '@angular/router';
import { HeaderComponent } from './header/header.component';
import { DonationListComponent } from './donation-list/donation-list.component';
import { SideBarComponent } from './admin-management/side-bar/side-bar.component';

export const routes: Routes = [
    {
        path: '', component: DonationListComponent
    },
    {
        path: 'donation-list', component: DonationListComponent
    },
    {
        path: 'admin', component: SideBarComponent
    }
]
