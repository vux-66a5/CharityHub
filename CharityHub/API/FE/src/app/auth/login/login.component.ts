import { Component, inject, NgZone } from '@angular/core';
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
import { FacebookLoginProvider, GoogleSigninButtonModule, SocialAuthService } from '@abacritt/angularx-social-login';
import { MatCardModule } from '@angular/material/card'; // Import MatCardModule

declare const FB: any;

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
  imports: [MatCardModule, MatFormField, MatLabel, MatCheckbox, MatDialogActions, MatDialogContent, FormsModule, GoogleSigninButtonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  socialAuthService = inject(SocialAuthService);
  name!: string;
  httpService = inject(AuthService);
  ngOnInit() {
    this.socialAuthService.authState.subscribe({
      next: (result) => {
        this.name = result.name;
        if (result.idToken) {
          this.httpService.googleLogin(result.idToken).subscribe({
            next: (response) => {
              console.log(response);
              this.cookieService.set('Authorization', `Bearer ${response.jwtToken}`,
                undefined, '/', undefined, true, 'Strict'
              );

              const user: User = {
                id: response.id,
                userName: response.userName,
                role: response.role
              };

              console.log(user)

              this.authService.setUser(user)

              this.dialogRef.close();

              this.router.navigateByUrl('/');
            }
          })
        }

        if (result.authToken) {
          this.httpService.facebookLogin(result.authToken).subscribe({
            next: (response) => {
              console.log(response);
              this.cookieService.set('Authorization', `Bearer ${response.jwtToken}`,
                undefined, '/', undefined, true, 'Strict'
              );

              const user: User = {
                id: response.id,
                userName: response.userName,
                role: response.role
              };

              console.log(user)

              this.authService.setUser(user)

              this.dialogRef.close();

              this.router.navigateByUrl('/');
            }
          })
        }

        console.log(result);
      },
      error: (error) => {
        console.log(error)
      }
    });
  }

  model: LoginRequest;

  constructor(public dialog: MatDialog,
    private dialogRef: MatDialogRef<LoginComponent>,
    private authService: AuthService,
    private cookieService: CookieService,
    private router: Router,
    private _ngZone: NgZone) {
    this.model = {
      userName: '',
      password: ''
    }
  }

  signInWithFB(): void {
    this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  onFormSubmit(): void {
    this.authService.login(this.model)
      .subscribe({
        next: (response) => {
          localStorage.setItem('jwtToken', response.jwtToken);
          this.cookieService.set('Authorization', `Bearer ${response.jwtToken}`,
            undefined, '/', undefined, true, 'Strict'
          );

          const user: User = {
            id: response.id,
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

  // loginWithFacebook(): void {
  //   this.authService.loginFacebook().subscribe({
  //     next: (response) => {
  //       window.location.href = response.redirectUrl; // Redirect đến Facebook để login
  //     },
  //     error: (err) => {
  //       console.error('Login Facebook failed', err);
  //     }
  //   });
  // }

  // async loginWithFacebook() {
  //   FB.login(async (result: any) => {
  //     await this.authService.loginWithFacebook(result.authResponse.accessToken).subscribe(
  //       (x:any) => {
  //         this._ngZone.run(() => {
  //           this.router.navigate(['/']);
  //         })
  //       },
  //       (error: any) => {
  //         console.log(error);
  //       }
  //     );
  //   }, {scope: 'email'});
  // }

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

