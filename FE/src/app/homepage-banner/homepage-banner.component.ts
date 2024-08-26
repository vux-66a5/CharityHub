import { Component, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-homepage-banner',
  standalone: true,
  imports: [],
  templateUrl: './homepage-banner.component.html',
  styleUrl: './homepage-banner.component.css'
})
export class HomepageBannerComponent {
  @ViewChild('donationList', { static: false }) donationList!: ElementRef;

  scrollToDonationList() {
    const donationListElement = document.querySelector('app-donation-list');
    if (donationListElement) {
      donationListElement.scrollIntoView({ behavior: 'smooth' });
    }
  }
}
