import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Receipt } from './components/Receipt';
import Login from "./components/Login";

export default class App extends Component {
    static displayName = App.name;

    constructor() {
        super();

        this.state = {
            logged_status : "NOT_LOG_IN",
            logged_In: false,
            error_message: "",
            user: {

            }
        }

        this.handleLogin = this.handleLogin.bind(this);
    }

    handleLogin = (data) => {
        this.setState ({
            logged_status: "LOG_IN",
            logged_In: true,
            user : data.user
        });
    }

    checkLoginStatus = () => {

        fetch('api/User/checkSession')
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    this.setState({
                        logged_status: "LOG_IN",
                        logged_In: true,
                        user: data.user
                    });
                }
            });
        //Revisar como recuperar el objeto de datos cuando se logea con .NET
    }

    componentDidMount = () => {
        this.checkLoginStatus();
    }

    render () {
        return (
            !this.state.logged_In ? <Login handleLogin={this.handleLogin}  /> :
            <Layout>
                <Route
                        exact path='/'
                        render={props => (<Home {...props} logged_status={this.state.logged_status} />
                    )}
                />
                    <Route
                        path='/recipment'
                        render={props => (<Receipt {...props} logged_status={this.state.logged_status} />
                        )}
                />
            </Layout>
        );
    }
}
