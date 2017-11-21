import { Component, OnInit } from '@angular/core';
import {Quote } from '../quote'
import { QuoteService } from '../quote.service';

@Component({
  selector: 'app-quote',
  templateUrl: './quote.component.html',
  styleUrls: ['./quote.component.css']
})
export class QuoteComponent implements OnInit {

  constructor(private quoteService: QuoteService) { }

  quote: Quote = {
    id: 1,
    text: 'this works !!'
  }

  getQuote(): void  {
    alert(this.quoteService.getQuote());
  }

  ngOnInit() {

  }

}
