import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


import { HomeComponent } from './home/home.component';
import { DevicesComponent } from './devices/devices.component';   
import { ScannersComponent } from './scanners/scanners.component';  
import { AdminComponent } from './admin/admin.component';


const routes: Routes = [
  // { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'devices', component: DevicesComponent },
  { path: 'scanners', component: ScannersComponent },
  { path: 'admin', component: AdminComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }