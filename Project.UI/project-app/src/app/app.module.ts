import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
 

import { AppRoutingModule } from './app-routing.module';

  
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTab } from '@angular/material/tabs/typings/tab';
import { MatMenuModule } from '@angular/material/menu';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { CdkTableModule } from '@angular/cdk/table';


import { AppComponent } from './app.component';
import { NavPanelComponent } from './components/nav-panel/nav-panel.component';
import { DevicesComponent } from './components/devices/devices.component';
import { HomeComponent } from './components/home/home.component';
import { ScannersComponent } from './components/scanners/scanners.component';
import { AdminComponent } from './components/admin/admin.component';
import { PairsComponent } from './components/pairs/pairs.component';


@NgModule({
  declarations: [
    AppComponent,
    NavPanelComponent,
    DevicesComponent,
    HomeComponent,
    ScannersComponent,
    AdminComponent,
    PairsComponent
  ],
  imports: [
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
    CdkTableModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
