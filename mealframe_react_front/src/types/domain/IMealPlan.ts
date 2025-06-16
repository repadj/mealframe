export interface IMealPlan {
    id: string;
    planName: string;
    date: string;
}

export interface IMealPlanDetail extends IMealPlan {
    mealEntries: IMealEntryDetail[];
}

export interface IMealEntryDetail {
    id: string;
    amount: number;
    unit: string;
    mealType: string;
    productId?: string;
    productName?: string;
    recipeId?: string;
    recipeName?: string;
}