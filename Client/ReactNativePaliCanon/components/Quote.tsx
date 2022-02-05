import React from 'react';
import {StyleSheet, useColorScheme, View, Animated} from 'react-native';
import QuoteText from './QuoteText';

import {Colors} from 'react-native/Libraries/NewAppScreen';
import useQuote, {Mode} from '../hooks/useQuote';
import Citation from './Citation';
import {GestureDetector, Gesture} from 'react-native-gesture-handler';
import {Swipe} from '../services/Swipe';

const Quote = () => {
  const isDarkMode = useColorScheme() === 'dark';
  const {quote, nextQuote, previousQuote, fadeAnim, setMode} = useQuote();
  const gesture = Gesture.Pan()
    .onStart(() => {
      console.log('pan start');
      setMode(Mode.Stop);
      fadeAnim.setValue(1);
    })
    .onEnd(e => {
      const swipe = new Swipe(e.translationX, e.translationY);
      console.log(
        `pan end:: right swipe: ${swipe.isRightSwipe()},left ${swipe.isLeftSwipe()}`,
      );

      if (swipe.isRightSwipe()) {
        nextQuote();
      } else {
        previousQuote();
      }
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
