import * as React from "react";
import {Datagrid, DateField, DateInput, List, NumberField, SelectField, TextField, TextInput} from 'react-admin';
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
            <SelectField source="status" choices={[
                {id: 100, name: 'Once'},
                {id: 101, name: 'Daily'},
                {id: 102, name: 'Weekly'},
                {id: 103, name: 'Yearly'},
                {id: 104, name: 'Seldom'},
                {id: 105, name: 'Monthly'},
                {id: 106, name: 'Often'},
                {id: 107, name: 'Never'}
            ]}/>
            <ColorField source="color"/>
        </Datagrid>
    </List>
);