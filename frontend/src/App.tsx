import React from 'react';
import './App.scss';
import {Admin, Resource} from 'react-admin';
import dataProvider from './DataProvider';
import {HistoryDataList} from "./resources/historyData/HistoryDataList";

const App = () => (
    <Admin dataProvider={dataProvider}>
        <Resource
            name="historyData"
            options={{label: 'Data'}}
            list={HistoryDataList}/>
    </Admin>
);

export default App;