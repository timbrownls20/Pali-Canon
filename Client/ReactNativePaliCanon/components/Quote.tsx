import React from 'react';
import {StyleSheet, useColorScheme, View, Animated} from 'react-native';
import QuoteText from './QuoteText';

import {Colors} from 'react-native/Libraries/NewAppScreen';
import useQuote from '../hooks/useQuote';
import Citation from './Citation';
import {GestureDetector, Gesture} from 'react-native-gesture-handler';

const Quote = () => {
  const isDarkMode = useColorScheme() === 'dark';
  const {quote, nextQuote, fadeAnim} = useQuote();
  const gesture = Gesture.Pan()
    .onStart(() => {
      console.log('pan start');
    })
    .onEnd(e => {
      console.log(`pan end X:${e.translationX} Y:${e.translationY}`);
      nextQuote();
    });

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
    <GestureDetector gesture={gesture}>
      <Animated.View
        style={{...styles.topContainer, opacity: fadeAnim}}
        onTouchStart={() => console.log('touch start')}>
        <View />
        <View style={styles.quoteContainer}>
          <QuoteText text={quote?.text} />
        </View>
        <View>
          <Citation citation={quote?.citation} />
        </View>
      </Animated.View>
    </GestureDetector>
  );
};

export default Quote;
