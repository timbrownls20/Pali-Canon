import { Component, OnInit, NgModule } from '@angular/core';
import {Quote } from '../quote'
import { QuoteService } from '../quote.service';
//import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

// @NgModule({
//   declarations: [NgbModule],
//   imports: [NgbModule]
// })

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

    //alert(this.quote.text);
  }

  ngOnInit() {
     this.getQuote();
  }

}
