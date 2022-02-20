import { GET_LIST } from 'react-admin';

const apiUrl = '/api';

export default (type, resource, params) => {
    let url = '';
    const options = {
        headers : new Headers({
            Accept: 'application/json',
        }),
    };
    switch (type) {
        case GET_LIST: {
            const { page, perPage } = params.pagination;
            const { field, order } = params.sort;
            const query = {
                sort: JSON.stringify([field, order]),
                range: JSON.stringify([
                    (page - 1) * perPage,
                    page * perPage - 1,
                ]),
                filter: JSON.stringify(params.filter),
            };
            url = `${apiUrl}/${resource}?${JSON.stringify(query)}`;
            break;
        }
        default:
            throw new Error(`Unsupported Data Provider request type ${type}`);
    }

    return fetch(url, options)
        .then(res => res.json())
        .then(response => ({
            data: response.data,
            total: response.totalCount
        }));
};