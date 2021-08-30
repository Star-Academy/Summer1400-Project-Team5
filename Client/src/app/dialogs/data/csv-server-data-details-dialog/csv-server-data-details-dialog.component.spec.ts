import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CsvServerDataDetailsDialogComponent } from './csv-server-data-details-dialog.component';

describe('CsvServerDataDetailsDialogComponent', () => {
  let component: CsvServerDataDetailsDialogComponent;
  let fixture: ComponentFixture<CsvServerDataDetailsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CsvServerDataDetailsDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CsvServerDataDetailsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
