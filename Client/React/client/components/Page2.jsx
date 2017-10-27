import React from 'react';
import { connect } from 'react-redux';
import {Jumbotron, Button, Row, Col} from 'react-bootstrap'
import * as reduxActions from './redux/actions.redux.js';
import MasterPage from './MasterPage.jsx'

var Page2 = class extends React.Component {

  showMessage(message){
    this.props.showMessage(message);
  }


  render() {
    return <MasterPage>
        <Jumbotron>
            <h1>Page 2</h1>
        </Jumbotron>
         <Row>
          <Col lgOffset={1}>
            <Button bsStyle="primary">Next</Button>
            <Button bsStyle="primary">Back</Button>
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

//export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Page2));
export default connect(mapStateToProps, mapDispatchToProps)(Page2);
//export default Page2;