import { Component, Inject, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';


import { Pair } from '../../interfaces/pair';
import { DeviceService } from '../../services/device.service';
import { PairExtended } from '../../interfaces/pair-extended';


@Component({
  selector: 'app-pair-create-dialog',
  templateUrl: './pair-create-dialog.component.html',
  styleUrls: ['./pair-create-dialog.component.css'],
  providers: [ FormBuilder ]
})
export class PairCreateDialogComponent implements OnInit {

  options: FormGroup;
  moreOptions: boolean;
  address = new FormControl('', [Validators.required, this.noWhitespaceValidator]);
  name = new FormControl('', [Validators.required, this.noWhitespaceValidator]);
  pair: PairExtended;

  constructor(
    private deviceService: DeviceService,
    public dialogRef: MatDialogRef<PairCreateDialogComponent>,
    public fb: FormBuilder) { 
    this.moreOptions = false;
    this.options = fb.group({
      hideRequired: false,
      floatLabel: 'auto',
    });
    this.pair = {
      id: 0, 
      name: '', 
      friendlyName: '', 
      address: ''
    };
  }

  ngOnInit() {
    this.address.valueChanges.subscribe(() => this.pair.address = this.pair.address.trim());
    this.name.valueChanges.subscribe(() => this.pair.name = this.pair.name.trim());
  }

  save(): void {
    if (this.address.valid && this.name.valid) {
      this.deviceService.addPair(this.pair).subscribe(pair => {
        this.dialogRef.close(this.pair);
      });
    }
  }

  cancel(): void {
    this.dialogRef.close();
  }

  check(): void {
    this.moreOptions = !this.moreOptions;
  }

  getAddressErrorMessage() {
    return this.address.hasError('required') ? 'You must enter a value' : 
      this.address.hasError('whitespace') ? 'You must enter a value' : 
        '';
  }

  getNameErrorMessage() {
    return this.name.hasError('required') ? 'You must enter a value' : 
      this.name.hasError('whitespace') ? 'You must enter a value' : 
        '';
  }

  private noWhitespaceValidator(control: FormControl) {
    let isWhitespace = (control.value || '').trim().length === 0;
    let isValid = !isWhitespace;
    return isValid ? null : { 'whitespace': true }
  }
}
