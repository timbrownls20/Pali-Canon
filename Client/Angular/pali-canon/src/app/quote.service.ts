import { Injectable } from '@angular/core';

@Injectable()
export class QuoteService {

  constructor() { }

  getQuote(): string {
    return "hello from service"
  }

}
