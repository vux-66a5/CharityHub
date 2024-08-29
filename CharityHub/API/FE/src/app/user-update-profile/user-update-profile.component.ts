import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { EditPass, EditProfile, Profile } from '../Management/Models/campaign.model';
import { AuthService } from '../auth/services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserServiceService } from '../user-service.service';

@Component({
  selector: 'app-user-update-profile',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './user-update-profile.component.html',
  styleUrls: ['./user-update-profile.component.css']
})
export class UserUpdateProfileComponent implements OnInit, OnDestroy{
  profile?: Profile;
  currentPassword = '';
  newPassword = '';
  confirmNewPassword = '';
  userId: string | null = '';
  editProfileSubscription?: Subscription;
  editPassSubscription?: Subscription;
  isUpdateInfo = true;
  isChangePassword = false;

  constructor(private authService: AuthService, private route: ActivatedRoute, private router: Router, private userService: UserServiceService) {
    
  }

  ngOnInit(): void {
    const user = this.authService.getUser();
    this.userId = user?.id || '';

    this.userService.getProfile(this.userId)
    .subscribe({
      next: (respone) => {
        this.profile = respone;
      }
    })
  }

  toggleUpdateInfo() {
    this.isUpdateInfo = true;
    this.isChangePassword = false;
  }

  toggleChangePassword() {
    this.isUpdateInfo = false;
    this.isChangePassword = true;
  }

  onSubmitUpdateInfo() {
    const editProfile: EditProfile = {
      displayName: this.profile?.displayName ?? '',
      phoneNumber: this.profile?.phoneNumber ?? ''
    }

    if (this.userId) {
      this.editProfileSubscription = this.userService.editProfileById(this.userId, editProfile)
      .subscribe({
        next: (reponse) => {
          this.router.navigateByUrl(`update-profile`);
        }
      })
      window.location.reload();
    }
  }

  onSubmitChangePassword() {
    if (this.newPassword !== this.confirmNewPassword) {
      alert("Mật khẩu mới không khớp!");
      return;
    }

    if (this.userId) {
      const editPass: EditPass = {
        id: this.userId,
        currentPassword: this.currentPassword,
        newPassword: this.newPassword,
        confirmPasword: this.confirmNewPassword
      }

      this.editPassSubscription = this.userService.editPassword(editPass)
      .subscribe({
        next: (reponse) => {
          this.router.navigateByUrl(`update-profile`);
        }
      });
    }
    // if (this.userId) {
    //   this.userService.changePassword(this.userId, this.currentPassword, this.newPassword, this.confirmNewPassword)
    // .subscribe(
    //   response => {
    //     // Xử lý phản hồi thành công
    //     console.log(response.message); // Ví dụ: "Password changed successfully!"
    //   },
    //   error => {
    //     // Xử lý lỗi
    //     if (error.error && error.error.errors) {
    //       console.error('Errors:', error.error.errors.join(', ')); // Ví dụ: "Incorrect password."
    //     } else {
    //       console.error('Error changing password', error);
    //     }
    //   }
    // );
    // }  
  }

  ngOnDestroy(): void {
      this.editProfileSubscription?.unsubscribe();
      this.editPassSubscription?.unsubscribe();
  }
}