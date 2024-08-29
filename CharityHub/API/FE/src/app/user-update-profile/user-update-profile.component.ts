import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-update-profile',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './user-update-profile.component.html',
  styleUrls: ['./user-update-profile.component.css']
})
export class UserUpdateProfileComponent {
  profile = {
    displayName: 'Nguyen Van A',
    phoneNumber: '0123456789',
    userName: 'nguyenvana',
    userEmail: 'nguyenvana@example.com',
    password: ''
  };
  oldPassword = '';
  newPassword = '';
  confirmNewPassword = '';

  isUpdateInfo = true;
  isChangePassword = false;

  constructor(private authService: AuthService) {
    const user = this.authService.user.getValue();
    if (user) {
      this.profile.displayName = user.displayName;
      this.profile.userName = user.userName;
      this.profile.userEmail = user.userEmail;
      this.profile.phoneNumber = user.phoneNumber || '';
    }
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
    console.log('Updated Profile Information:', this.profile);
  }

  onSubmitChangePassword() {
    if (this.newPassword !== this.confirmNewPassword) {
      alert("Mật khẩu mới không khớp!");
      return;
    }
    console.log('Changing Password:', {
      oldPassword: this.oldPassword,
      newPassword: this.newPassword
    });
  }
}