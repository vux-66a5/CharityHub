import { Component } from '@angular/core';
import { MatCheckbox } from '@angular/material/checkbox';
import { MatDialog, MatDialogActions, MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { RegisterComponent } from '../register/register.component';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatFormField, MatLabel, MatCheckbox, MatDialogActions, MatDialogContent, CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginData = {
    username: '',
    password: ''
  };
  constructor(public dialog: MatDialog, 
    private dialogRef: MatDialogRef<LoginComponent>, 
    private http: HttpClient,
    ) {}

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
  
  login(): void {
    this.http.post('https://localhost:7244/api/User/Login', this.loginData)
      .subscribe(response => {
        console.log(response);
        alert('Đăng nhập thành công!');
      }, error => {
        console.error(error);
        alert('Đăng nhập thất bại. Vui lòng thử lại.');
      });
  }
}
