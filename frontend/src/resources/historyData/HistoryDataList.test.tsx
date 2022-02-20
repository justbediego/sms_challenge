import React from 'react';
import { render, screen } from '@testing-library/react';
import { HistoryDataList } from './HistoryDataList';

test('renders learn react link', () => {
    render(<HistoryDataList />);
    const linkElement = screen.getByText(/learn react/i);
    expect(linkElement).toBeInTheDocument();
});
