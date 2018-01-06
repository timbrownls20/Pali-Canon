import { Component, OnInit } from '@angular/core';
import {Quote } from '../quote'
import { QuoteService } from '../quote.service';


@Component({
  selector: 'app-quote',
  templateUrl: './quote.component.html',  
  styleUrls: ['./quote.component.css']
})


export class QuoteComponent implements OnInit {

  quote: Quote; 

  constructor(private quoteService: QuoteService) { 
    this.quote = new Quote();    
  }

  getQuote(): void  {

    this.quoteService.getQuote()
        .subscribe(quote => this.quote = quote);

  }

  ngOnInit() {
     this.getQuote();
  }

}
