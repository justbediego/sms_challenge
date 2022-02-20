import React from 'react';
import './App.scss';
import {Admin, Resource} from 'react-admin';
import dataProvider from './DataProvider';
import {HistoryDataList} from "./component/HistoryDataList";

const App = () => (
    <Admin dataProvider={dataProvider}>
        <Resource name="historyData" list={HistoryDataList}/>
    </Admin>
);

export default App;