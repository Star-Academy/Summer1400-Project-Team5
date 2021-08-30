import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataDetailsDialogComponent } from './data-details-dialog.component';

describe('DataDetailsDialogComponent', () => {
  let component: DataDetailsDialogComponent;
  let fixture: ComponentFixture<DataDetailsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DataDetailsDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DataDetailsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
