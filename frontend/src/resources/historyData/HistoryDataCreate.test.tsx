import React from 'react';
import { render, screen } from '@testing-library/react';
import { HistoryDataCreate } from './HistoryDataCreate';

test('renders learn react link', () => {
    render(<HistoryDataCreate />);
    const linkElement = screen.getByText(/learn react/i);
    expect(linkElement).toBeInTheDocument();
});
