import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MonthRateResultsModel } from './month-rate-results/month-rate-results.model';
import { Observable } from 'rxjs';
import { DateTimeFromJsDatePipe , DateTimeToFormatPipe } from 'luxon-angular';
import { AppConfigService } from './appconfig.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Exchange Rates App';
  selectedMonth: Date;
  hasResults: boolean = false;
  results: MonthRateResultsModel;
  loading: boolean = false;

  constructor(private http: HttpClient, private appconfig: AppConfigService) {    
    this.results = {
      GRAPH: {},
      max: 0,
      min: 0
    }
    this.selectedMonth = new Date();
  }

  onMonthSelected(monthToQuery: Date): void {
    this.selectedMonth = monthToQuery;
    const jsToDateTimePipe = new DateTimeFromJsDatePipe();
    const dateTimeToFormatPipe = new DateTimeToFormatPipe();
    const monthParam = dateTimeToFormatPipe.transform(jsToDateTimePipe.transform(monthToQuery), "yyMM");
    this.loading = true;
    this.getExchangeRates(monthParam).subscribe({
      next: (data: MonthRateResultsModel) => {
        this.results = data;
        this.hasResults = true;
        this.loading = false;
      }, 
      error: (e) => {
        this.loading=false;
        alert("Error getting rates");
      }
    });
  }

  getExchangeRates(month: string): Observable<MonthRateResultsModel> {
    const baseUrl = this.appconfig.apiBaseUrl;
    const url = `${baseUrl}/Currency/${month}`;
    return this.http.get<MonthRateResultsModel>(url);
  }  
  
}
