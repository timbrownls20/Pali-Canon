import axios from 'axios';
import {useEffect, useState} from 'react';
import config from '../config';
import {QuoteResponse} from '../model/QuoteResponse';

const useQuote = () => {
  const [quote, setQuote] = useState<QuoteResponse>();

  useEffect(() => {
    const getQuote = () => {
      axios
        .get(config.api)
        .then(response => {
          setQuote(response.data);
        })
        .catch(e => {
          console.log(e);
        });
    };

    const handler = setInterval(() => {
      getQuote();
    }, config.interval);

    getQuote();

    return () => {
      clearInterval(handler);
    };
  }, []);

  return {quote};
};

export default useQuote;
