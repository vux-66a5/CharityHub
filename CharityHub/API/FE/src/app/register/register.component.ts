import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { LoginComponent } from '../auth/login/login.component';
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
  errors = {
    password: '',
    username: ''
  };


  constructor(
    public dialog: MatDialog,
    public dialogRef: MatDialogRef<RegisterComponent>,
    private http: HttpClient
  ) { }


  validateForm(): boolean {
    let valid = true;

    // Password validation
    if (this.registerData.password.length < 8) {
      this.errors.password = 'Mật khẩu phải có ít nhất 8 ký tự.';
      valid = false;
    } else {
      this.errors.password = '';
    }

    // Email validation
    if (!this.validateEmailFormat(this.registerData.username)) {
      this.errors.username = 'Email không hợp lệ.';
      valid = false;
    } else {
      this.errors.username = '';
    }

    // Check if email is already taken
    if (valid) {
      this.http.get(`https://localhost:7244/api/NoUser/CheckEmail?email=${this.registerData.username}`)
        .subscribe((response: any) => {
          if (response.exists) {
            this.errors.username = 'Email đã được đăng ký.';
            valid = false;
          } else {
            this.errors.username = '';
          }
        });
    }

    return valid;
  }

  validateEmailFormat(email: string): boolean {
    // Regular expression for validating email format
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
  }

  validateEmail(): void {
    if (this.registerData.username) {
      if (!this.validateEmailFormat(this.registerData.username)) {
        this.errors.username = 'Email không hợp lệ.';
      } else {
        this.errors.username = '';
      }
    }
  }

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
    if (this.validateForm()) {
      this.http.post('https://localhost:7244/api/NoUser/Register', this.registerData)
        .subscribe(response => {
          console.log(response);
          alert('Đăng ký thành công!');
          this.openLoginDialog();
        }, error => {
          console.error(error);
          alert('Đăng ký thất bại. Vui lòng thử lại.');
        });
    }
  }
}