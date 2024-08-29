import { Component } from '@angular/core';
import { DonationCardComponent } from "../donation-card/donation-card.component";
import { DonationListComponent } from "../donation-list/donation-list.component";
import { HomepageBannerComponent } from "../homepage-banner/homepage-banner.component";

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [DonationCardComponent, DonationListComponent, HomepageBannerComponent],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent {

}
