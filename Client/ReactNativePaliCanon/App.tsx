import React from 'react';
import {
  SafeAreaView,
  ScrollView,
  StatusBar,
  StyleSheet,
  useColorScheme,
} from 'react-native';
import Quote from './components/Quote';

import {Colors} from 'react-native/Libraries/NewAppScreen';
import {GestureHandlerRootView} from 'react-native-gesture-handler';

const App = () => {
  const isDarkMode = useColorScheme() === 'dark';
  const styles = StyleSheet.create({
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
    },
  });

  return (
    <SafeAreaView style={styles.backgroundStyle}>
      <StatusBar barStyle={isDarkMode ? 'light-content' : 'dark-content'} />
      <GestureHandlerRootView>
        <ScrollView
          contentInsetAdjustmentBehavior="automatic"
          style={styles.scrollViewStyle}
          contentContainerStyle={styles.contentControllerStyle}>
          <Quote />
        </ScrollView>
      </GestureHandlerRootView>
    </SafeAreaView>
  );
};

export default App;
