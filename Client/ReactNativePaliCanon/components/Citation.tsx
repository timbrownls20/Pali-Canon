import React from 'react';
import {StyleSheet, Text} from 'react-native';

interface ICitationProps {
  citation: string | undefined;
}

const styles = StyleSheet.create({
  citation: {
    margin: 32,
    fontSize: 9,
    color: 'white',
  },
});

const Citation = ({citation}: ICitationProps) => {
  return <Text style={styles.citation}>{citation}</Text>;
};

export default Citation;
