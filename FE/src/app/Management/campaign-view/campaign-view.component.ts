import { CurrencyPipe } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-campaign-view',
  standalone: true,
  imports: [CurrencyPipe],
  templateUrl: './campaign-view.component.html',
  styleUrl: './campaign-view.component.css'
})
export class CampaignViewComponent {
  constructor(
    public dialogRef: MatDialogRef<CampaignViewComponent>, // Sử dụng MatDialogRef thay vì MatDialog
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  close(): void {
    this.dialogRef.close();
  }
}
