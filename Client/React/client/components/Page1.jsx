import React from 'react';
import { connect } from 'react-redux';
import {Jumbotron, Button, Row, Col} from 'react-bootstrap'
import * as reduxActions from './redux/actions.redux.js';
import MasterPage from './MasterPage.jsx'
import { Link  } from 'react-router-dom'

var Page1 = class extends React.Component {

  showMessage(message){
    this.props.showMessage(message);
  }


  render() {
    return <MasterPage>
      
        <Jumbotron>
            <h1>Home Page</h1>
        </Jumbotron>
        
         <Row>
          <Col lgOffset={1}>
            <Button bsStyle="primary" onClick={() => this.showMessage('Danger, danger: high voltage')}>Panic</Button>
            <Button bsStyle="primary" onClick={() => this.showMessage('Calm down, calm down')}>No Panic</Button>
          </Col>
        </Row>
    </MasterPage>;
  }
}

// Maps state from store to props
const mapStateToProps = (state, ownProps) => {
  return {
    message: state.message
  }
};

// Maps actions to props
const mapDispatchToProps = (dispatch) => {
  return {
    showMessage: message => dispatch(reduxActions.showMessage(message))
  }
};

export default connect(mapStateToProps, mapDispatchToProps)(Page1);
//export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Page1));
