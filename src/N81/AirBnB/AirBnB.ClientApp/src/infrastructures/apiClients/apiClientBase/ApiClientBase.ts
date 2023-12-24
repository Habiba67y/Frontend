import { AxiosError, type AxiosInstance, type AxiosRequestConfig, type AxiosResponse } from "axios";
import axios from "axios";
import { ApiResponce} from "./ApiResponce";
import type { ProblemDetails } from "./ProblemDetails";

export default class ApiClientBase {
    public readonly client: AxiosInstance;

    constructor(config: AxiosRequestConfig) {
        this.client = axios.create(config);

        this.client.interceptors.response.use(<TResponce>(response: AxiosResponse<TResponce>) => {
                return {
                    ...response,
                    data: new ApiResponce(response.data as TResponce, null, response.status)
                }
            },
            (error: AxiosError) => {
                return {
                    ...error,
                    data: new ApiResponce(null, error.response?.data as ProblemDetails, error.response?.status ?? 500)                    
                };
            }
        )
    }
    
    public async getAsync<T>(url: string, config?: AxiosRequestConfig) : Promise<ApiResponce<T>> {
        return (await this.client.get<ApiResponce<T>>(url, config)).data;
    }
    
    public async postAsync<T>(url: string, data?: any, config?: AxiosRequestConfig) : Promise<ApiResponce<T>> {
        return (await this.client.post<ApiResponce<T>>(url, data, config)).data;
    }
    
    public async putAsync<T>(url: string, data?: any, config?: AxiosRequestConfig) : Promise<ApiResponce<T>> {
        return (await this.client.put<ApiResponce<T>>(url, data, config)).data;
    }
    
    public async deleteAsync<T>(url: string, config?: AxiosRequestConfig) : Promise<ApiResponce<T>> {
        return (await this.client.delete<ApiResponce<T>>(url, config)).data;
    }
}