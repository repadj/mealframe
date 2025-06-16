"use client";

import { useRouter } from "next/navigation";
import { MealPlanService } from "@/services/MealPlanService";
import MealPlanForm from "@/components/forms/MealPlanForm";
import { IMealPlanAdd } from "@/types/domain/IMealPlanAdd";

export default function MealPlanCreatePage() {
    const router = useRouter();
    const service = new MealPlanService();

    const handleSave = async (data: IMealPlanAdd) => {
        const result = await service.addAsync(data);
        if (!result.errors) {
            router.push("/mealplans");
        }
    };

    return <MealPlanForm onSave={handleSave} />;
}