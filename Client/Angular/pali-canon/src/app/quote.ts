export class Quote {
    id: number;
    text: string;
    title: string;
    author: string;
    chapter: number;
    verse: number;
    verseLast: number;
    book: string;

    get verseDisplay (): string{
    
      if(this.verseLast === null)
        return this.verse + "";
      else
        return this.verse + "-" + this.verseLast;
      
    }

  }