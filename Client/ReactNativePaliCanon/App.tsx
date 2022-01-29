import React from 'react';
import {
  SafeAreaView,
  ScrollView,
  StatusBar,
  StyleSheet,
  Text,
  useColorScheme,
  View,
} from 'react-native';
import Quote from './components/Quote';

import {Colors} from 'react-native/Libraries/NewAppScreen';

const App = () => {
  const isDarkMode = useColorScheme() === 'dark';

  let touchY: number;
  let touchX: number;
  const sampleQuote: string = 'Sample quote 1';

  const styles = StyleSheet.create({
    topContainer: {
      backgroundColor: isDarkMode ? Colors.black : Colors.white,
      flexDirection: 'column',
      justifyContent: 'center',
      alignItems: 'center',
      flex: 1,
    },
    timerContainer: {
      marginTop: 32,
      paddingHorizontal: 24,
    },
    timer: {
      fontSize: 24,
      fontWeight: '600',
    },
    backgroundStyle: {
      backgroundColor: isDarkMode ? Colors.darker : Colors.lighter,
      flex: 1,
    },
    scrollViewStyle: {
      backgroundColor: isDarkMode ? Colors.darker : Colors.lighter,
      height: '100%',
    },
    contentControllerStyle: {
      flex: 1,
      backgroundColor: 'yellow',
    },
  });

  return (
    <SafeAreaView style={styles.backgroundStyle}>
      <StatusBar barStyle={isDarkMode ? 'light-content' : 'dark-content'} />
      <ScrollView
        contentInsetAdjustmentBehavior="automatic"
        style={styles.scrollViewStyle}
        contentContainerStyle={styles.contentControllerStyle}>
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
          <View style={styles.timerContainer}>
            <Quote />
          </View>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

export default App;
