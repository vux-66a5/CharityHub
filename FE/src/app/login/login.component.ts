import { Component } from '@angular/core';
import { MatCheckbox } from '@angular/material/checkbox';
import { MatDialog, MatDialogActions, MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { RegisterComponent } from '../register/register.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatFormField, MatLabel, MatCheckbox, MatDialogActions, MatDialogContent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(public dialog: MatDialog, private dialogRef: MatDialogRef<LoginComponent>) {}

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
