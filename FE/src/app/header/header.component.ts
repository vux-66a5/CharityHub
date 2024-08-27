import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router, RouterModule } from '@angular/router'; // Thêm import này
import { LoginComponent } from '../login/login.component';
import { AuthService } from '../services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  isLoggedIn = false;
  user: any = null;
  role: string = 'user';

  // Thêm Router vào constructor
  constructor(public dialog: MatDialog, private authService: AuthService, private router: Router) {
    this.authService.isLoggedIn$.subscribe(status => this.isLoggedIn = status);
    this.authService.user$.subscribe(user => this.user = user);
    this.authService.role$.subscribe(role => this.role = role);
  }

  openLoginDialog(): void {
    const dialogRef = this.dialog.open(LoginComponent, {
      width:'30%',
      height:'80%',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  logout(): void {
    this.authService.logout();
  }
}
