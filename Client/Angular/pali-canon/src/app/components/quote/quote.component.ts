import { Component, OnInit } from '@angular/core';
import {Quote } from '../../model/quote'
import { QuoteService } from '../../services/quote.service';
import { SettingsService } from '../../services/settings.service';


@Component({
  selector: 'app-quote',
  templateUrl: './quote.component.html',  
  styleUrls: ['./quote.component.css']
})


export class QuoteComponent implements OnInit {

  quote: Quote; 

  constructor(private quoteService: QuoteService,
              private settingsService: SettingsService) { 
    this.quote = new Quote();    
  }

  randomQuote(): void  {

    var randomBook = this.settingsService.getRandomBook();

    this.quoteService.randomQuote(randomBook.code)
        .subscribe(quote => this.quote = quote);

  }

  nextQuote(): void  {
    
      this.quoteService.nextQuote(this.quote)
          .subscribe(quote => this.quote = quote);
  
    }

  ngOnInit() {
     this.randomQuote();
    //  this.quoteService.getQuote(19, 271)
    //  .subscribe(quote => this.quote = quote);
  
  }

}
