import 'rxjs/add/operator/map';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { Book } from '../model/book'
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable()
export class BookService {

  constructor(private http: HttpClient) {  }

  list(): Observable<Book[]> {
    
        let url = `${environment.apiEndpoint}book/list`;
    
        return this.http.get(url) 
        .map(res => { 
         
          return <Book[]>res;
         
        });
    
      }

  
}
