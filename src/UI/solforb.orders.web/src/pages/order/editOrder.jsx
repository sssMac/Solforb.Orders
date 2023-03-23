import React, {useEffect, useState} from 'react';
import {Button, Container, Placeholder} from "react-bootstrap";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faXmark} from "@fortawesome/free-solid-svg-icons";
import OrderForm from "../../components/orderForm";
import {useNavigate, useParams} from "react-router-dom";
import orderService from "../../services/order.service";
import providerService from "../../services/provider.service";

const EditOrder = () => {
    const { id } = useParams()
    const [errors, setErrors] = useState([""])
    const [load, setLoad] = useState(true)
    const [order, setOrder] = useState(
        {
            id: "",
            number: "",
            date: "",
            providerId: "",
            orderItems: []}
    )

    const [orderItems, setOrderItems] = useState()
    const [providers, setProviders] = useState([])
    const [provider, setProvider] = useState({name: "", id: ""})
    useEffect(() => {
        retrieveOrder(id)
        retrieveProviders()
        setTimeout(() => {setLoad(false)},1000)
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
                setOrderItems(res.data.orderItems)
                retrieveProvider(res.data.providerId)
            })
            .finally(() => {
                retrieveProviders()
            })
            .catch((ex) => {
                console.log(ex)
            });

    }
    function retrieveProviders(){
        providerService.getProviders()
            .then((res) => {
                setProviders(res.data)
            })
            .catch((ex) => {
                console.log(ex)
            });
    }
    function retrieveProvider(id){
        providerService.getProvider(id)
            .then((res) => {
                setProvider(res.data)
                setLoad(false)
            })
            .catch((ex) => {
                console.log(ex)
            });
    }

    function createDateAsUTC(date) {
        return new Date(Date.UTC(date.getFullYear(),
            date.getMonth(),
            date.getDate(),
            date.getHours(),
            date.getMinutes(),
            date.getSeconds()));
    }
    const onCancel = () => {
        navigate(`/order/${id}`)
    }

    const onSubmit = (e) => {
        e.preventDefault()
        let data = {
            id: order.id,
            number: order.number,
            date: createDateAsUTC(new Date(order.date)),
            providerId: order.providerId,
            orderItems: orderItems
        }
        orderService.editOrder(id, data)
            .then((res) => {
                console.log(res.data)
                if(res.data.success === false)
                    setErrors(res.data.errors)
                else navigate(`/order/${id}`)

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
                           orderItems={orderItems}
                           setOrder={setOrder}
                           setOrderItems={setOrderItems}
                           providers={providers}
                           provider={provider}
                           isViewMode={false}
                           isEditMode={true}
                           onCancel={onCancel}
                           onSubmit={onSubmit}

                />
                {
                    errors
                        .map((e,i) => <p key={i} className="text-danger">{e}</p> )
                }
            </Container>
        );
    }
};

export default EditOrder;