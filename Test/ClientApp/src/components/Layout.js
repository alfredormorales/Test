import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
    constructor(props) {
        super(props)

        this.handleLogout = this.handleLogout.bind(this);
    }

    handleLogout = () => {
        this.props.handleLogout()
    }

  render () {
    return (
        <div>
        <NavMenu handleLogout={this.handleLogout} />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
