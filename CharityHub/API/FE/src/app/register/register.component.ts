import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { LoginComponent } from '../login/login.component';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerData = {
    displayName: '',
    username: '',
    password: ''
  };

  constructor(
    public dialog: MatDialog,
    public dialogRef: MatDialogRef<RegisterComponent>,
    private http: HttpClient
  ) {}

  openLoginDialog(): void {
    const loginDialogRef = this.dialog.open(LoginComponent, {
      width: '30%',
      height: '80%',
    });

    this.dialogRef.close();

    loginDialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  register(): void {
    this.http.post('https://localhost:7244/api/NoUser/Register', this.registerData)
      .subscribe(response => {
        console.log(response);
        alert('Đăng ký thành công!');
      }, error => {
        console.error(error);
        alert('Đăng ký thất bại. Vui lòng thử lại.');
      });
  }
}
