import React from 'react';
import {ImageBackground, StyleSheet} from 'react-native';

const BackgroundImage = ({children}: {children: React.ReactNode}) => {
  return (
    <ImageBackground
      source={require('../assets/images/background1.jpg')}
      style={styles.container}>
      {children}
    </ImageBackground>
  );
};

var styles = StyleSheet.create({
  container: {
    flex: 1,
  },
});

export default BackgroundImage;
