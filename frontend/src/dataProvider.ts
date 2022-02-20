import {CREATE, DELETE_MANY, GET_LIST, GET_ONE, UPDATE} from 'react-admin';

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

    const getOne = async () => {
        const res = await fetch(`${baseUrl}/${params.id}`, {headers});
        const data = await res.json();
        return {data};
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
        const res = await fetch(`${baseUrl}?${filter.join('&')}`, {headers});
        const body: PagingResult<HistoryDataDTO> = await res.json();
        return {
            data: body.data,
            total: body.totalCount
        };
    }

    const throwProperMessage = (res: any) => {
        throw res.errors ?
            Object.keys(res.errors).map(key => res.errors[key]).join(", ")
            : "Request did not succeed !";
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
        const res = await fetch(baseUrl, {method: 'POST', headers, body: JSON.stringify(newData)});
        const body = await res.json();
        if (body.status && body.status !== 200) {
            throwProperMessage(body);
        }
        return {data: {...params.data, id: body}};
    }

    const updateOne = async () => {
        const updateData = {
            city: params.data.city,
            startDate: params.data.startDate,
            endDate: params.data.endDate,
            price: params.data.price,
            status: params.data.status,
            color: params.data.color,
        } as HistoryDataDTO;
        const response = await fetch(`${baseUrl}/${params.id}`, {
            method: 'PUT',
            headers,
            body: JSON.stringify(updateData)
        });
        if (response.status === 200) {
            return {data: {...params.data, id: params.id}};
        }
        // trying to process the exception
        const body = await response.json();
        throwProperMessage(body);
    }

    switch (type) {
        case GET_LIST:
            return getList();
        case GET_ONE:
            return getOne();
        case CREATE:
            return createOne();
        case DELETE_MANY:
            return deleteMany();
        case UPDATE:
            return updateOne();
        default:
            debugger;
            throw new Error(`Unsupported Data Provider request type ${type}`);
    }
};

export default dataProvider;