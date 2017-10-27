
import React from 'react';
import {Alert} from 'react-bootstrap'
import { connect } from 'react-redux';

var InfoLabel = class extends React.Component {
  render() {

    if(this.props.message){

      return <Alert bsStyle="danger">
        {this.props.message}
      </Alert>;
    }
    else {
      return null;
    }
  }
}

const mapStateToProps = (state, ownProps) => {

    return {
         message: state.message
    }
  };
  
 
  export default connect(mapStateToProps)(InfoLabel);