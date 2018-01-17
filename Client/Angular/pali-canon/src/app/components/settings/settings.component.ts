import { Component, OnInit } from '@angular/core';
import { BookService } from '../../services/book.service';
import { Book } from '../../model/book'

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  books: Book[];

  constructor(private bookService:BookService) { }

  ngOnInit() {
    this.bookService.list().subscribe(books => this.books = books);;
  }


}
