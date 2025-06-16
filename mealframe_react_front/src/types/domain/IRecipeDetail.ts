export interface IRecipeDetail {
    id: string;
    recipeName: string;
    description: string;
    cookingTime: number;
    servings: number;
    pictureUrl: string;
    public: boolean;
    ingredients: IRecipeIngredientDetail[];
}

export interface IRecipeIngredientDetail {
    productId: string;
    amount: number;
    unit: string;
    productName: string;
}