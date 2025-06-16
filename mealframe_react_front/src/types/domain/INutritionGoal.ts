import {IDomainId} from "@/types/IDomainId";

export interface INutritionGoal extends IDomainId {
    calorieTarget: number;
    proteinTarget: number;
    fatTarget: number;
    sugarTarget: number;
    carbsTarget: number;
    saltTarget: number;
}