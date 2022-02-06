import React from 'react';
import {StyleSheet, Text} from 'react-native';

interface IQuoteProps {
  text: string | undefined;
}

const Quote = ({text}: IQuoteProps) => {
  const styles = StyleSheet.create({
    quote: {
      fontSize: 20,
      fontWeight: '600',
      color: 'white',
    },
  });

  return <Text style={styles.quote}>{text}</Text>;
};

export default Quote;
