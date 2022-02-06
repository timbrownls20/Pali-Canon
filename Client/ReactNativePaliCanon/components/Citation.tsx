import React from 'react';
import {StyleSheet, Text} from 'react-native';
import {QuoteResponse} from '../model/QuoteResponse';

interface ICitationProps {
  quote: QuoteResponse | undefined;
}

const styles = StyleSheet.create({
  citation: {
    margin: 32,
    fontSize: 9,
    color: 'white',
  },
});

const Citation = ({quote}: ICitationProps) => {
  return (
    <Text style={styles.citation}>
      {quote ? `Verse ${quote.verseNumber}. ${quote.citation}` : ''}
    </Text>
  );
};

export default Citation;
