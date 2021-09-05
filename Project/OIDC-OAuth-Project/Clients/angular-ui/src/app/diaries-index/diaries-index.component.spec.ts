import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiariesIndexComponent } from './diaries-index.component';

describe('DiariesIndexComponent', () => {
  let component: DiariesIndexComponent;
  let fixture: ComponentFixture<DiariesIndexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DiariesIndexComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DiariesIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
