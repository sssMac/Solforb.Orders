import React, {useEffect, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import orderService from "../../services/order.service";
import providerService from "../../services/provider.service";
import {Button, Container, Form, InputGroup} from "react-bootstrap";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faXmark} from "@fortawesome/free-solid-svg-icons";
import OrderForm from "../../components/orderForm";

const ViewOrder = () => {
    const { id } = useParams()
    const [order, setOrder] = useState(
        {
            id: "",
            number: "",
            date: "",
            providerId: "",
            orderItems: []}
    )
    const [load, setLoad] = useState(true)

    const [provider, setProvider] = useState({name: "", id: ""})
    useEffect(() => {
        retrieveOrder(id)
        setTimeout(() => {setLoad(false)
        },1000)
    },[])

    let navigate = useNavigate()

    const onBackClick = (e) => {
        e.preventDefault();
        navigate("/orders");
    }

    function retrieveOrder(id){
        orderService.getOrder(id)
            .then((res) => {
                setOrder(res.data)
                retrieveProvider(res.data.providerId)
            })
            .catch((ex) => {
                console.log(ex)
            });
    }
    function retrieveProvider(id){
        providerService.getProvider(id)
            .then((res) => {
                setProvider(res.data)
            })
            .catch((ex) => {
                console.log(ex)
            });
    }

    const onEdit = () => {
        navigate(`/order/edit/${id}`)
    }

    const onDelete = () => {
        orderService.deleteOrder(id)
            .then((res) => {
                setProvider(res.data)
                navigate(`/orders`)
            })
            .catch((ex) => {
                console.log(ex)
            });
    }

    if(load){
        return (
            <Container className="d-flex container">
                Loading...
            </Container>
        )
    }
    else{
        return (
            <Container className="d-flex container flex-column w-50">
                <Button className="align-self-end"
                        variant="secondary"
                        onClick={(e) => onBackClick(e)}>
                    <FontAwesomeIcon icon={faXmark} />
                </Button>
                <OrderForm order={order}
                           orderItems={order.orderItems}
                           provider={provider}
                           isViewMode={true}
                           isEditMode={false}
                           onEdit={onEdit}
                           onDelete={onDelete}

                />
            </Container>
        );
    }
};

export default ViewOrder;