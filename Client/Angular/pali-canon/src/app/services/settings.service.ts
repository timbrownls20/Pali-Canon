import { Injectable } from '@angular/core';
import { Settings } from '../model/settings'
import { Book } from '../model/book';

@Injectable()
export class SettingsService {

  private _settings: Settings;

  get settings():Settings {
    return this._settings;
  }
  set settings(settings:Settings) {
      this._settings = settings;
  }
  get activeBooks(): Book[]{
    
    var activeBooks = new Array<Book>();
    for(let key in this._settings){
        
      if(this._settings[key].available){
        activeBooks.push(this._settings[key].book);
      }
    }
    return activeBooks;
  }


  getRandomBook(): Book{
    var activeBooks = this.activeBooks;
    let bookIndex = this.randomIntFromInterval(0, activeBooks.length -1);
    return activeBooks[bookIndex];
  }

  

  //.. TODO move to util function
  private randomIntFromInterval(min: number,max: number): number
  {
      return Math.floor(Math.random()*(max-min+1)+min);
  }

}
