import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';

import { Pair } from '../../interfaces/pair';


@Component({
  selector: 'app-pairs',
  templateUrl: './pairs.component.html',
  styleUrls: ['./pairs.component.css']
})
export class PairsComponent {

  public displayedColumns = ['Id', 'Name'];
  public myDataSource = new MatTableDataSource<Pair>(PAIR_DATA);

  // public applyFilter(filterValue: string): void {
  //   filterValue = filterValue.trim(); // Remove whitespace
  //   filterValue = filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
  //   this.dataSource.filter = filterValue;
  // }


}

const PAIR_DATA: Pair[] = [
  {Id: 1, FriendlyName: 'shitFuck'},
  {Id: 2, FriendlyName: 'shitdasdFuck'},
  {Id: 3, FriendlyName: 'uck'},
  {Id: 4, FriendlyName: 'shit'}
];
