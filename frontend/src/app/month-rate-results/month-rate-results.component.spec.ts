import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthRateResultsComponent } from './month-rate-results.component';

describe('MonthRateResultsComponent', () => {
  let component: MonthRateResultsComponent;
  let fixture: ComponentFixture<MonthRateResultsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MonthRateResultsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonthRateResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
