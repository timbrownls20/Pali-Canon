import React, {useEffect, useState, useRef} from 'react';
import axios from 'axios'

interface QuoteResponse {

    book: string | undefined
    text: string | undefined,
    citation: string | undefined,
    source: string | undefined
}

enum Phase {
    GetQuote = 1,
    ShowQuote = 2,
    HideQuote = 9
}

const Quote = () => {

    const url: string = 'http://palicanon.codebuckets.com.au/api/quote';
    const interval: number = 1500;
    const [quote, setQuote]: [QuoteResponse, Function] = useState({} as QuoteResponse);
    const [quoteVisible, setQuoteVisible]: [boolean, Function] = useState(false);
    const quoteVisibleRef: React.MutableRefObject<boolean> = useRef(false);

    const getQuote = () => {
        axios.get(url)
        .then(response => {
        setQuote(response.data);
        });
    }

    const showQuote = (show: boolean)=> {
        quoteVisibleRef.current = show;    
        setQuoteVisible(quoteVisibleRef.current);
    }

    useEffect(() => {
        let count: number = 0;
        getQuote();
        
        setInterval(() => {
            count = count + 1;
            let phase = count % 10 + 1;

            console.log(phase);

            if(phase === Phase.GetQuote){
                getQuote();
            }
            else if(phase === Phase.ShowQuote){
                showQuote(true)
            }
            else if(phase === Phase.HideQuote) {
                showQuote(false)
            }
        } ,interval);
    }, []);

return <div className="d-flex flex-column justify-content-between align-items-center quote-background">
            <div className="heading d-flex justify-content-end">
                <small>{quote.book} quotes</small>
            </div>
            <div className={"quote-container" + (quoteVisible ? "" : " hidden")}>
                <h5 className={"quote" + (quoteVisible ? "" : " hidden")}>{quote.text}</h5>
            </div>
            <div className="citation d-flex justify-content-end">
                <small className={"quote" + (quoteVisible ? "" : " hidden")}><a href={quote.source}>{quote.citation}</a></small>
            </div>
        </div>
}

export default Quote;