import {VerseResponse} from './VerseResponse';

export class QuoteResponse {
  public book!: string;
  public text!: string;
  public citation!: string;
  public source!: string;
  public verseNumber!: number;

  fromVerse(verse: VerseResponse): QuoteResponse {
    this.book = verse.book;
    this.citation = verse.citation;
    this.source = verse.source;

    if (verse.verses.length > 0) {
      this.text = verse.verses[0].text;
      this.verseNumber = verse.verses[0].verseNumber;
    }

    return this;
  }
}
