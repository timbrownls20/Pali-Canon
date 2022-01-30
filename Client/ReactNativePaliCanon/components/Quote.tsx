import React, {useRef, useEffect} from 'react';
import {StyleSheet, useColorScheme, View, Animated} from 'react-native';
import QuoteText from './QuoteText';

import {Colors} from 'react-native/Libraries/NewAppScreen';
import useQuote from '../hooks/useQuote';
import Citation from './Citation';

const Quote = () => {
  const isDarkMode = useColorScheme() === 'dark';
  const {quote} = useQuote();
  const fadeAnim = useRef(new Animated.Value(0)).current;

  useEffect(() => {
    Animated.timing(fadeAnim, {
      toValue: 1,
      duration: 10000,
      useNativeDriver: true,
    }).start();
  }, [fadeAnim]);

  let touchY: number;
  let touchX: number;

  const styles = StyleSheet.create({
    topContainer: {
      backgroundColor: isDarkMode ? Colors.black : Colors.white,
      flexDirection: 'column',
      justifyContent: 'space-between',
      alignItems: 'center',
      flex: 1,
    },
    quoteContainer: {
      marginTop: 32,
      paddingHorizontal: 24,
    },
  });

  return (
    <Animated.View
      style={{...styles.topContainer, opacity: fadeAnim}}
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
      <View />
      <View style={styles.quoteContainer}>
        <QuoteText text={quote?.text} />
      </View>
      <View>
        <Citation citation={quote?.citation} />
      </View>
    </Animated.View>
  );
};

export default Quote;
