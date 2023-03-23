import React, {useState} from 'react';
import Form from 'react-bootstrap/Form';
import {Button, Container, InputGroup} from "react-bootstrap";
import Select from "react-select";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faMinus} from "@fortawesome/free-solid-svg-icons";
import {forEach} from "react-bootstrap/ElementChildren";

const options = [
    {value: "number", label: "Number" },
    {value: "itemName", label: "Item Name" },
    {value: "unit", label: "Unit" },
    {value: "providerName", label: "Provider Name" },
];


const ActionPanel = (props) => {

    const [selectedFilter, setSelectedFilter] = useState([])

    const handleFilterChange = (e) => {
        const {name, value} = e.target;
        const temp = {...props.filters}
        temp[name] = value
        props.setFilters(temp)
    }

    const handleSelectChange = (items, action) => {
        setSelectedFilter(items)


        if(action.action === "remove-value"){
            handleRemove(action.removedValue)
        }
    }

    const handleRemove = (target) => {
        const temp = {...props.filters}
        temp[target.value] = ""
        props.setFilters(temp)
    }

    return (
        <div className="flex-column w-50 me-5">
            <InputGroup >
                <InputGroup.Text >From</InputGroup.Text>
                <Form.Control type="date"
                              placeholder="From"
                              name="dateMin"
                              value={props.filters.dateMin}
                              isInvalid={props.filters.dateMax < props.filters.dateMin }
                              onChange={(e) => handleFilterChange(e)}
                />
                <InputGroup.Text >To</InputGroup.Text>

                <Form.Control type="date"
                              placeholder="To"
                              name="dateMax"
                              value={props.filters.dateMax}
                              isInvalid={props.filters.dateMin > props.filters.dateMax}
                              onChange={(e) => handleFilterChange(e)}
                />
            </InputGroup>
            <Select
                isMulti
                options={options}
                onChange={(item,action) => handleSelectChange(item, action)}
                className="select"
                isClearable={false}
                isSearchable={true}
                closeMenuOnSelect={false}

            />
            {
                selectedFilter.map((f,i) => {
                    return(
                        <Form.Group key={i} className="mb-3 text-left" controlId="formBasicNumber">
                            <Form.Label >{f.label}</Form.Label>
                            <Form.Control type="text"
                                          name={f.value}
                                          placeholder={f.label}
                                          value={props.filters[f.value]}
                                          onChange={(e) => handleFilterChange(e)}
                            />
                        </Form.Group>
                    )
                })
            }
           <div className="d-flex justify-content-center mb-3">
               <Button className="bg-success mt-3 me-3"
                       onClick={(e) => props.onFiltration(e)}>Find</Button>
               <Button variant="primary"
                       className="me-3 mt-3 "
                       onClick={(e) => props.onCreate(e)}>Create</Button>
           </div>
        </div>
    );
};

export default ActionPanel;