import React from 'react';
import './App.scss';
import {Admin, Resource} from 'react-admin';
import dataProvider from './dataProvider';
import {HistoryDataList} from "./resources/historyData/HistoryDataList";
import {HistoryDataCreate} from "./resources/historyData/HistoryDataCreate";

const App = () => (
    <Admin dataProvider={dataProvider}>
        <Resource
            name="historyData"
            options={{label: 'Data'}}
            list={HistoryDataList}
            create={HistoryDataCreate}
        />
    </Admin>
);

export default App;