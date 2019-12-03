import React, { Component } from 'react';
import { FaEdit } from "react-icons/fa";
import { Tooltip, Button } from 'reactstrap';
import AddUpdReceipt from './AddUpdReceipt'


export class Receipt extends Component {
    static displayName = Receipt.name;
    
    constructor(props) {
        super(props);
        this.state = {
            receipts: [], loading: true, search: "test",
            tooltipOpen: false
        };

        this.getReceipts = this.getReceipts.bind(this);
        this.toggleLoading = this.toggleLoading.bind(this);

        this.getReceipts();

    }

    getReceipts = () => {
        fetch('api/Receipts/getReceipts?search=' + this.state.search
        )
        .then(response => response.json())
        .then(data => {
            this.setState({ receipts: data, loading: false });
        });
    }

    


    toggleLoading = () => { this.setState({ loading: true })};

    toggle = () => { this.setState({ tooltipOpen: !this.state.tooltipOpen }) };

    static renderReceiptTable(receipts, tooltipOpen) {
        console.log(receipts);
        return ( 
      <table className='table table-striped'>
        <thead>
          <tr>
            <th># Recibo</th>
            <th>Proveedor</th>
            <th>Moneda</th>
            <th>Monto</th>
            <th>fecha Registro</th>
            <th>Comentario</th>
                    <th>Estatus</th>
                    <th></th>
          </tr>
        </thead>
        <tbody>
         {receipts.list.map(receipt =>
            <tr key={receipt.id}>
                <td>{receipt.id}</td>
                <td>{receipt.provider.name}</td>
                <td>{receipt.currency.name}</td>
                <td>{receipt.amount}</td>
                <td>{receipt.date}</td>
                <td>{receipt.comment}</td>
                            <td>{receipt.status ? "Activo" : "Inactivo"}</td>
                            <td id="EditIcon"><a onClick={ this.toggle }><FaEdit /><Tooltip placement="right" isOpen={tooltipOpen} target="EditIcon" toggle={this.toggle}>
                            Editar</Tooltip></a></td>
            </tr>
         )}

        </tbody>
      </table>
    );
  }

    render() {
    let contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : Receipt.renderReceiptTable(this.state.receipts, this.state.tooltipOpen);

    return (
        <div>
            <h1>Recibos</h1>
            <AddUpdReceipt tooltipOpen={this.tooltipOpen} getReceipts={this.getReceipts} toggleLoading={this.toggleLoading}/> 
           {contents}
      </div>
    );
  }
}
