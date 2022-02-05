import axios from 'axios';
import {useEffect, useRef, useState} from 'react';
import {Animated} from 'react-native';
import config from '../config';
import {QuoteResponse} from '../model/QuoteResponse';
import {VerseResponse} from '../model/VerseResponse';

enum Phase {
  GetQuote = 1,
  ShowQuote = 2,
  HideQuote = 9,
}

export enum Mode {
  Start = 1,
  Stop = 2,
  Reset = 3,
}

const useQuote = () => {
  const bookCode = 'dhp';
  const [quote, setQuote] = useState<QuoteResponse>();
  const [mode, setMode] = useState<Mode>(Mode.Start);
  const fadeAnim = useRef(new Animated.Value(0)).current;

  const nextQuote = () => {
    const url = `${config.api}/sutta/next/${bookCode}/${quote?.verseNumber}`;
    console.log(url);
    axios
      .get<VerseResponse>(url)
      .then(response => {
        const nextQuoteResponse = new QuoteResponse().fromVerse(response.data);
        console.log(nextQuoteResponse);
        setQuote(nextQuoteResponse);
      })
      .catch(e => {
        console.log(e);
      });
  };

  const previousQuote = () => {
    //.. previous quote missing in API so workaround until API is changed
    let verseBeforePrevious: number = quote?.verseNumber! - 2;
    if (verseBeforePrevious === 0) {
      verseBeforePrevious = 0;
    }

    const url = `${config.api}/sutta/next/${bookCode}/${verseBeforePrevious}`;
    console.log(url);
    axios
      .get<VerseResponse>(url)
      .then(response => {
        const nextQuoteResponse = new QuoteResponse().fromVerse(response.data);
        console.log(nextQuoteResponse);
        setQuote(nextQuoteResponse);
      })
      .catch(e => {
        console.log(e);
      });
  };

  useEffect(() => {
    const getQuote = () => {
      const url = `${config.api}/quote/${bookCode}`;
      console.log(url);
      axios
        .get<QuoteResponse>(url)
        .then(response => {
          setQuote(response.data);
        })
        .catch(e => {
          console.log(e);
        });
    };

    const fadeIn = () => {
      fadeAnim.setValue(0);
      Animated.timing(fadeAnim, {
        toValue: 1,
        duration: config.interval,
        useNativeDriver: true,
      }).start();
    };

    const fadeOut = () => {
      fadeAnim.setValue(1);
      Animated.timing(fadeAnim, {
        toValue: 0,
        duration: config.interval,
        useNativeDriver: true,
      }).start();
    };

    const quoteCycle = () => {
      let phase = (count % 10) + 1;

      if (phase === Phase.GetQuote) {
        getQuote();
      } else if (phase === Phase.ShowQuote) {
        fadeIn();
      } else if (phase === Phase.HideQuote) {
        fadeOut();
      }

      count = count + 1;
    };

    let count: number = 0;

    let handler: NodeJS.Timer | null = null;

    if (mode === Mode.Start) {
      handler = setInterval(quoteCycle, config.interval);
    } else if (mode === Mode.Stop) {
      clearInterval(handler!);
    }

    return () => clearInterval(handler!);
  }, [fadeAnim, mode]);

  return {quote, nextQuote, previousQuote, fadeAnim, setMode};
};

export default useQuote;
