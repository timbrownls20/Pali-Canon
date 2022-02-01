export class VerseResponse {
  book!: string;
  verses!: [
    {
      verseNumber: number;
      text: string;
    },
  ];
  citation!: string;
  source!: string;
}
