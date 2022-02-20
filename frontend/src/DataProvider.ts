import {GET_LIST} from 'react-admin';

const apiUrl = '/api';

interface HistoryDataDTO {
    id: number;
    city: string;
    startDate: string;
    endDate: string;
    price: number;
    status: string;
    color: string;
}

interface PagingResult<T> {
    data: T[];
    pageIndex: number;
    pageSize: number;
    totalCount: number;
}

const dataProvider = (type: any, resource: any, params: any) => {
    const options = {
        headers: new Headers({
            Accept: 'application/json',
        }),
    };
    switch (type) {
        case GET_LIST: {
            const {page, perPage} = params.pagination;
            const {field, order} = params.sort;
            return fetch(`${apiUrl}/${resource}?pageIndex=${page}&pageSize=${perPage}&sortField=${field}&isAscending=${order === "ASC"}`, options)
                .then(res => res.json())
                .then((response: PagingResult<HistoryDataDTO>) => ({
                    data: response.data,
                    total: response.totalCount
                }));
        }
        default:
            throw new Error(`Unsupported Data Provider request type ${type}`);
    }
};

export default dataProvider;