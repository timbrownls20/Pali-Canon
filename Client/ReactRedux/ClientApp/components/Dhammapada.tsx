import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState }  from '../store';
import { Link, RouteComponentProps } from 'react-router-dom';
import * as CounterStore from '../store/Counter';


type CounterProps =
CounterStore.CounterState
& typeof CounterStore.actionCreators
& RouteComponentProps<{}>;

class Dhammapada extends React.Component<CounterProps, {}>  {
    public render() {
        return <div>
            <h1>Dhammapada</h1>
        </div>;
    }
}

export default connect(
    (state: ApplicationState) => state.counter, // Selects which state properties are merged into the component's props
    CounterStore.actionCreators                 // Selects which action creators are merged into the component's props
)(Dhammapada) as typeof Dhammapada;
