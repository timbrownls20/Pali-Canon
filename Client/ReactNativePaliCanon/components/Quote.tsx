import React from 'react';
import {StyleSheet, View, Animated} from 'react-native';
import QuoteText from './QuoteText';
import useQuote, {Mode} from '../hooks/useQuote';
import Citation from './Citation';
import {GestureDetector, Gesture} from 'react-native-gesture-handler';
import {Swipe} from '../services/Swipe';

const Quote = () => {
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
      flexDirection: 'column',
      justifyContent: 'space-between',
      alignItems: 'center',
      backgroundColor: 'rgba(52, 52, 52, 0)',
      flex: 1,
    },
    quoteContainer: {
      padding: 15,
      backgroundColor: 'rgba(52, 52, 52, 0.2)',
      borderRadius: 5,
      maxWidth: '90%',
    },
  });

  return (
    <GestureDetector gesture={gesture}>
      <View style={{...styles.topContainer}}>
        <View />
        <Animated.View style={{...styles.quoteContainer, opacity: fadeAnim}}>
          <QuoteText text={quote?.text} />
        </Animated.View>
        <Animated.View style={{opacity: fadeAnim}}>
          <Citation citation={quote?.citation} />
        </Animated.View>
      </View>
    </GestureDetector>
  );
};

export default Quote;
