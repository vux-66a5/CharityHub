import { Component, ViewChild } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./header/header.component";
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from './auth/login/login.component';
import { DonationListComponent } from "./donation-list/donation-list.component";
import { DonationCardComponent } from "./donation-card/donation-card.component";
import { CardDetailComponent } from "./card-detail/card-detail.component";
import { FooterComponent } from "./footer/footer.component";
import { TestComponent } from "./test/test.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, 
    HeaderComponent, 
    LoginComponent, 
    DonationListComponent, 
    DonationCardComponent, 
    CardDetailComponent,
    FooterComponent, 
    TestComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'CharityHub';
  showHeader: boolean = true;

  constructor(private router: Router) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        // Kiểm tra nếu URL bắt đầu bằng "/admin"
        this.showHeader = !event.url.startsWith('/admin');
      }
    });
  }
}
