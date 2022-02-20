import * as React from "react";
import {Datagrid, DateField, DateInput, List, NumberField, TextField, TextInput} from 'react-admin';
import "./HistoryDataList.scss"

const {ColorField} = require('react-admin-color-input');

const postFilters = [
    <TextInput label="Keyword" source="keyword" alwaysOn/>,
    <DateInput label="From" source="fromDate" alwaysOn/>,
    <DateInput label="To" source="toDate" alwaysOn/>
];

export const HistoryDataList = (props: any) => (
    <List {...props} filters={postFilters}>
        <Datagrid rowClick="edit">
            <NumberField source="id"/>
            <TextField source="city"/>
            <DateField source="startDate" locales="de-DE"/>
            <DateField source="endDate" locales="de-DE"/>
            <NumberField source="price"/>
            <TextField source="status"/>
            <ColorField source="color"/>
        </Datagrid>
    </List>
);