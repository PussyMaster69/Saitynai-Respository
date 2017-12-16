import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { DevicesComponent } from './components/devices/devices.component';   
import { ScannersComponent } from './components/scanners/scanners.component';  
import { AdminComponent } from './components/admin/admin.component';


const routes: Routes = [
  // { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'devices', component: DevicesComponent },
  { path: 'scanners', component: ScannersComponent },
  { path: 'login', component: LoginComponent },
  { path: 'admin', component: AdminComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }