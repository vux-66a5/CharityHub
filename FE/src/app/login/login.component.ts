import { Component } from '@angular/core';
import { MatCheckbox } from '@angular/material/checkbox';
import { MatDialog, MatDialogActions, MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { RegisterComponent } from '../register/register.component';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatFormField, MatLabel, MatCheckbox, MatDialogActions, MatDialogContent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(
    public dialog: MatDialog, 
    private dialogRef: MatDialogRef<LoginComponent>, 
    private authService: AuthService // Thêm dòng này
  ) {}

  login() {
    const username = (document.querySelector('.auth-form__input[type="text"]') as HTMLInputElement).value;
    const password = (document.querySelector('.auth-form__input[type="password"]') as HTMLInputElement).value;
  
    let fakeUser;
    let role;
  
    // Kiểm tra username và password
    if (username === 'admin' && password === 'admin123') {
      fakeUser = { name: 'Admin User', avatarUrl: 'path/to/admin-avatar.jpg' };
      role = 'admin';
    } else if (username === 'user' && password === 'user123') {
      fakeUser = { name: 'Regular User', avatarUrl: 'path/to/user-avatar.jpg' };
      role = 'user';
    } else {
      alert('Invalid username or password!');
      return;
    }
  
    // Cập nhật trạng thái đăng nhập với role
    this.authService.login(fakeUser, role);
    this.dialogRef.close();
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
