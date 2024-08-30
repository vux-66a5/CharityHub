// import { Component, EventEmitter, Output } from '@angular/core';
// import { MatDialog } from '@angular/material/dialog';
// import { Router } from '@angular/router';
// import { LoginComponent } from '../auth/login/login.component';
// import { CommonModule } from '@angular/common';

// @Component({
//   selector: 'app-header',
//   standalone: true,
//   imports: [CommonModule],
//   templateUrl: './header.component.html',
//   styleUrl: './header.component.css'
// })
// export class HeaderComponent {

//   constructor(public dialog: MatDialog) {
//   }

//   openLoginDialog(): void {
//     const dialogRef = this.dialog.open(LoginComponent, {
//       width:'30%',
//       height:'80%',
//     });

//     dialogRef.afterClosed().subscribe(result => {
//       console.log('The dialog was closed');
//     });
//   }
// }

import { Component, EventEmitter, HostListener, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router, RouterModule } from '@angular/router';
import { LoginComponent } from '../auth/login/login.component';
import { AuthService } from '../auth/services/auth.service';
import { User } from '../auth/models/user.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit{
  user?: User;

  constructor(public dialog: MatDialog, private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    // Đăng ký để nhận thông tin người dùng từ BehaviorSubject
    this.authService.user()
    .subscribe({
      next: (response) => {
        this.user = response;
      }
    });

    this.user = this.authService.getUser();
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigateByUrl('/');
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
}

