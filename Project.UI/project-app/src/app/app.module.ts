import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
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
  MatInputModule 
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
    LoginComponent
  ],
  imports: [
    HttpClientModule,
    HttpClientJsonpModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
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
    MatInputModule,
  ],
  providers: [ SessionService, DeviceService ],
  bootstrap: [ AppComponent ],
  entryComponents: [ PairUpdateDialogComponent ]
})
export class AppModule { }
