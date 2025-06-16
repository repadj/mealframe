import {IDomainId} from "@/types/IDomainId";

export interface IRecipe extends IDomainId {
    recipeName: string;
    description: string;
    cookingTime: number;
    servings: number;
    pictureUrl: string;
    public: boolean;
    ingredients: IRecipeIngredient[];
}

export interface IRecipeIngredient extends IDomainId {
    productId: string;
    amount: number;
    unit: string;
}