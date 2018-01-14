import { Component, OnInit } from '@angular/core';
import {Quote } from '../quote'
import { QuoteService } from '../quote.service';


@Component({
  selector: 'app-quote',
  templateUrl: './quote.component.html',  
  styleUrls: ['./quote.component.css', '../app.component.css']
})


export class QuoteComponent implements OnInit {

  quote: Quote; 

  constructor(private quoteService: QuoteService) { 
    this.quote = new Quote();    
  }

  randomQuote(): void  {

    this.quoteService.randomQuote()
        .subscribe(quote => this.quote = quote);

  }

  nextQuote(): void  {
    
      this.quoteService.nextQuote(this.quote)
          .subscribe(quote => this.quote = quote);
  
    }

  ngOnInit() {
     //this.randomQuote();
     this.quoteService.getQuote(19, 271)
     .subscribe(quote => this.quote = quote);
  
  }

}
