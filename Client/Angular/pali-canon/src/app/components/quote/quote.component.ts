import { Component, OnInit } from '@angular/core';

import {Quote } from '../../model/quote'
import { Settings } from '../../model/settings'

import { QuoteService } from '../../services/quote.service';
import { SettingsService } from '../../services/settings.service';
import { BookService } from '../../services/book.service';



@Component({
  selector: 'app-quote',
  templateUrl: './quote.component.html',  
  styleUrls: ['./quote.component.css']
})


export class QuoteComponent implements OnInit {

  quote: Quote; 

  constructor(private quoteService: QuoteService,
              private settingsService: SettingsService,
              private bookService: BookService) { 
    this.quote = new Quote();    
  }

  randomQuote(): void  {

    var randomBook = this.settingsService.getRandomBook();

    if(randomBook !== undefined){
      this.quoteService.randomQuote(randomBook.code)
      .subscribe(quote => this.quote = quote);
    }


  }

  nextQuote(): void  {
    
      this.quoteService.nextQuote(this.quote)
          .subscribe(quote => this.quote = quote);
  
    }

  ngOnInit() {

    debugger;

    if(this.settingsService.settings === undefined) {
       //.. TODO move this initalisation code to app module
      this.bookService.list().subscribe(books => {
      
            let settings = new Settings();
            for(let book of books){
              settings[book.code] = { available : true, book: book};
            }
      
            this.settingsService.settings = settings;
            this.randomQuote();
      
          });
    }
    else{
      this.randomQuote();
    }

   
    
    //  this.quoteService.getQuote(19, 271)
    //  .subscribe(quote => this.quote = quote);
  
  }

}
