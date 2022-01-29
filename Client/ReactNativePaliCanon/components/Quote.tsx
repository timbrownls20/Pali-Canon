import React from 'react';
import {StyleSheet, useColorScheme, View} from 'react-native';
import QuoteText from './QuoteText';

import {Colors} from 'react-native/Libraries/NewAppScreen';

const Quote = () => {
  const isDarkMode = useColorScheme() === 'dark';

  let touchY: number;
  let touchX: number;

  const styles = StyleSheet.create({
    topContainer: {
      backgroundColor: isDarkMode ? Colors.black : Colors.white,
      flexDirection: 'column',
      justifyContent: 'center',
      alignItems: 'center',
      flex: 1,
    },
    quoteContainer: {
      marginTop: 32,
      paddingHorizontal: 24,
    },
  });

  return (
    <View
      style={styles.topContainer}
      onTouchStart={e => {
        touchX = e.nativeEvent.pageX;
        touchY = e.nativeEvent.pageY;
      }}
      onTouchEnd={e => {
        if (
          Math.abs(touchY - e.nativeEvent.pageY) > 20 ||
          Math.abs(touchX - e.nativeEvent.pageX) > 20
        ) {
          console.log('swiped');
        } else {
          console.log('touched');
        }
      }}>
      <View style={styles.quoteContainer}>
        <QuoteText />
      </View>
    </View>
  );
};

export default Quote;
