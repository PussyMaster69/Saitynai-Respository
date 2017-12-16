import { Component, Inject } from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Pair } from '../../interfaces/pair';

@Component({
  selector: 'app-pair-update-dialog',
  templateUrl: './pair-update-dialog.component.html',
  styleUrls: ['./pair-update-dialog.component.css']
})
export class PairUpdateDialogComponent {
  
  constructor(
    public dialogRef: MatDialogRef<PairUpdateDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  onCancel(): void {
    this.dialogRef.close();
  }
}
