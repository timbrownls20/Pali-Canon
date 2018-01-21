import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

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

  constructor(
              private route: ActivatedRoute,
              private router: Router,
              private quoteService: QuoteService,
              private settingsService: SettingsService,
              private bookService: BookService) { 
    this.quote = new Quote();    
  }



  ngOnInit() {

    console.log(this.router.url);

    if(this.router.url === '/next'){
      this.nextQuote();
    }
    else{
      //.. TODO move this initalisation code to app module
      if(this.settingsService.settings === undefined) {
        
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
    }

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

}
