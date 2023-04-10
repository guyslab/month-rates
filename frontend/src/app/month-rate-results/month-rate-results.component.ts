import { Component, OnChanges, SimpleChanges, Input } from '@angular/core';
import * as Highcharts from 'highcharts';
import { MonthRateResultsModel } from './month-rate-results.model';

@Component({
  selector: 'month-rate-results',
  templateUrl: './month-rate-results.component.html',
  styleUrls: ['./month-rate-results.component.scss']
})
export class MonthRateResultsComponent implements OnChanges {
  Highcharts: typeof Highcharts = Highcharts;
  chartOptions: Highcharts.Options = {};
  @Input() results: MonthRateResultsModel;
  @Input() month: Date;

  constructor() {
    this.results = {
      GRAPH: {},
      max: 0,
      min: 0
    };
    this.month = new Date();
  }
  
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['results']) {
      this.onNewResults(changes['results'].currentValue);
    }
  }

  onNewResults(results: MonthRateResultsModel) : void {
    const dailyDates = Object.keys(results["GRAPH"]);
    const dailyRates: number[] = Object.values(results["GRAPH"]);

    this.chartOptions = {
      title: {
        text: `Exchange Rates for ${this.month.getFullYear()}/${this.month.getMonth() + 1}`
      },
      xAxis: {
        categories: dailyDates,
        title: {
          text: 'Date'
        }
      },
      yAxis: {
        title: {
          text: 'Exchange Rate'
        }
      },
      series: [
        {
          name: 'Exchange Rate',
          type: 'line',
          data: dailyRates
        }
      ]
    };
  }



}
