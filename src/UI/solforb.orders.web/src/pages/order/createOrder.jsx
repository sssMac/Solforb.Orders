import React, {useEffect, useState} from 'react';
import providerService from "../../services/provider.service";
import {useNavigate} from "react-router-dom";
import {Button, Container} from "react-bootstrap";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faXmark} from "@fortawesome/free-solid-svg-icons";
import OrderForm from "../../components/orderForm";
import orderService from "../../services/order.service";
import createDateAsUTC from "../../utils/date-helper";

const CreateOrder = () => {
    const [errors, setErrors] = useState([""])

    const [providers, setProviders] = useState([])
    const [order, setOrder] = useState(
        {
            number: "",
            date: "",
            providerId: "",
        })
    const [orderItems, setOrderItems] = useState(
        [{
            name: "",
            quantity : "" ,
            unit : ""
        }]);

    useEffect(() => {
        retrieveProviders();
    },[])

    function retrieveProviders(){
        providerService.getProviders()
            .then((res) => {
                setProviders(res.data)
            })
            .catch((ex) => {
                console.log(ex)
            });

    }

    let navigate = useNavigate();
    const onBackClick = (e) => {
        e.preventDefault();
        navigate("/orders");
    }

    const onSubmit = (e) => {
        e.preventDefault()
        let data = {
            number: order.number,
            date: createDateAsUTC(new Date(order.date)),
            providerId: order.providerId,
            orderItems: orderItems
        }
        orderService.createOrder(data)
            .then((res) => {
                console.log(res.data)
                if(res.data.success)
                    navigate(`/orders`)
                else
                    setErrors(res.data.errors)
            })
            .catch((ex) => {
                console.log(ex)
            });
    }

    return (
        <Container className="d-flex container flex-column">
            <Button className="align-self-end"
                    variant="secondary"
                    onClick={(e) => onBackClick(e)}>
                <FontAwesomeIcon icon={faXmark} />
            </Button>
            <OrderForm order={order}
                       setOrder={setOrder}
                       providers={providers}
                       orderItems={orderItems}
                       setOrderItems={setOrderItems}
                       isViewMode={false}
                       onSubmit={onSubmit}

            />
            {
                errors
                .map((e,i) => <p key={i} className="text-danger">{e}</p> )
            }
        </Container>
    );
};

export default CreateOrder;