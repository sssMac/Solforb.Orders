import React from 'react';
import Table from 'react-bootstrap/Table';
import OrderTableItem from "./orderTableItem";

const OrdersTable = (props) => {

    return (
        <Table striped bordered hover>
            <thead>
            <tr>
                <th>#</th>
                <th>Number</th>
                <th>Date</th>
                <th>Count Items</th>
                <th>Provider</th>
            </tr>
            </thead>
            <tbody>
            {
                props.orders
                    .map((order, index) => <OrderTableItem key={order.id} index={index}  order={order}/>)
            }
            </tbody>
        </Table>
    );
};

export default OrdersTable;