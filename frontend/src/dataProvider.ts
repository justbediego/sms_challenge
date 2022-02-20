import {CREATE, DELETE_MANY, GET_LIST} from 'react-admin';

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
    const headers = new Headers({Accept: 'application/json', 'Content-Type': "application/json"});
    const baseUrl = `/api/${resource}`;

    const deleteMany = async () => {
        for (const id of params.ids) {
            await fetch(`${baseUrl}/${id}`, {method: 'DELETE', headers});
        }
        return {data: []}
    }

    const getList = async () => {
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
        return await fetch(`${baseUrl}?${filter.join('&')}`, {headers})
            .then(res => res.json())
            .then((response: PagingResult<HistoryDataDTO>) => ({
                data: response.data,
                total: response.totalCount
            }));
    }

    const createOne = async () => {
        const newData = {
            city: params.data.city,
            startDate: params.data.startDate,
            endDate: params.data.endDate,
            price: params.data.price,
            status: params.data.status,
            color: params.data.color,
        } as HistoryDataDTO;
        return fetch(baseUrl, {method: 'POST', headers, body: JSON.stringify(newData)})
            .then(res => res.json())
            .then(res => {
                if (res.status && res.status !== 200) {
                    throw res.errors ?
                        Object.keys(res.errors).map(key => res.errors[key]).join(", ")
                        : "Request did not succeed !";
                }
                return {data: {...params.data, id: res}};
            });
    }

    switch (type) {
        case GET_LIST: {
            return getList();
        }
        case CREATE: {
            return createOne();
        }
        case DELETE_MANY:
            return deleteMany();
        default:
            debugger;
            throw new Error(`Unsupported Data Provider request type ${type}`);
    }
};

export default dataProvider;