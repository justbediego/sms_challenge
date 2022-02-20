import * as React from "react";
import {SimpleForm, DateInput, Create, NumberInput, TextInput, SelectInput} from 'react-admin';
import "./HistoryDataCreate.scss";
const {ColorInput} = require('react-admin-color-input');

export const HistoryDataCreate = (props: any) => (
    <Create  {...props}>
        <SimpleForm>
            <TextInput source="city"/>
            <DateInput source="startDate" locales="de-DE"/>
            <DateInput source="endDate" locales="de-DE"/>
            <NumberInput source="price"/>
            <SelectInput source="status" choices={[
                {id: 100, name: 'Once'},
                {id: 101, name: 'Daily'},
                {id: 102, name: 'Weekly'},
                {id: 103, name: 'Yearly'},
                {id: 104, name: 'Seldom'},
                {id: 105, name: 'Monthly'},
                {id: 106, name: 'Often'},
                {id: 107, name: 'Never'}
            ]}/>
            <ColorInput source="color"/>
        </SimpleForm>
    </Create>
);