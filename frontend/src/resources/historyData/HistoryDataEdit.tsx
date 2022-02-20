import * as React from "react";
import {Edit, DateInput, NumberInput, SelectInput, SimpleForm, TextInput} from 'react-admin';
import "./HistoryDataEdit.scss";

const {ColorInput} = require('react-admin-color-input');

export const HistoryDataEdit = (props: any) => (
    <Edit  {...props}>
        <SimpleForm>
            <TextInput source="city" required={true}/>
            <DateInput source="startDate" required={true}/>
            <DateInput source="endDate" required={true}/>
            <NumberInput source="price" required={true}/>
            <SelectInput source="status" required={true} choices={[
                {id: 100, name: 'Once'},
                {id: 101, name: 'Daily'},
                {id: 102, name: 'Weekly'},
                {id: 103, name: 'Yearly'},
                {id: 104, name: 'Seldom'},
                {id: 105, name: 'Monthly'},
                {id: 106, name: 'Often'},
                {id: 107, name: 'Never'}
            ]}/>
            <ColorInput source="color" required={true}/>
        </SimpleForm>
    </Edit>
);