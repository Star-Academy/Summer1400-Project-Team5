import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { DataListPageComponent } from './pages/data-list-page/data-list-page.component';
import { PipesListPageComponent } from './pages/pipes-list-page/pipes-list-page.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AddDataDialogComponent } from './dialogs/data/add-data-dialog/add-data-dialog.component';
import { AddPipeDialogComponent } from './dialogs/pipe/add-pipe-dialog/add-pipe-dialog.component';
import { SQLServerDataDetailsDialogComponent } from './dialogs/data/sql-data-details-dialog/s-q-l-server-data-details-dialog.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { PipelineComponent } from './pages/pipeline/pipeline.component';
import { FormsModule } from '@angular/forms';
import { CSVDataDetailsDialogComponent } from './dialogs/data/csv-server-data-details-dialog/c-s-v-data-details-dialog.component';
import { ActionsListComponent } from './components/actions-list/actions-list.component';
import { ActionItemComponent } from './components/action-item/action-item.component';
import { EditActionDialogComponent } from './dialogs/pipe/edit-action-dialog/edit-action-dialog.component';
import { DataTableComponent } from './components/data-table/data-table.component';
import { ServerRequestDialogComponent } from './dialogs/server-request-dialog/server-request-dialog.component';
import { MaterialModule } from './material/material.module';
import { HttpClientModule } from '@angular/common/http';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { DragDropModule } from '@angular/cdk/drag-drop';

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
    SQLServerDataDetailsDialogComponent,
    NotFoundComponent,
    CSVDataDetailsDialogComponent,
    PipelineComponent,
    ActionsListComponent,
    ActionItemComponent,
    EditActionDialogComponent,
    DataTableComponent,
    ServerRequestDialogComponent,
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    BrowserAnimationsModule,
    FormsModule,
    MaterialModule,
    HttpClientModule,
    NgxChartsModule,
    DragDropModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [
    AddDataDialogComponent,
    AddPipeDialogComponent,
    SQLServerDataDetailsDialogComponent,
    EditActionDialogComponent,
    ServerRequestDialogComponent,
  ],
})
export class AppModule {}
