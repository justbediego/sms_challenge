import * as React from "react";
import {Datagrid, DateField, List, NumberField, TextField} from 'react-admin';
import "./HistoryDataList.scss"

export const HistoryDataList = (props: any) => (
    <List {...props}>
        <Datagrid rowClick="edit">
            <NumberField source="id"/>
            <TextField source="city"/>
            <DateField source="startDate"/>
            <DateField source="endDate"/>
            <NumberField source="price"/>
            <TextField source="status"/>
            <TextField source="color"/>
        </Datagrid>
    </List>
);