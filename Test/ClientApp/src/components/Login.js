import React, { Component } from "react";
import { Button, FormGroup, Input, Label } from "reactstrap";
import "./Login.css";

class Login extends Component {

    constructor(props) {
        super(props);

        this.state = {
            username: "",
            passowrd: ""
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange = (event) => {
        this.setState({
            [event.target.name]: event.target.value
        });
    }

    handleSubmit = (event) => {
        event.preventDefault();

        try {

            fetch("api/User/validateUser", {
                method: 'POST', // or 'PUT'
                body: JSON.stringify({ user_name: this.state.username, password: this.state.passowrd }), // data can be `string` or {object}!
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(res => res.json())
                .catch(error => console.error('Error:', error))
                .then(response => {
                    console.log('Success:', response);

                    this.props.handleLogin({
                        logged_status: "LOG_IN",
                        logged_In: true,
                        user: { user_name: "Fredy" }
                    });
                }
            );

            //metodo para consultar usuario
        } catch (ex) {
            alert(ex.message);
        }
    };

    render() {
        return (
            <div className="Login">
                <form onSubmit={this.handleSubmit}>
                    <FormGroup controlId="email" bsSize="large">
                        <Label>Email</Label>
                        <Input
                            autoFocus
                            type="email"
                            value={this.state.value}
                            onChange={this.handleChange}
                            required
                        />
                    </FormGroup>
                    <FormGroup controlId="password" bsSize="large">
                        <Label>Password</Label>
                        <Input
                            value={this.state.password}
                            onChange={this.handleChange}
                            required
                    type="password"
                />
                    </FormGroup>
                    <Button block bsSize="large" /*disabled={!this.validateForm()}*/ type="submit">
                        Login
                    </Button>
                </form>
            </div>
        );
    }
}

export default Login;