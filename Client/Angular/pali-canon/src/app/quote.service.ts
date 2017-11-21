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

    debugger;

    return this.http.get(this.url) 
    .map(res => { 
      
      let quote = new Quote();
      var response = (res as any);
      
      if(response.verses.length > 0)
      {
          quote.text = response.verses[0].text;
      }

      return quote;
  
    });

    // return this.http.get(this.url). 
    // map((res: HttpResponse<any>) => 
    
    //   res.body.json().results.map(item => {

    //   debugger;

    //   let quote = new Quote();
    //   if(item.verses.length > 0)
    //   {
    //       quote.text = item.verses[0].text;
    //   }      
    //   return quote;
    // }
    // )); 


    // .map(res => { 
    //   return res.json().results.map(item => { 

    //     return this.url; 

    //     // return new SearchItem( 
    //     //     item.trackName,
    //     //     item.artistName,
    //     //     item.trackViewUrl,
    //     //     item.artworkUrl30,
    //     //     item.artistId
    //     // );
    //   });
    // });


  }

}
