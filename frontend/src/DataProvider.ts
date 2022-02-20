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
            const {keyword, fromDate, toDate} = params.filter;
            const filter = [
                `pageIndex=${page}`,
                `pageSize=${perPage}`,
                `sortField=${field}`,
                `isAscending=${order === "ASC"}`,
            ]
            if (keyword) {
                filter.push(`keyword=${keyword}`)
            }
            if (fromDate) {
                filter.push(`fromDate=${fromDate}`)
            }
            if (toDate) {
                filter.push(`toDate=${toDate}`)
            }
            return fetch(`${apiUrl}/${resource}?${filter.join('&')}`, options)
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