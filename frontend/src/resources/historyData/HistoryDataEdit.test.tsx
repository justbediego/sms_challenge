import React from 'react';
import { render, screen } from '@testing-library/react';
import { HistoryDataEdit } from './HistoryDataEdit';

test('renders learn react link', () => {
    render(<HistoryDataEdit />);
    const linkElement = screen.getByText(/learn react/i);
    expect(linkElement).toBeInTheDocument();
});
