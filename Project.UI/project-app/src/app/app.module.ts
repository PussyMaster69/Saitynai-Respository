import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';


import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CdkTableModule } from '@angular/cdk/table';    
import {
  MatFormFieldModule,
  MatToolbarModule,
  MatTabsModule,
  MatMenuModule,
  MatListModule,
  MatCardModule,
  MatButtonModule,
  MatTableModule,
  MatDialogModule,
  MatInputModule,
  MatCheckboxModule,
  MatSidenavModule,
  MatIconModule,
  MatIconRegistry
} from '@angular/material';


import { SessionService } from './services/session.service';
import { DeviceService } from './services/device.service';


import { AppComponent } from './app.component';
import { NavPanelComponent } from './components/nav-panel/nav-panel.component';
import { DevicesComponent } from './components/devices/devices.component';
import { HomeComponent } from './components/home/home.component';
import { ScannersComponent } from './components/scanners/scanners.component';
import { AdminComponent } from './components/admin/admin.component';
import { PairsComponent } from './components/pairs/pairs.component';
import { PairUpdateDialogComponent } from './components/pair-update-dialog/pair-update-dialog.component';
import { PairCreateDialogComponent } from './components/pair-create-dialog/pair-create-dialog.component';
import { LoginComponent } from './components/login/login.component';


@NgModule({
  declarations: [
    AppComponent,
    NavPanelComponent,
    DevicesComponent,
    HomeComponent,
    ScannersComponent,
    AdminComponent,
    PairsComponent,
    PairUpdateDialogComponent,
    LoginComponent,
    PairCreateDialogComponent
  ],
  imports: [
    HttpClientModule,
    HttpClientJsonpModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FlexLayoutModule,
    MatToolbarModule,
    MatTabsModule,
    MatMenuModule,
    MatListModule,
    MatCardModule,
    MatButtonModule,
    MatTableModule,
    CdkTableModule,
    MatDialogModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatCheckboxModule,
    MatSidenavModule,
    MatIconModule,
  ],
  providers: [ SessionService, DeviceService, MatIconRegistry ],
  bootstrap: [ AppComponent ],
  entryComponents: [ 
    PairUpdateDialogComponent, 
    PairCreateDialogComponent 
  ]
})
export class AppModule { }
