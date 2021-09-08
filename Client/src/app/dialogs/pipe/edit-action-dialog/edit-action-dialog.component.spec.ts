import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditActionDialogComponent } from './edit-action-dialog.component';

describe('EditActionDialogComponent', () => {
  let component: EditActionDialogComponent;
  let fixture: ComponentFixture<EditActionDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditActionDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditActionDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
