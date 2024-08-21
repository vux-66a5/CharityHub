import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  constructor(public dialog: MatDialog, public dialogRef: MatDialogRef<RegisterComponent>) {}

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
}
