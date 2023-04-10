import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthRateQueryComponent } from './month-rate-query.component';

describe('MonthRateQueryComponent', () => {
  let component: MonthRateQueryComponent;
  let fixture: ComponentFixture<MonthRateQueryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MonthRateQueryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonthRateQueryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
