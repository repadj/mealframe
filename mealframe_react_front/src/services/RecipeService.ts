import {EntityService} from "@/services/EntityService";
import {IRecipeAdd} from "@/types/domain/IRecipeAdd";
import {IRecipe} from "@/types/domain/IRecipe";
import {IResultObject} from "@/types/IResultObject";
import {IRecipeDetail} from "@/types/domain/IRecipeDetail";
import {IRecipeMacroSummary} from "@/types/domain/IRecipeMacroSummary";

export class RecipeService extends EntityService<IRecipe, IRecipeAdd> {
    constructor(){
        super('recipes')
    }

    async getDetailedAsync(id: string): Promise<IResultObject<IRecipeDetail>> {
        try {
            const response = await this.axiosInstance.get<IRecipeDetail>(`${this.basePath}/${id}/detailed`);
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

    async getMacrosAsync(id: string): Promise<IResultObject<IRecipeMacroSummary>> {
        try {
            const response = await this.axiosInstance.get<IRecipeMacroSummary>(`${this.basePath}/${id}/macros`);
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