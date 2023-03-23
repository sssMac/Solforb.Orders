import React, {useEffect, useState} from 'react';
import providerService from "../services/provider.service";
import {useNavigate} from "react-router-dom";
import { format } from 'date-fns'

const OrderTableItem = (props) => {
    const [provider, setProvider] = useState()
    let navigate = useNavigate();

    useEffect(()=>{
        providerService.getProvider(props.order.providerId)
            .then((res) => {
                setProvider(res.data)
            })
            .catch((ex) => {
                console.log(ex)
            });
    },[])

    const onNavigate = (e) => {
        e.preventDefault();
        navigate(`/order/${props.order.id}`, {
            id : props.order.id
        });
    }
    //<td>{Moment(props.order.date ).format('MMMM do, yyyy H:mma')}</td>

    if(props.order !== undefined){
        return (
            <tr onClick={onNavigate}>
                <td>{props.index + 1}</td>
                <td>{props.order.number}</td>
                <td>{format(Date.parse(props.order.date), "yyyy MMMM d")}</td>
                <td>{props.order.orderItems.length}</td>
                <td>{provider === undefined ? props.order.providerId
                    : provider.name}</td>
            </tr>
        );
    }
    else {
        return (<h1>Loading...</h1>)
    }

};

export default OrderTableItem;