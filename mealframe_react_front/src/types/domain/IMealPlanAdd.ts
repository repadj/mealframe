export interface IMealPlanAdd {
    planName: string;
    date: string;
    mealEntries?: IMealEntryAdd[];
}

export interface IMealEntryAdd {
    amount: number;
    unit: string;
    mealType: string;
    productId?: string | null;
    recipeId?: string | null;
}

export interface IMealPlanUpdate extends IMealPlanAdd {
    id: string;
}