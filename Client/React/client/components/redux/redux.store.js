// ./src/store/configureStore.js
import {createStore} from 'redux';
import rootReducer from './reducer.redux.js';

export default function configureStore(initialState) {
  return createStore(rootReducer, initialState);
}