import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SQLServerDataDetailsDialogComponent } from './s-q-l-server-data-details-dialog.component';

describe('DataDetailsDialogComponent', () => {
  let component: SQLServerDataDetailsDialogComponent;
  let fixture: ComponentFixture<SQLServerDataDetailsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SQLServerDataDetailsDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SQLServerDataDetailsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
