"use client";

import { useRouter } from "next/navigation";
import { ShoppingListService } from "@/services/ShoppingListService";
import { MealPlanService } from "@/services/MealPlanService";
import { useEffect, useState } from "react";
import { IMealPlan } from "@/types/domain/IMealPlan";
import {IShoppingListAdd} from "@/types/domain/IShoppingListAdd";
import ShoppingListForm from "@/components/forms/ShoppingListForm";

export default function CreateShoppingListPage() {
    const [mealPlans, setMealPlans] = useState<IMealPlan[]>([]);
    const router = useRouter();
    const shoppingListService = new ShoppingListService();
    const mealPlanService = new MealPlanService();

    useEffect(() => {
        const fetchMealPlans = async () => {
            const result = await mealPlanService.getAllAsync();
            if (result.data) setMealPlans(result.data);
        };
        fetchMealPlans();
    }, []);

    const handleSave = async (data: IShoppingListAdd) => {
        const result = await shoppingListService.generateFromMealPlansAsync(data);
        if (!result.errors) {
            router.push("/shoppinglist");
        }
    };

    return <ShoppingListForm onSave={handleSave} mealPlans={mealPlans} />;
}
