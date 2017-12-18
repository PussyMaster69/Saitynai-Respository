import { Component, Inject,OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Pair } from '../../interfaces/pair';
import { DeviceService } from '../../services/device.service';

@Component({
  selector: 'app-pair-update-dialog',
  templateUrl: './pair-update-dialog.component.html',
  styleUrls: ['./pair-update-dialog.component.css']
})
export class PairUpdateDialogComponent implements OnInit {
  
  constructor(
    private deviceService: DeviceService,
    public dialogRef: MatDialogRef<PairUpdateDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.getPairInfo();
  }

  public getPairInfo(): void {
    var id = this.data.id;
    console.log(id);
    this.deviceService.getPair(id).subscribe(pairInfo => {
      if (pairInfo != null) {
        this.data.id = pairInfo.id;
        this.data.friendlyName = pairInfo.friendlyName;
        this.data.name = pairInfo.name;
        this.data.address = pairInfo.address;
      }
    });
  }

  public onSave(): void {
    var result = { 'action': 'save', 'friendlyName': this.data.friendlyName };
    this.dialogRef.close(result);
  }

  public onDelete(): void {
    // TODO: delete pair from database
    this.deviceService.deletePair(this.data.id).subscribe(pair => {
      var result = { 'action': 'delete' };
      this.dialogRef.close(result); 
    });
  }

  public onCancel(): void {
    this.dialogRef.close();
  }
}
