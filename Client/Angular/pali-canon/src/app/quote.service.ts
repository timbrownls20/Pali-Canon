import 'rxjs/add/operator/map';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { Quote } from './quote'
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';

@Injectable()
export class QuoteService {

  private url: string = "http://localhost:49200/api/quote/dhp";

  constructor(private http: HttpClient) { }

  test(): string {
    return "hello from service"
  }



  getQuote(): Observable<Quote> {

    return this.http.get(this.url) 
    .map(res => { 
      
     
      let quote = new Quote();
      var response = (res as any);
      
      quote.title = response.title;
      quote.chapter = response.chapterNumber;
      quote.author = response.author;
      quote.book = response.book;

      if(response.verses.length > 0)
      {
          quote.text = response.verses[0].text;
          quote.verse = response.verses[0].verseNumber;
      }

      return quote;
  
    });

   

  }

}
