import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { DataListPageComponent } from './pages/data-list-page/data-list-page.component';
import {PipesListPageComponent} from "./pages/pipes-list-page/pipes-list-page.component";
import { NotFoundComponent } from './pages/not-found/not-found.component';
// TODO: 404 Page!
export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', redirectTo: '', pathMatch: 'full' },
  { path: 'login', component: LoginPageComponent },
  { path: 'data', component: DataListPageComponent },
  { path: 'pipes', component: PipesListPageComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { anchorScrolling: 'enabled' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
