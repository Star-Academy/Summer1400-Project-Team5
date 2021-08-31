import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServerRequestDialogComponent } from './server-request-dialog.component';

describe('ServerRequestDialogComponent', () => {
  let component: ServerRequestDialogComponent;
  let fixture: ComponentFixture<ServerRequestDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServerRequestDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ServerRequestDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
