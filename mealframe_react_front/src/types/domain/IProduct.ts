import {IDomainId} from "@/types/IDomainId";

export interface IProduct extends IDomainId {
    productName: string;
    calories: number;
    protein: number;
    fat: number;
    sugar: number;
    carbs: number;
    salt: number;
    categoryId: string;
}