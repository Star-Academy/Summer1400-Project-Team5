import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPipeDialogComponent } from './add-pipe-dialog.component';

describe('AddPipeDialogComponent', () => {
  let component: AddPipeDialogComponent;
  let fixture: ComponentFixture<AddPipeDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddPipeDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddPipeDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
