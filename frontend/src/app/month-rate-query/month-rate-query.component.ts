import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'month-rate-query',
  templateUrl: './month-rate-query.component.html',
  styleUrls: ['./month-rate-query.component.scss']
})
export class MonthRateQueryComponent {
  @Output() monthSelected = new EventEmitter<Date>();
  selectedMonth: Date;
  months: Date[] = [];

  constructor() {
    this.setPastMonths(360);
    this.selectedMonth = new Date();
  }

  onSubmit() {
    this.monthSelected.emit(this.selectedMonth);
  }

  setPastMonths(numOfMonths: number) {
    var currentDate = new Date();

    for (var i = 0; i < numOfMonths; i++) {
      this.months.push(new Date(currentDate));
      currentDate.setMonth(currentDate.getMonth() - 1);
    }
  }
}
