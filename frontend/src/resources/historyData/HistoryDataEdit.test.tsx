import React from 'react';
import {render, RenderResult} from '@testing-library/react';
import {HistoryDataEdit} from './HistoryDataEdit';
import {TestContext} from "ra-test";

describe('HistoryDataEdit', () => {
    let testUtils: RenderResult;

    beforeEach(() => {
        const defaultEditProps = {
            basePath: 'basePath',
            id: 'testID',
            resource: 'testResource',
        };

        testUtils = render(
            <TestContext enableReducers={true}>
                <HistoryDataEdit {...defaultEditProps} />
            </TestContext>
        );
    });

    test('matches snapshot', () => {
        expect(testUtils).toMatchSnapshot();
    });

});