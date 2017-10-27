
import React from 'react';
import InfoLabel from './InfoLabel.jsx';
import DebugLabel from './DebugLabel.jsx';
import {Nav, NavItem, Row, Col} from 'react-bootstrap'
import { Link } from 'react-router-dom'

export default class MasterPage extends React.Component {
  render() {
    return <div className="container"> 
         <Nav bsStyle="tabs" >
         <li>
            <Link to="/Page1">Home</Link>
          </li>
          <li>
            <Link to="/Page2">Page 2</Link>
          </li>
          <li>
            <Link to="/Page3">Page 3</Link>
          </li>
        </Nav>  
        <InfoLabel />
        {this.props.children}
        <DebugLabel/>
      </div>;
  }

}