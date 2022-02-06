import React from 'react';
import {
  SafeAreaView,
  ScrollView,
  StatusBar,
  StyleSheet,
  useColorScheme,
} from 'react-native';
import Quote from './components/Quote';
import {GestureHandlerRootView} from 'react-native-gesture-handler';
import BackgroundImage from './components/BackgroundImage';

const App = () => {
  const isDarkMode = useColorScheme() === 'dark';
  const styles = StyleSheet.create({
    backgroundStyle: {
      flex: 1,
    },
    scrollViewStyle: {
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
          <BackgroundImage>
            <Quote />
          </BackgroundImage>
        </ScrollView>
      </GestureHandlerRootView>
    </SafeAreaView>
  );
};

export default App;
