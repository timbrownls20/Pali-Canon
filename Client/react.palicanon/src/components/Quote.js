import React, {useEffect, useState, useRef} from 'react';
import axios from 'axios'

const Quote = () => {

    const interval = 4000;
    const [quote, setQuote] = useState({});
    const [quoteVisible, setQuoteVisible] = useState(true);
    const quoteVisibleRef = useRef(true);

    const getQuote = () => {
        axios.get('http://palicanon.codebuckets.com.au/api/quote')
        .then(response => {
        setQuote(response.data);
        });
    }

    const showQuote = (show) => {
        quoteVisibleRef.current = show;    
        setQuoteVisible(quoteVisibleRef.current);
    }

    useEffect(() => {
        let count = 0;
        getQuote();
        
        setInterval(() => {
            count = count + 1;
            let phase = count % 5 + 1;

            console.log(phase);

            if(phase === 1){
                showQuote(true)
            }
            else if(phase === 4){
                showQuote(false)
            }
            else if(phase === 5) {
                getQuote();
            }
        } ,interval);
    }, []);

return <div className="d-flex flex-column justify-content-between align-items-center quote-background">
            <div class="heading d-flex justify-content-end">
                <small>{quote.book} quotes</small>
            </div>
            <div className={"quote-container" + (quoteVisible ? "" : " hidden")}>
                <h5 className={"quote" + (quoteVisible ? "" : " hidden")}>{quote.text}</h5>
            </div>
            <div class="citation d-flex justify-content-end">
                <small className={"quote" + (quoteVisible ? "" : " hidden")}><a href={quote.source}>{quote.citation}</a></small>
            </div>
        </div>
}

export default Quote;