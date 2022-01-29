import React from 'react';
import {StyleSheet, Text} from 'react-native';
import useQuote from '../hooks/useQuote';

const Quote = () => {
  const {quote} = useQuote();

  const styles = StyleSheet.create({
    quote: {
      fontSize: 20,
      fontWeight: '600',
    },
  });

  return <Text style={styles.quote}>{quote?.text}</Text>;
};

export default Quote;
