import axios from 'axios';
import {useEffect, useRef, useState} from 'react';
import {Animated} from 'react-native';
import config from '../config';
import {QuoteResponse} from '../model/QuoteResponse';

const useQuote = () => {
  const [quote, setQuote] = useState<QuoteResponse>();
  const fadeAnim = useRef(new Animated.Value(0)).current;

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

    const fadeIn = () => {
      fadeAnim.setValue(0);
      Animated.timing(fadeAnim, {
        toValue: 1,
        duration: config.interval / 4,
        useNativeDriver: true,
      }).start();
    };

    const handler = setInterval(() => {
      getQuote();
      fadeIn();
    }, config.interval);

    getQuote();
    fadeIn();

    return () => {
      clearInterval(handler);
    };
  }, [fadeAnim]);

  return {quote, fadeAnim};
};

export default useQuote;
