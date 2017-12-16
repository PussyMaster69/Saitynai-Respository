import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';


import { SelectionModel } from '@angular/cdk/collections';
import { MatDialog, MatDialogRef} from '@angular/material/dialog';


import { Pair } from '../../interfaces/pair';
import { PairUpdateDialogComponent } from '../pair-update-dialog/pair-update-dialog.component';


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

  constructor(public dialog: MatDialog) { }

  public showInfoDialog(): void {
    var selectedPair = this.selection.selected[0];
    let dialogRef = this.dialog.open(PairUpdateDialogComponent, {
      data: {id: selectedPair.Id, friendlyName: selectedPair.FriendlyName},
      height: '500px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result)
      {
        selectedPair.FriendlyName = result;
        // TODO: update pair in the database
      }
    });
  }
}

const PAIR_DATA: Pair[] = [
  {Id: 1, FriendlyName: 'shitFuck'},
  {Id: 2, FriendlyName: 'shitdasdFuck'},
  {Id: 3, FriendlyName: 'uck'},
  {Id: 4, FriendlyName: 'shit'}
];
