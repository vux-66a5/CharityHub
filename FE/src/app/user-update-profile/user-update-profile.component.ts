import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-update-profile',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './user-update-profile.component.html',
  styleUrl: './user-update-profile.component.css'
})
export class UserUpdateProfileComponent {
  profile = {
    displayName: '',
    phoneNumber: '',
    password: ''
  };
  confirmPassword = '';

  constructor(private authService: AuthService) {
    // Lấy thông tin user từ BehaviorSubject
    const user = this.authService.user.getValue();
    if (user) {
      this.profile.displayName = user.name;
      // Giả sử bạn có thêm phoneNumber trong user
      this.profile.phoneNumber = user.phoneNumber || '';
    }
  }

  onSubmit() {
    if (this.profile.password !== this.confirmPassword) {
      alert("Passwords do not match!");
      return;
    }

    // Cập nhật thông tin profile
    console.log('Updated Profile:', this.profile);

    // Sau khi cập nhật thành công, bạn có thể chuyển hướng hoặc thông báo thành công
  }
}
