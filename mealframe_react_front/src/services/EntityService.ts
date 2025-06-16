import {BaseService} from "@/services/BaseService";
import {IResultObject} from "@/types/IResultObject";
import {AxiosError} from "axios";
import {IDomainId} from "@/types/IDomainId";

export abstract class EntityService<TEntity extends IDomainId, TAddEntity> extends BaseService {
    constructor(protected basePath: string) {
        super();
    }

    async getAllAsync(): Promise<IResultObject<TEntity[]>> {
        try {
            const response = await this.axiosInstance.get<TEntity[]>(this.basePath);

            if (response.status <= 300) {
                return {
                    statusCode: response.status,
                    data: response.data
                }
            }
            return {
                statusCode: response.status,
                errors: [(response.status.toString() + " " + response.statusText).trim()]
            }
        } catch (error) {
            return {
                statusCode: (error as AxiosError)?.status ?? 0,
                errors: [(error as AxiosError).code ?? ""]
            }
        }
    }

    async getAsync(id: string): Promise<IResultObject<TEntity>> {
        try {

            const response = await this.axiosInstance.get<TEntity>(this.basePath + "/" + id)

            if (response.status <= 300) {
                return {
                    statusCode: response.status,
                    data: response.data
                }
            }

            return {
                statusCode: response.status,
                errors: [(response.status.toString() + ' ' + response.statusText).trim()],
            }
        } catch (error) {
            return {
                statusCode: (error as AxiosError).status ?? 0,
                errors: [(error as AxiosError).code ?? "???"],
            }
        }
    }

    async deleteAsync(id: string): Promise<IResultObject<null>> {
        try {

            const response = await this.axiosInstance.delete<null>(this.basePath + "/" + id)

            if (response.status <= 300) {
                return {
                    statusCode: response.status,
                    data: null
                }
            }

            return {
                statusCode: response.status,
                errors: [(response.status.toString() + ' ' + response.statusText).trim()],
            }
        } catch (error) {
            return {
                statusCode: (error as AxiosError).status ?? 0,
                errors: [(error as AxiosError).code ?? "???"],
            }
        }
    }

    async addAsync(entity: TAddEntity): Promise<IResultObject<TEntity>> {
        try {
            const response = await this.axiosInstance.post<TEntity>(this.basePath, entity)

            if (response.status <= 300) {
                return {
                    statusCode: response.status,
                    data: response.data
                }
            }

            return {
                statusCode: response.status,
                errors: [(response.status.toString() + ' ' + response.statusText).trim()],
            }
        } catch (error) {
            return {
                statusCode: (error as AxiosError).status ?? 0,
                errors: [(error as AxiosError).code ?? "???"],
            }
        }
    }

    async updateAsync(id: string, entity: TEntity): Promise<IResultObject<TEntity>> {
        try {
            const response = await this.axiosInstance.put<TEntity>(this.basePath + "/" + id, entity);

            if (response.status <= 300) {
                return {
                    statusCode: response.status,
                    data: response.data
                }
            }

            return {
                statusCode: response.status,
                errors: [(response.status.toString() + ' ' + response.statusText).trim()],
            }
        } catch (error) {
            return {
                statusCode: (error as AxiosError).status ?? 0,
                errors: [(error as AxiosError).code ?? "???"],
            }
        }
    }
}