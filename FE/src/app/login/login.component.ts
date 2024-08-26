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
    // Xác thực đăng nhập ở đây...
    const fakeUser = { name: 'John Doe', avatarUrl: 'path/to/avatar.jpg' };
    
    // Cập nhật trạng thái đăng nhập
    this.authService.login(fakeUser);
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
