import React from 'react';
import {render, RenderResult} from '@testing-library/react';
import {HistoryDataEdit} from './HistoryDataEdit';
import {TestContext} from "ra-test";
import {ThemeProvider} from '@material-ui/styles';
import {createTheme} from '@material-ui/core/styles';

describe('HistoryDataEdit', () => {
    let testUtils: RenderResult;

    beforeEach(() => {
        const defaultEditProps = {
            basePath: 'basePath',
            id: 'testID',
            resource: 'testResource',
        };
        const theme = createTheme({});

        testUtils = render(
            <ThemeProvider theme={theme}>
                <TestContext enableReducers={true}>
                    <HistoryDataEdit {...defaultEditProps} />
                </TestContext>
            </ThemeProvider>
        );
    });

    test('matches snapshot', () => {
        expect(testUtils).toMatchSnapshot();
    });

});