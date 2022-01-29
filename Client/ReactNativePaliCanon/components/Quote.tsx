import React, {useEffect, useState} from 'react';
import {StyleSheet, Text} from 'react-native';
import axios from 'axios';
import config from '../config';

class QuoteResponse {
  book!: string;
  text!: string;
  citation!: string;
  source!: string;
}

const Quote = () => {
  const [quote, setQuote] = useState<QuoteResponse>();

  useEffect(() => {
    console.log('use effect');

    const getQuote = () => {
      console.log('get quote');

      axios
        .get(config.api)
        .then(response => {
          setQuote(response.data);
        })
        .catch(e => {
          console.log(e);
        });
    };
    getQuote();
  }, []);

  const styles = StyleSheet.create({
    quote: {
      fontSize: 24,
      fontWeight: '600',
    },
  });

  return <Text style={styles.quote}>{quote?.text}</Text>;
};

export default Quote;
