
import React from 'react';
import JSONPretty from 'react-json-pretty';
import {Col, Row} from 'react-bootstrap'
import { connect } from 'react-redux';

const style = {paddingTop: 10}  

var DebugLabel = class extends React.Component {

  render() {
    return <Row style={style}>
          <Col>
            <JSONPretty id="json-pretty" json={this.props.debug}></JSONPretty>
          </Col>
        </Row>;
  }
}


const mapStateToProps = (state, ownProps) => {
    return {
      // You can now say this.props.books
      debug: state
    }
  };
  
  // Use connect to put them together
  export default connect(mapStateToProps)(DebugLabel);