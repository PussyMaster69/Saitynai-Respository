import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { MatDialog, MatDialogRef} from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';


import { Pair } from '../../interfaces/pair';
import { PairUpdateDialogComponent } from '../pair-update-dialog/pair-update-dialog.component';
import { DeviceService } from '../../services/device.service';


@Component({
  selector: 'app-pairs',
  templateUrl: './pairs.component.html',
  styleUrls: ['./pairs.component.css']
})
export class PairsComponent {

  private initialSelection = [];
  private allowMultiSelect = false;
  
  public selection = new SelectionModel<Pair>(this.allowMultiSelect, this.initialSelection);
  public shit: string;

  public displayedColumns = ['Id', 'FriendlyName'];
  public myDataSource = new MatTableDataSource<Pair>(PAIR_DATA);

  constructor(
    private deviceService: DeviceService, 
    public dialog: MatDialog, 
    private httpClient: HttpClient
  ) { }

  public showInfoDialog(): void {
    var selectedPair = this.selection.selected[0];
    let dialogRef = this.dialog.open(PairUpdateDialogComponent, {
      data: {
        id: selectedPair.Id, 
        friendlyName: selectedPair.FriendlyName, 
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
            selectedPair.FriendlyName = result.friendlyName;
            console.log(result.action);
            break;
          case 'delete':
            // TODO: delete and entry in the database
            console.log(result.action);
            break;
        }
        // selectedPair.FriendlyName = result;
        // TODO: update pair in the database
        // this.deviceService.updatePair(selectedPair).subscribe();    
      }
    });
  }
}

const PAIR_DATA: Pair[] = [
  {Id: 1, FriendlyName: 'shitFuck'},
  {Id: 10, FriendlyName: 'shitdasdFuck'},
  {Id: 3, FriendlyName: 'uck'},
  {Id: 4, FriendlyName: 'shit'}
];
