import {EntityService} from "@/services/EntityService";
import {IShoppingList} from "@/types/domain/IShoppingList";
import {IShoppingListAdd} from "@/types/domain/IShoppingListAdd";
import {IResultObject} from "@/types/IResultObject";
import {AxiosError} from "axios";

export class ShoppingListService extends EntityService<IShoppingList, IShoppingListAdd> {
    constructor(){
        super('shoppingLists');
    }

    async generateFromMealPlansAsync(data: IShoppingListAdd): Promise<IResultObject<IShoppingList>> {
        try {
            const response = await this.axiosInstance.post<IShoppingList>(`${this.basePath}/generate`, data);
            if (response.status <= 300) {
                return {
                    statusCode: response.status,
                    data: response.data
                };
            }
            return {
                statusCode: response.status,
                errors: [response.status.toString() + " " + response.statusText]
            };
        } catch (error) {
            return {
                statusCode: (error as AxiosError)?.status ?? 0,
                errors: [(error as AxiosError).message ?? ""]
            };
        }
    }

    async getDetailedAsync(): Promise<IResultObject<IShoppingList>> {
        try {
            const response = await this.axiosInstance.get<IShoppingList>(`${this.basePath}/detailed`);
            return {
                statusCode: response.status,
                data: response.data,
            };
        } catch (error: any) {
            return {
                statusCode: error?.response?.status ?? 0,
                errors: [error?.message ?? "Unknown error"]
            };
        }
    }
}