import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'truncatetext'
})
export class TruncateTextPipe implements PipeTransform {

  transform(value: string, length: number): string {

    //https://en.wikipedia.org/wiki/Longest_word_in_English
    const biggestWord = 50;
    const elipses = "...";


    if(typeof value === "undefined") return value;
    if(value.length <= length) return value;

    //.. truncate to about correct lenght
    let truncatedText = value.slice(0, length + biggestWord);
    
    //.. now nibble ends till correct length
    while (truncatedText.length > length - elipses.length) {
        let lastSpace = truncatedText.lastIndexOf(" ");

        if(lastSpace === -1) break;

        truncatedText = truncatedText.slice(0, lastSpace).replace(/[!,.?]$/, '');

    };

    console.log((truncatedText + elipses).length);

    return truncatedText + elipses;
  }

}
