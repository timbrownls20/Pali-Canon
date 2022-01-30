import axios from 'axios';
import {useEffect, useRef, useState} from 'react';
import {Animated} from 'react-native';
import config from '../config';
import {QuoteResponse} from '../model/QuoteResponse';

const useQuote = () => {
  const [quote, setQuote] = useState<QuoteResponse>();
  const fadeAnim = useRef(new Animated.Value(0)).current;

  useEffect(() => {
    Animated.timing(fadeAnim, {
      toValue: 1,
      duration: config.interval / 2,
      useNativeDriver: true,
    }).start();
  }, [fadeAnim]);

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

  return {quote, fadeAnim};
};

export default useQuote;
