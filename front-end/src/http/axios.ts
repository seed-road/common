import axios, {AxiosError, AxiosRequestConfig} from "axios";
import {HttpError, isHttpErrorResponse} from "../errors/HttpError";

export const preconfiguredAxios = (config?: AxiosRequestConfig) => {
    const axiosInstance = axios.create({...config, withCredentials: true});
    axiosInstance.interceptors.response.use(response => response, (error: AxiosError) => {
        if (!!error.response?.data && isHttpErrorResponse(error.response.data)) {
            return Promise.reject({
                url: error.response?.data.url,
                errors: error.response.data.errors,
                code: error.response?.status ?? 500,
            } as HttpError);
        }
        return Promise.reject(error);
    })
    return axiosInstance;
};
