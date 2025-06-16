"use client"

import NutritionGoalForm from "@/components/forms/NutritionalGoalForm";
import { NutritionalGoalService } from "@/services/NutritionalGoalService";
import { useRouter } from "next/navigation";

export default function CreateNutritionGoal() {
    const router = useRouter();
    const service = new NutritionalGoalService();

    const handleSave = async (goalData: any) => {
        const result = await service.addAsync(goalData);
        if (!result.errors) router.push("/nutritionGoals");
    };

    return <NutritionGoalForm onSave={handleSave} />;
}
