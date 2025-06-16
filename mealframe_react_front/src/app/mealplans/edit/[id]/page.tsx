"use client";

import { useParams, useRouter } from "next/navigation";
import { MealPlanService } from "@/services/MealPlanService";
import { useEffect, useState } from "react";
import { IMealPlanAdd } from "@/types/domain/IMealPlanAdd";
import { IMealPlanDetail } from "@/types/domain/IMealPlan";
import MealPlanForm from "@/components/forms/MealPlanForm";

export default function EditMealPlanPage() {
    const { id } = useParams();
    const router = useRouter();
    const service = new MealPlanService();

    const [initialMealPlan, setInitialMealPlan] = useState<IMealPlanAdd | null>(null);

    useEffect(() => {
        const fetch = async () => {
            const result = await service.getByIdDetailedAsync(id as string);
            if (result.data) {
                const detailed = result.data;
                const mapped: IMealPlanAdd = {
                    planName: detailed.planName,
                    date: detailed.date,
                    mealEntries: detailed.mealEntries.map(e => ({
                        amount: e.amount,
                        unit: e.unit,
                        mealType: e.mealType,
                        productId: e.productId,
                        recipeId: e.recipeId,
                    })),
                };
                setInitialMealPlan(mapped);
            }
        };
        fetch();
    }, [id]);

    const handleSave = async (data: IMealPlanAdd) => {
        const result = await service.updateAsync(id as string, { ...data, id } as IMealPlanDetail);
        if (!result.errors) {
            router.push("/mealplans");
        }
    };

    if (!initialMealPlan) return <p className="text-white">Loading...</p>;

    return <MealPlanForm onSave={handleSave} initial={initialMealPlan} />;
}


