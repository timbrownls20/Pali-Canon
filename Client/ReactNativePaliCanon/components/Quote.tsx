import React from 'react';
import {StyleSheet, Text} from 'react-native';

const Quote = () => {
  const sampleQuote: string = 'Sample quote 2';

  const styles = StyleSheet.create({
    timer: {
      fontSize: 24,
      fontWeight: '600',
    },
  });

  return <Text style={styles.timer}>{sampleQuote}</Text>;
};

export default Quote;
