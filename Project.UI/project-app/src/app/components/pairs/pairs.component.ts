import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { MatDialog, MatDialogRef} from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';


import { Pair } from '../../interfaces/pair';
import { PairUpdateDialogComponent } from '../pair-update-dialog/pair-update-dialog.component';
import { DeviceService } from '../../services/device.service';
import { forEach } from '@angular/router/src/utils/collection';
import { PairCreateDialogComponent } from '../pair-create-dialog/pair-create-dialog.component';


@Component({
  selector: 'app-pairs',
  templateUrl: './pairs.component.html',
  styleUrls: ['./pairs.component.css']
})
export class PairsComponent implements OnInit {

  public pairData: Pair[];

  private initialSelection = [];
  private allowMultiSelect = false;
  
  public selection = new SelectionModel<Pair>(this.allowMultiSelect, this.initialSelection);

  public displayedColumns = ['Id', 'FriendlyName'];
  public myDataSource: MatTableDataSource<Pair>;

  constructor(
    private deviceService: DeviceService, 
    public dialog: MatDialog, 
    private httpClient: HttpClient
  ) { }

  ngOnInit() {
    this.deviceService.getPairs().subscribe(pairs => {
      this.pairData = pairs;
      this.myDataSource = new MatTableDataSource<Pair>(this.pairData);
    });
    
  }

  public showInfoDialog(): void {
    var selectedPair = this.selection.selected[0];
    let dialogRef = this.dialog.open(PairUpdateDialogComponent, {
      data: {
        id: selectedPair.id, 
        friendlyName: selectedPair.friendlyName, 
        name: '', 
        address: ''
      },
      height: '500px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result)
      {
        switch (result.action) {
          case 'save':
            console.log(result.action);
            selectedPair.friendlyName = result.friendlyName;
            this.updatePair(selectedPair);
            break;
          case 'delete':
            // TODO: delete and entry in the database
            console.log(result.action);
            var pairIndex = this.pairData.findIndex(p => p.id == selectedPair.id);
            this.pairData.splice(pairIndex, 1);
            break;
        }
      }
    });
  }

  public createPairDialog(): void {
    let dialogRef = this.dialog.open(PairCreateDialogComponent, {
      height: '500px',
      width: '600px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result);
        this.pairData.push(result as Pair);
      }
    });
  }

  private updatePair(pair: Pair): void {
    var pairIndex = this.pairData.findIndex(p => p.id == pair.id);
    this.deviceService.updatePair(pair).subscribe(p => this.pairData[pairIndex] = p);
  }
}
