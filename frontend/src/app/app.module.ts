import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HighchartsChartModule } from 'highcharts-angular';
import { LuxonModule } from 'luxon-angular';

import { AppComponent } from './app.component';
import { MonthRateQueryComponent } from './month-rate-query/month-rate-query.component';
import { MonthRateResultsComponent } from './month-rate-results/month-rate-results.component';
import { AppConfigService } from './appconfig.service';

@NgModule({
  declarations: [
    AppComponent,
    MonthRateQueryComponent,
    MonthRateResultsComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    MatSelectModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    HighchartsChartModule,
    LuxonModule
  ],
  providers: [
    { 
      provide : APP_INITIALIZER, 
      multi : true, 
       deps : [AppConfigService], 
       useFactory : (appConfigService : AppConfigService) =>  () => appConfigService.loadAppConfig()
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
