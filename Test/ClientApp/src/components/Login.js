import React, { Component } from "react";
import { Button, FormGroup, Input, Label } from "reactstrap";
import "./Login.css";

class Login extends Component {

    constructor(props) {
        super(props);

        this.state = {
            username: "",
            password: "",
            message_error: ""
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
                body: JSON.stringify({ UserName : this.state.username, Password: this.state.password }), // data can be `string` or {object}!
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(res => res.json())
                .catch(error => console.error('Error:', error))
                .then(response => {
                    console.log('Success:', response);
                    if (response.success) {
                        this.props.handleLogin({
                            logged_status: "LOG_IN",
                            logged_In: true,
                            user: response.user
                        });
                    } else {
                        this.setState({ message_error: response.message});
                    }
                }
            );

            //metodo para consultar usuario
        } catch (ex) {
            console.log(ex.message);
        }
    };

    render() {
        return (
            <div className="Login">
                <form onSubmit={this.handleSubmit}>
                    <FormGroup >
                        <Label>Email</Label>
                        <Input
                            autoFocus
                            name="username"
                            type="email"
                            value={this.state.value}
                            onChange={this.handleChange}
                            required
                        />
                    </FormGroup>
                    <FormGroup>
                        <Label>Password</Label>
                        <Input
                            value={this.state.password}
                            onChange={this.handleChange}
                            name="password"
                            required
                    type="password"
                />
                    </FormGroup>
                    <Button block type="submit">
                        Login
                    </Button>
                    <label style={{color : "red"}}>{this.state.message_error}</label>
                </form>
            </div>
        );
    }
}

export default Login;