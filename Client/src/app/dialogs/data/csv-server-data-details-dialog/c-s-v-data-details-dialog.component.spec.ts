import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CSVDataDetailsDialogComponent } from './c-s-v-data-details-dialog.component';

describe('CsvServerDataDetailsDialogComponent', () => {
  let component: CSVDataDetailsDialogComponent;
  let fixture: ComponentFixture<CSVDataDetailsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CSVDataDetailsDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CSVDataDetailsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
