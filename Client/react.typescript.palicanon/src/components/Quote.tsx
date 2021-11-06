import React, {useEffect, useState, useRef} from 'react';
import axios from 'axios';
import _ from 'lodash';
import config from '../config';

class QuoteResponse {

    book: string;
    text: string;
    citation: string;
    source: string;
}

interface ImageStyle {
    backgroundImage: string | undefined
}

enum Phase {
    GetQuote = 1,
    ShowQuote = 2,
    HideQuote = 9
}

const Quote = () => {

    const [quote, setQuote]: [QuoteResponse, Function] = useState({} as QuoteResponse);
    const [quoteVisible, setQuoteVisible]: [boolean, Function] = useState(false);
    const quoteVisibleRef: React.MutableRefObject<boolean> = useRef(false);
    const imageStyleRef: React.MutableRefObject<ImageStyle> = useRef({} as ImageStyle);

    const showQuote = (show: boolean)=> {
        quoteVisibleRef.current = show;    
        setQuoteVisible(quoteVisibleRef.current);
    }

    useEffect(() => {
   
        const getQuote = () => {
            axios.get(config.api)
            .then(response => {
            setQuote(response.data);
            });
        }
        let count: number = 0;
        getQuote();
        
        setInterval(() => {
            count = count + 1;
            let phase = count % 10 + 1;

            if(phase === Phase.GetQuote){
                getQuote();
            }
            else if(phase === Phase.ShowQuote){
                showQuote(true)
            }
            else if(phase === Phase.HideQuote) {
                showQuote(false)
            }
        } ,config.interval);
    }, []);

    useEffect(() => {
        let imageCount:number = 10;
        let imageNumber:number = config.imageNumber === null ?  _.random(1, imageCount) : config.imageNumber;
        let image = require(`../assets/images/background${imageNumber}.jpg`);
        imageStyleRef.current = {
            backgroundImage:"url(" + image.default + ")"
        }
    }, [])

return <div className="d-flex flex-column justify-content-between align-items-center quote-background" style={imageStyleRef.current}>
            <div className="heading d-flex justify-content-end">
                <small>Buddha quotes</small>
            </div>
            <div className={"quote" + (quoteVisible ? "" : " hidden")}>
                <div className={"fadein-text" + (quoteVisible ? "" : " hidden")}>{quote.text}</div>
            </div>
            <div className="citation d-flex justify-content-end">
                <a href={quote.source}><small className={"fadein-text" + (quoteVisible ? "" : " hidden")}>{quote.citation}</small></a>
            </div>
        </div>
}

export default Quote;