import React, { useState, useEffect } from 'react';
import { Row, Col } from 'reactstrap';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, FormGroup, Input, Label } from 'reactstrap';
import "./AddUpdReceipt.css";

const AddUpdReceipt = (props) => {

    const {
        className
    } = props;

    const [title, setTitle] = useState('');

    const [receipt_number, setReceipt_Number] = useState(0);
    const [provider, setProvider] = useState(0);
    const [currency, setCurrency] = useState(0);
    const [providers, setProviders] = useState([]);
    const [currencies, setCurrencies] = useState([]);
    const [amount, setAmount] = useState(0);
    const [date, setDate] = useState('');
    const [status, setStatus] = useState(true);
    const [comment, setComment] = useState('');

    const [modal, setModal] = useState(false);

    const toggleModal = () => setModal(!modal);

    const handleChange = (event) => {
        const val = event.target.value;
        switch (event.target.name) {
            case "receipt_number":
                setReceipt_Number(val);
                return;
            case "provider":
                setProvider(val);
                return;
            case "amount":
                setAmount(val);
                return;
            case "currency":
                setCurrency(val);
                return;
            case "date":
                setDate(val);
                return;
            case "comment":
                setComment(val);
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

        fetch('api/Currencies/getCurrency'
        )
            .then(response => response.json())
            .catch(error => console.error('Error:', error))
            .then(data => {
                setCurrencies(data.list);
            });

    }, []);

    let defVal = <option key={0} value={0}>Seleccionar...</option>

    let optionProvItems = providers.map((provider) =>
        <option key={provider.id} value={provider.id}>{provider.name}</option>
    );

    let optionCurrencyItems = currencies.map((currency) =>
        <option key={currency.id} value={currency.id}>{currency.name}</option>
    );

    const getReceipts = () => {
        props.toggleLoading();
        props.getReceipts();
    }

    const handleSubmit = (event) => {
        event.preventDefault();

        try {

            let data = {
                Id: receipt_number,
                ProviderId: provider,
                CurrencyId: currency,
                Amount: amount,
                Date: date,
                Comment: comment,
                Status: status
            };

            fetch("api/Receipts/saveReceipts", {
                method: 'POST', // or 'PUT'
                body: JSON.stringify(data), // data can be `string` or {object}!
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(res => res.json())
                .catch(error => console.error('Error:', error))
                .then(response => {
                    console.log('Success saveReceipts:', response);
                    toggleModal();
                    getReceipts();
                });

            //metodo para consultar usuario
        } catch (ex) {
            console.log(ex.message);
        }
    };

    return (
        <div>
            <Button color="primary" onClick={() => { setTitle('Nuevo'); toggleModal(); }} >Nuevo Recibo</Button>
            <Modal className="AddUpdReceipt" isOpen={modal} toggle={toggleModal} className={className}>
                <form onSubmit={handleSubmit}>
                <ModalHeader toggle={toggleModal}>{title}</ModalHeader>
                <ModalBody>
                        <FormGroup>
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
                                    <Input type="select" value={provider} name="provider" onChange={handleChange} required>
                                        {defVal}
                                        {optionProvItems}
                                    </Input>
                                </Col>
                            </Row>
                        </FormGroup>
                        <FormGroup >
                            <Row>
                                <Col>
                                    <Label>Moneda</Label>
                                </Col>
                                <Col>
                                    <Input type="select" value={currency} name="currency" onChange={handleChange} required>
                                        {defVal}
                                        {optionCurrencyItems}
                                    </Input>
                                </Col>
                            </Row>
                        </FormGroup>
                        <FormGroup>
                            <Row>
                                <Col>
                                    <Label>Monto</Label>
                                </Col>
                                <Col>
                                    <Input
                                        autoFocus
                                        name="amount"
                                        type="number"
                                        value={amount}
                                        onChange={handleChange}
                                        required
                                    />
                                </Col>
                            </Row>
                        </FormGroup>
                        <FormGroup>
                            <Row>
                                <Col>
                                    <Label>Fecha</Label>
                                </Col>
                                <Col>
                                    <Input
                                        autoFocus
                                        name="date"
                                        type="date"
                                        value={date}
                                        onChange={handleChange}
                                        required
                                    />
                                </Col>
                            </Row>
                        </FormGroup>
                        <FormGroup>
                            <Row>
                                <Col>
                                    <Label>Comentario</Label>
                                </Col>
                                <Col>
                                    <Input
                                        autoFocus
                                        name="comment"
                                        type="textarea"
                                        value={comment}
                                        onChange={handleChange}
                                        required
                                    />
                                </Col>
                            </Row>
                        </FormGroup>
                </ModalBody>
                <ModalFooter>
                    <Row>
                    <Col><Button color="primary" type="submit">Guardar</Button>{' '}</Col>
                     <Col>       <Button color="secondary" onClick={toggleModal}>Cancelar</Button></Col>
                    </Row>
                </ModalFooter>
                </form>
            </Modal>
        </div>
    );
};

export default AddUpdReceipt;