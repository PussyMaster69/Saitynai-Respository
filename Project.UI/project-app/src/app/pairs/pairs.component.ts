import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';


@Component({
  selector: 'app-pairs',
  templateUrl: './pairs.component.html',
  styleUrls: ['./pairs.component.css']
})
export class PairsComponent implements OnInit {

  displayedColumns = [ 'position', 'name', 'wight', 'symbol' ];
  dataSource = new MatTableDataSource<Element>(ELEMENT_DATA);

  constructor() { }

  ngOnInit() {
  }

}

export interface Element {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

const ELEMENT_DATA: Element[] = [
  {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'}
];
