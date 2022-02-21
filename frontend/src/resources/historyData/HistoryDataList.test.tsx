import React from 'react';
import {HistoryDataList} from './HistoryDataList';
import {TestContext} from "ra-test";
import {ThemeProvider} from '@material-ui/styles';
import {createTheme} from '@material-ui/core/styles';
import ReactDOMServer from 'react-dom/server';

describe('HistoryDataList', () => {
    let testUtils: string;

    beforeEach(() => {
        const defaultEditProps = {
            basePath: 'basePath',
            id: 'testID',
            resource: 'testResource',
        };
        const theme = createTheme({});

        testUtils = ReactDOMServer.renderToStaticMarkup(
            <ThemeProvider theme={theme}>
                <TestContext enableReducers={true}>
                    <HistoryDataList {...defaultEditProps} />
                </TestContext>
            </ThemeProvider>
        );
    });

    test('matches snapshot', () => {
        expect(testUtils
            // .replace(/id="mui-[0-9]*"/g, 'id="mui-id"')
            // .replace(/aria-labelledby="(mui-[0-9]* *)*"/g, 'aria-labelledby="mui-area-id"')
        ).toMatchSnapshot();
    });

});