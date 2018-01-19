import {Book } from './book'

export class Settings {
    [key: string]: {
        available: boolean,
        book: Book
    };
  }