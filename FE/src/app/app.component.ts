import { Component, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./header/header.component";
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from './login/login.component';
import { DonationListComponent } from "./donation-list/donation-list.component";
import { DonationCardComponent } from "./donation-card/donation-card.component";
import { CardDetailComponent } from "./card-detail/card-detail.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, LoginComponent, DonationListComponent, DonationCardComponent, CardDetailComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'CharityHub';
}
