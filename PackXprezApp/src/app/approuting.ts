import { RouterModule, Routes } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { UpdatepackxComponent } from './updatepackx/updatepackx.component';
//import { HomeComponent } from './home/home.component';
const routes: Routes = [
  //{ path: '', component: HomeComponent },
  //{ path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'updatepackx', component: UpdatepackxComponent }
];
export const routing: ModuleWithProviders = RouterModule.forRoot(routes);
