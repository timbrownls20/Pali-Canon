import { Component, OnInit } from '@angular/core';
import { BookService } from '../../services/book.service';
import { SettingsService } from '../../services/settings.service';
import { Book } from '../../model/book'
import { Settings } from '../../model/settings'

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  books: Book[];
  model: Settings

  constructor(private bookService:BookService, 
              private settingsService: SettingsService) { 
      this.bookService.list().subscribe(books => {
        this.books = books;

        this.model = new Settings();
        for(let book of this.books){
          this.model[book.code] = { available : true, book: book};
        }


      });
      
          
  }

  ngOnInit() {
  
  }

  saveSettings(){
    console.log("saving settings");
    return false;
  }

  get diagnostic() { return JSON.stringify(this.model); }
}


