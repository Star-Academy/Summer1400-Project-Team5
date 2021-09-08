import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PipesListPageComponent } from './pipes-list-page.component';

describe('PipesListPageComponent', () => {
  let component: PipesListPageComponent;
  let fixture: ComponentFixture<PipesListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PipesListPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PipesListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
