import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import {DataListPageComponent} from "./pages/data-list-page/data-list-page.component";

// TODO: 404 Page!
export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'data', component: DataListPageComponent },
  { path: 'home', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
