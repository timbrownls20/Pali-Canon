import 'rxjs/add/operator/map';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { Quote } from '../model/quote'
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable()
export class QuoteService {

  
  constructor(private http: HttpClient) { }

  
   getQuote(chapterId:number, verseId:number): Observable<Quote> {
    
        let url = `${environment.apiEndpoint}/sutta/dhp/${chapterId}/${verseId}`;
    
        return this.http.get(url) 
        .map(res => { 
         
          return this.mapResponse(res[0]);
         
        });
    
      }

  randomQuote(): Observable<Quote> {

    let url = environment.apiEndpoint + "/quote";

    return this.http.get(url) 
    .map(res => { 
      
      return this.mapResponse(res);
     
    });

  }

  nextQuote(quote: Quote): Observable<Quote> {
   
        let url = `${environment.apiEndpoint}quote/next/${quote.bookCode}/${quote.chapter}/${quote.verse}` ;
    
        return this.http.get(url) 
        .map(res => { 
          
          return this.mapResponse(res);
         
        });
    
      }
  
  private mapResponse(response: any): Quote{
    let quote = new Quote();
      
    quote.title = response.title;
    quote.chapter = response.chapterNumber;
    quote.author = response.author;
    quote.book = response.book;
    quote.bookCode = response.bookCode;

    if(response.verses.length > 0)
    {
        quote.text = response.verses[0].text;
        quote.verse = response.verses[0].verseNumber;
        quote.verseLast = response.verses[0].verseNumberLast;
    }

    return quote;

  }
    

}
