import React, {useState} from 'react';
import {Button, Form, InputGroup} from "react-bootstrap";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faMinus, faPlus} from "@fortawesome/free-solid-svg-icons";
import moment from "moment";

const OrderForm = (props) => {

    const handleItemInputChange = (e, index) => {
        const { name, value } = e.target;
        const list = [...props.orderItems];
        list[index][name] = value;
        props.setOrderItems(list);
    };

    const handleOrderInputChange = (e) => {
        const {name, value} = e.target;
        const temp = {...props.order}
        temp[name] = value
        props.setOrder(temp)
    }

    const handleAddClick = () => {
        props.setOrderItems([...props.orderItems, { name: "", quantity : "" , unit : "" }]);
    };

    const handleRemoveClick = index => {
        const list = [...props.orderItems];
        list.splice(index, 1);
        props.setOrderItems(list);
    };


    return (
        <Form validated={true} >
            <Form.Group className="mb-3 text-left" controlId="formBasicNumber">
                <Form.Label >Number</Form.Label>
                <Form.Control type="text"
                              name="number"
                              placeholder="Enter number"
                              value={props.order.number}
                              onChange={(e) => handleOrderInputChange(e)}
                              required
                              disabled={props.isViewMode}
                />
                <Form.Control.Feedback type="invalid">
                    Please enter order number.
                </Form.Control.Feedback>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicDate">
                <Form.Label>Date</Form.Label>
                <Form.Control required
                              type="date"
                              name="date"
                              placeholder="Date"
                              value={moment(props.order.date).format("YYYY-MM-DD")}
                              onChange={(e) => handleOrderInputChange(e)}
                              disabled={props.isViewMode}

                />
                <Form.Control.Feedback type="invalid">
                    Please set date.
                </Form.Control.Feedback>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicSelect">
                <Form.Label>Provider</Form.Label>
                <Form.Select aria-label="Default select example"
                             name="providerId"
                             onChange={(e) => handleOrderInputChange(e)}
                             disabled={props.isViewMode}
                             required>
                    {props.isViewMode || props.isEditMode ?
                        <option value={props.provider.id}>{props.provider.name}</option>
                        : <option value="">Open this select menu</option>}

                    {
                        props.providers === undefined || props.isViewMode ? false :
                            props.providers
                                .map(provider => <option key={provider.id} value={provider.id}>{provider.name}</option>)
                    }
                </Form.Select>
                <Form.Control.Feedback type="invalid">
                    Please set provider.
                </Form.Control.Feedback>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicOrderItems">
                <Form.Label>Items</Form.Label>
                {
                    props.orderItems
                        .map((item, i) => {
                            return(<div key={i}>
                                    <InputGroup>
                                        <Form.Control type="text"
                                                      placeholder="Name"
                                                      name="name"
                                                      value={item.name}
                                                      onChange={(e) => handleItemInputChange(e, i)}
                                                      required
                                                      disabled={props.isViewMode}
                                        />
                                        <Form.Control type="number"
                                                      placeholder="Quantity"
                                                      name="quantity"
                                                      value={item.quantity}
                                                      onChange={(e) => handleItemInputChange(e, i)}
                                                      required
                                                      disabled={props.isViewMode}
                                        />
                                        <Form.Control type="text"
                                                      placeholder="Unit"
                                                      name="unit"
                                                      value={item.unit}
                                                      onChange={(e) => handleItemInputChange(e, i)}
                                                      required
                                                      disabled={props.isViewMode}
                                        />

                                        {
                                            props.orderItems.length === 1 || props.isViewMode? false :
                                                <Button variant="danger"
                                                        onClick={() => handleRemoveClick(i)}>
                                                    <FontAwesomeIcon icon={faMinus} />
                                                </Button>

                                        }
                                        <Form.Control.Feedback type="invalid">
                                            Fill in all the fields .
                                        </Form.Control.Feedback>
                                    </InputGroup>
                                    <br/>
                                </div>
                            )
                        })
                }
                <Button variant="success"
                        onClick={handleAddClick}
                        hidden={props.isViewMode}
                >
                    <FontAwesomeIcon icon={faPlus} />
                </Button>
            </Form.Group>

            <Button variant="primary"
                    type="submit"
                    className="me-2"
                    onClick={(e) => props.onSubmit(e)}
                    hidden={props.isViewMode}
            >
                Submit
            </Button>
            <Button variant="danger"
                    onClick={props.onCancel}
                    hidden={!props.isEditMode}
            >
                Cancel
            </Button>
            <Button variant="warning"
                    className="me-2"
                    onClick={props.onEdit}
                    hidden={!props.isViewMode}
            >
                Edit
            </Button>

            <Button variant="danger"
                    onClick={props.onDelete}
                    hidden={!props.isViewMode}
            >
                Delete
            </Button>

        </Form>
    );
};

export default OrderForm;