export interface IRecipeAdd {
    recipeName: string;
    description: string;
    cookingTime: number;
    servings: number;
    pictureUrl: string;
    public: boolean;
    ingredients: IRecipeIngredientAdd[];
}

export interface IRecipeIngredientAdd {
    productId: string;
    amount: number;
    unit: string;
}