import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
//import { ui.bootstrap } from @'angular-bootstrap-npm/dist/'

import { AppComponent } from './app.component';
import { QuoteComponent } from './quote/quote.component';

import { QuoteService } from './quote.service';


@NgModule({
  declarations: [
    AppComponent,
    QuoteComponent
  ],
  imports: [
    BrowserModule,
    FormsModule
  ],
  providers: [QuoteService],
  bootstrap: [AppComponent]
})
export class AppModule { }
