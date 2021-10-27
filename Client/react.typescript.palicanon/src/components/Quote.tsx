import React, {useEffect, useState, useRef} from 'react';
import axios from 'axios';
import _ from 'lodash';

interface QuoteResponse {

    book: string | undefined
    text: string | undefined,
    citation: string | undefined,
    source: string | undefined
}

interface ImageStyle {
    backgroundImage: string | undefined
}

interface TextStyle {
    color: string | undefined
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
    const imageStyleRef: React.MutableRefObject<ImageStyle> = useRef({} as ImageStyle);
    const textStyleRef: React.MutableRefObject<TextStyle> = useRef({} as TextStyle);
    
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

    useEffect(() => {

        const imageTextStyleArray: Array<string> = ["white", "white", "white", "white", "white", "white", "white", "white", "white", "white"]
        let imageCount: number = 10;
        let imageNumber: number = _.random(1, imageCount);
        //let imageNumber: number = 10;
        let image = require(`../assets/images/background${imageNumber}.jpg`);
        imageStyleRef.current = {
            backgroundImage:"url(" + image.default + ")"
        }

        textStyleRef.current = {
            color: imageTextStyleArray[imageNumber - 1]
        }

    }, [])

return <div className="d-flex flex-column justify-content-between align-items-center quote-background" style={imageStyleRef.current}>
            <div className="heading d-flex justify-content-end">
                <small style={textStyleRef.current}>Buddha quotes</small>
            </div>
            <div className={"quote-container" + (quoteVisible ? "" : " hidden")}>
                <h5 className={"quote" + (quoteVisible ? "" : " hidden")}>{quote.text}</h5>
            </div>
            <div className="citation d-flex justify-content-end">
                <small className={"quote" + (quoteVisible ? "" : " hidden")}><a href={quote.source} style={textStyleRef.current}>{quote.citation}</a></small>
            </div>
        </div>
}

export default Quote;