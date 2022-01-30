import axios from 'axios';
import {useEffect, useRef, useState} from 'react';
import {Animated} from 'react-native';
import config from '../config';
import {QuoteResponse} from '../model/QuoteResponse';

enum Phase {
  GetQuote = 1,
  ShowQuote = 2,
  HideQuote = 9,
}

const useQuote = () => {
  const [quote, setQuote] = useState<QuoteResponse>();
  //const [count, setCount] = useState<number>(0);
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

    let count: number = 0;

    const handler = setInterval(() => {
      let phase = (count % 10) + 1;

      console.log(`count ${count} phase ${phase}`);

      if (phase === Phase.GetQuote) {
        getQuote();
      } else if (phase === Phase.ShowQuote) {
        fadeIn();
      } else if (phase === Phase.HideQuote) {
        fadeOut();
      }

      count = count + 1;

      // getQuote();
      // fadeIn();
    }, config.interval);

    // getQuote();
    // fadeIn();

    return () => {
      clearInterval(handler);
    };
  }, [fadeAnim]);

  return {quote, fadeAnim};
};

export default useQuote;
