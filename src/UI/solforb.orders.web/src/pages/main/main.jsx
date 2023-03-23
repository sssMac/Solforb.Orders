import React, {useEffect, useState} from 'react';
import orderService from "../../services/order.service";
import OrdersTable from "../../components/ordersTable";
import 'bootstrap/dist/css/bootstrap.min.css';
import {Button, Container} from "react-bootstrap";
import {useNavigate} from "react-router-dom";
import ActionPanel from "../../components/actionPanel";
import {format, subMonths} from "date-fns";

const Main = () => {
    const [filters, setFilters] = useState({
        number: "",
        dateMin: format(subMonths(new Date(), 1),"yyyy-MM-dd"),
        dateMax: format(new Date(), "yyyy-MM-dd"),
        itemName: "",
        unit: "",
        providerName: ""
    })
    const [orders, setOrders] = useState([]);
    let navigate = useNavigate();

    useEffect(() =>{
        retrieveOrders(filters);
    },[])

    function retrieveOrders(filters){
        orderService.getOrders(filters)
            .then((res) => {
                setOrders(res.data)
            })
            .catch((ex) => {
                console.log(ex)
            });
    }

    const onCreate = (e) => {
        e.preventDefault();
        navigate("/order/create");
    }

    const onFiltration = (e) => {
        e.preventDefault()
        retrieveOrders(filters)
    }

    return (
        <Container className="d-flex flex-row">

            <ActionPanel onCreate={onCreate}
                         filters={filters}
                         setFilters={setFilters}
                         onFiltration={onFiltration}
            />
            <OrdersTable orders={orders} />

        </Container>
    );
};

export default Main;