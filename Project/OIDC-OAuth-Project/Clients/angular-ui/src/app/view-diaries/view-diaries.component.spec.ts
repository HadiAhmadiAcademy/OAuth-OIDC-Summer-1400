import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewDiariesComponent } from './view-diaries.component';

describe('ViewDiariesComponent', () => {
  let component: ViewDiariesComponent;
  let fixture: ComponentFixture<ViewDiariesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewDiariesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewDiariesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
