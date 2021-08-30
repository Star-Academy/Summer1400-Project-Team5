import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HomeComponent} from './pages/home/home.component';
import {NavbarComponent} from './components/navbar/navbar.component';
import {FooterComponent} from './components/footer/footer.component';
import {LoginPageComponent} from './pages/login-page/login-page.component';
import {DataListPageComponent} from './pages/data-list-page/data-list-page.component';
import {PipesListPageComponent} from './pages/pipes-list-page/pipes-list-page.component';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {AddDataDialogComponent} from './dialogs/add-data-dialog/add-data-dialog.component';
import {MatDialogModule} from "@angular/material/dialog";
import { AddPipeDialogComponent } from './dialogs/add-pipe-dialog/add-pipe-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    FooterComponent,
    LoginPageComponent,
    DataListPageComponent,
    PipesListPageComponent,
    AddDataDialogComponent,
    AddPipeDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    BrowserAnimationsModule,
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [
    AddDataDialogComponent,
    AddPipeDialogComponent
  ]
})
export class AppModule {
}
