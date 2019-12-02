import React, { Component, useState, useEffect } from 'react';
import { Dropdown, DropdownToggle, DropdownMenu, DropdownItem, Row, Col } from 'reactstrap';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, FormGroup, Input, Label } from 'reactstrap';
import "./AddUpdReceipt.css";

const AddUpdReceipt = (props) => {

    const {
        buttonLabel,
        className
    } = props;

    const [dropdownOpen, setDropdownOpen] = useState(false);
    const [title, setTitle] = useState('');

    const [receipt_number, setReceipt_Number] = useState(0);
    const [provider, setProvider] = useState(0);
    const [providers, setProviders] = useState([]);
    const [currency, setCurrency] = useState(0);
    const [amount, setAmount] = useState(0);
    const [status, setStatus] = useState(1);
    const [comment, setComment] = useState('');

    const [modal, setModal] = useState(false);

    const toggle = () => setDropdownOpen(prevState => !prevState);  
    const toggleModal = () => setModal(!modal);

    const handleChange = (event) => {
        var val = event.target.value;
        switch (event.target.name) {
            case "receipt_number":
                setReceipt_Number(val);
                return;
            case "provider":
                setProvider(val);
                return;
            default:
                return;
        }
    }

    useEffect(() => {
        fetch('api/Providers/getProvider'
        )
            .then(response => response.json())
            .catch(error => console.error('Error:', error))
            .then(data => {
                setProviders(data.list);
            });
    });

    let optionProvItems = providers.map((provider) =>
        <option value={provider.id}>{provider.name}</option>
    );

    return (
        <div>
            <Button color="primary" onClick={() => { setTitle('Nuevo'); toggleModal(); }} >Nuevo Recibo</Button>
            <Modal className="AddUpdReceipt" isOpen={modal} toggle={toggleModal} className={className}>
                <form>
                <ModalHeader toggle={toggleModal}>{title}</ModalHeader>
                <ModalBody>
                        <FormGroup >
                            <Row>
                                <Col>
                                    <Label>#Recibo</Label>
                                </Col>
                                <Col>
                            <Input
                                autoFocus
                                name="receipt_number"
                                type="number"
                                value={receipt_number}
                                onChange={handleChange}
                                required
                                readOnly
                                    />
                                </Col>
                            </Row>
                        </FormGroup>
                        <FormGroup >
                            <Row>
                                <Col>
                                    <Label>Proveedor</Label>
                                </Col>
                                <Col>

                                    <Input type="select" name="provider" id="provider" value="{provider}" onChange={handleChange} required>
                                        {optionProvItems}
                                    </Input>
                                </Col></Row>
                        </FormGroup>
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" block type="submit">Guardar</Button>{' '}
                    <Button color="secondary" onClick={toggleModal}>Cancelar</Button>
                </ModalFooter>
                </form>
            </Modal>
        </div>
    );
};

export default AddUpdReceipt;