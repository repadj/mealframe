"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import { NutritionalGoalService } from "@/services/NutritionalGoalService";
import { INutritionGoal } from "@/types/domain/INutritionGoal";
import { INutritionGoalAdd } from "@/types/domain/INutritionGoalAdd";
import NutritionGoalForm from "@/components/forms/NutritionalGoalForm";

export default function EditNutritionGoal() {
    const { id } = useParams();
    const router = useRouter();
    const [goal, setGoal] = useState<INutritionGoal | null>(null); // ✅ full object including id
    const service = new NutritionalGoalService();

    useEffect(() => {
        const fetch = async () => {
            const result = await service.getAsync(id as string);

            if (!result.errors && result.data) {
                setGoal(result.data); // ✅ keep full INutritionGoal
            }
        };

        fetch();
    }, [id]);

    const handleSave = async (updatedGoal: INutritionGoalAdd) => {
        const result = await service.updateAsync(id as string, {
            ...updatedGoal,
            id: goal!.id // ✅ reattach the id here
        });

        if (!result.errors) {
            router.push("/nutritionGoals");
        }
    };

    if (!goal) {
        return <p className="text-white">Loading...</p>;
    }

    // ✅ Convert to INutritionGoalAdd before sending to form
    const goalData: INutritionGoalAdd = {
        calorieTarget: goal.calorieTarget,
        proteinTarget: goal.proteinTarget,
        fatTarget: goal.fatTarget,
        sugarTarget: goal.sugarTarget,
        carbsTarget: goal.carbsTarget,
        saltTarget: goal.saltTarget,
    };

    return <NutritionGoalForm goal={goalData} onSave={handleSave} />;
}


