import React from 'react';
import { render } from '@testing-library/react';
import App from './App';

test('matches snapshot', () => {
  const result = render(<App />);
  expect(result).toMatchSnapshot();
});
