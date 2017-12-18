import { Component, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser/';

@Component({
  selector: 'app-scanners',
  templateUrl: './scanners.component.html',
  styleUrls: ['./scanners.component.css']
})
export class ScannersComponent implements OnInit {

  constructor(public iconReg: MatIconRegistry,
    public sanitizer: DomSanitizer) { 
    iconReg.addSvgIcon(
      'hamburger',
      sanitizer.bypassSecurityTrustResourceUrl('../../../assets/Hamburger_icon.svg'));
  }

  ngOnInit() {
  }

}
