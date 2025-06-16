import {EntityService} from "@/services/EntityService";
import {IMealPlan, IMealPlanDetail} from "@/types/domain/IMealPlan";
import {IMealPlanAdd} from "@/types/domain/IMealPlanAdd";
import {IResultObject} from "@/types/IResultObject";
import {AxiosError} from "axios";
import {IMealPlanMacroSummary} from "@/types/domain/IMealPlanMacroSummary";

export class MealPlanService extends EntityService<IMealPlan, IMealPlanAdd> {
    constructor(){
        super('mealPlans');
    }

    async getAllDetailedAsync(): Promise<IResultObject<IMealPlanDetail[]>> {
        try {
            const response = await this.axiosInstance.get<IMealPlanDetail[]>(this.basePath + "/detailed");

            if (response.status <= 300) {
                return {
                    statusCode: response.status,
                    data: response.data
                };
            }

            return {
                statusCode: response.status,
                errors: [(response.status.toString() + " " + response.statusText).trim()]
            };
        } catch (error) {
            return {
                statusCode: (error as AxiosError)?.status ?? 0,
                errors: [(error as AxiosError).code ?? ""]
            };
        }
    }

    async getByIdDetailedAsync(id: string): Promise<IResultObject<IMealPlanDetail>> {
        try {
            const response = await this.axiosInstance.get<IMealPlanDetail>(`${this.basePath}/${id}/detailed`);

            if (response.status <= 300) {
                return {
                    statusCode: response.status,
                    data: response.data
                };
            }

            return {
                statusCode: response.status,
                errors: [(response.status.toString() + " " + response.statusText).trim()]
            };
        } catch (error) {
            return {
                statusCode: (error as AxiosError)?.status ?? 0,
                errors: [(error as AxiosError).code ?? ""]
            };
        }
    }

    async getMacrosAsync(id: string): Promise<IResultObject<IMealPlanMacroSummary>> {
        try {
            const response = await this.axiosInstance.get<IMealPlanMacroSummary>(`${this.basePath}/${id}/macros`);

            return {
                statusCode: response.status,
                data: response.data
            };
        } catch (error: any) {
            return {
                statusCode: error?.response?.status ?? 0,
                errors: [error?.message ?? "Unknown error"]
            };
        }
    }


}