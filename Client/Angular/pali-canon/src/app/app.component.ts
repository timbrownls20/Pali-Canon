import { Component, NgModule, OnInit  } from '@angular/core';
import { SettingsService } from './services/settings.service';
import { BookService } from './services/book.service';
import { Settings } from './model/settings';

@Component({
  selector: 'app-root',
   templateUrl: './app.component.html',
   styleUrls: ['./app.component.css', ]
})

export class AppComponent {
  title = 'Pali Canon';

  constructor(private settingsService: SettingsService,
              private bookService: BookService){

  }

  ngOnInit() {
    this.bookService.list().subscribe(books => {

      let settings = new Settings();
      for(let book of books){
        settings[book.code] = { available : true, book: book};
      }

      this.settingsService.settings = settings;


    });
  }
}
