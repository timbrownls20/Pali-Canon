import { Injectable } from '@angular/core';

@Injectable()
export class SettingsService {

  bookCodes: string[]

  constructor() { 
    this.bookCodes = ["dhp", "thag"];
  }

}
