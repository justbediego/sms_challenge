import React from 'react';
import {render, RenderResult} from '@testing-library/react';
import {HistoryDataCreate} from './HistoryDataCreate';
import {TestContext} from 'ra-test';

describe('HistoryDataCreate', () => {
    let testUtils: RenderResult;

    beforeEach(() => {
        const defaultEditProps = {
            basePath: 'basePath',
            id: 'testID',
            resource: 'testResource',
        };

        testUtils = render(
            <TestContext enableReducers={true}>
                <HistoryDataCreate {...defaultEditProps} />
            </TestContext>
        );
    });

    test('matches snapshot', () => {
        expect(testUtils).toMatchSnapshot();
    });

});