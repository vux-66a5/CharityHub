import { Component } from '@angular/core';
import { MatCheckbox } from '@angular/material/checkbox';
import { MatDialog, MatDialogActions, MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { RegisterComponent } from '../../register/register.component';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { LoginRequest } from '../models/login-request.model';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { User } from '../models/user.model';

// @Component({
//   selector: 'app-login',
//   standalone: true,
//   imports: [MatFormField, MatLabel, MatCheckbox, MatDialogActions, MatDialogContent, FormsModule],
//   templateUrl: './login.component.html',
//   styleUrl: './login.component.css'
// })
// export class LoginComponent {

//   model: LoginRequest;

//   constructor(
//     public dialog: MatDialog, 
//     private dialogRef: MatDialogRef<LoginComponent>, 
//     private authService: AuthService, // Thêm dòng này
//     private cookieService: CookieService,
//     private router: Router
//   ) {
//     this.model = {
//       username: '',
//       password: ''
//     }
//   }

//   onFormSubmit(): void {
//     this.authService.login(this.model)
//     .subscribe({
//       next: (response) => {
//         this.cookieService.set('Authorization', `Bearer ${response.token}`, 
//           undefined, '/', undefined, true, 'Strict'
//         );

//         this.router.navigateByUrl('/');
//       }
//     });
//   }

//   openRegisterDialog(): void {
//     // Mở dialog modal của Register
//     const registerDialogRef = this.dialog.open(RegisterComponent, {
//       width: '30%',
//       height: '85%',
//     });

//     // Đóng dialog modal của Login
//     this.dialogRef.close();

//     registerDialogRef.afterClosed().subscribe(result => {
//       console.log('The dialog was closed');
//     });
//   }
// }

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatFormField, MatLabel, MatCheckbox, MatDialogActions, MatDialogContent, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  model: LoginRequest;

  constructor(public dialog: MatDialog, 
    private dialogRef: MatDialogRef<LoginComponent>,
    private authService: AuthService,
    private cookieService: CookieService,
    private router: Router) {
    this.model = {
      userName: '',
      password: ''
    }
  }

  onFormSubmit(): void {
    this.authService.login(this.model)
    .subscribe({
      next: (response) => {
        this.cookieService.set('Authorization', `Bearer ${response.jwtToken}`, 
          undefined, '/', undefined, true, 'Strict'
        );

        const user: User = {
          userName: response.userName,
          role: response.role
        };

        console.log(user)

        this.authService.setUser(user)

        this.dialogRef.close();

        this.router.navigateByUrl('/');
      }
    });
  }

  openRegisterDialog(): void {
    // Mở dialog modal của Register
    const registerDialogRef = this.dialog.open(RegisterComponent, {
      width: '30%',
      height: '85%',
    });

    // Đóng dialog modal của Login
    this.dialogRef.close();

    registerDialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }
}

