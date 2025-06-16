"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { NutritionalGoalService } from "@/services/NutritionalGoalService";
import { MealPlanService } from "@/services/MealPlanService";
import { INutritionGoal } from "@/types/domain/INutritionGoal";
import { IMealPlanMacroSummary } from "@/types/domain/IMealPlanMacroSummary";
import "@/app/home/home.css";

export default function RightSidebar() {
    const router = useRouter();
    const [goal, setGoal] = useState<INutritionGoal | null>(null);
    const [macros, setMacros] = useState<IMealPlanMacroSummary | null>(null);
    const goalService = new NutritionalGoalService();
    const mealPlanService = new MealPlanService();

    useEffect(() => {
        const fetchData = async () => {
            const goalResult = await goalService.getAllAsync();
            if (goalResult.data) setGoal(goalResult.data[0] ?? null);

            const today = new Date().toISOString().split("T")[0];
            const mealPlans = await mealPlanService.getAllDetailedAsync();
            const todayPlan = mealPlans.data?.find(plan => plan.date === today);

            if (todayPlan) {
                const macroResult = await mealPlanService.getMacrosAsync(todayPlan.id);
                if (macroResult.data) setMacros(macroResult.data);
            }
        };

        fetchData();
    }, []);

    const macroItems = goal
        ? [
            { label: "Calories", target: goal.calorieTarget, value: macros?.calories ?? 0 },
            { label: "Protein", target: goal.proteinTarget, value: macros?.protein ?? 0 },
            { label: "Carbs", target: goal.carbsTarget, value: macros?.carbs ?? 0 },
            { label: "Fat", target: goal.fatTarget, value: macros?.fat ?? 0 },
            { label: "Sugar", target: goal.sugarTarget, value: macros?.sugar ?? 0 },
            { label: "Salt", target: goal.saltTarget, value: macros?.salt ?? 0 },
        ]
        : [];

    return (
        <div className="right-sidebar">
            <h3 className="macros-title">Macros</h3>

            {goal ? (
                <div className="macro-list">
                    {macroItems.map((macro) => {
                        const progress = Math.min((macro.value / macro.target) * 100, 100);
                        return (
                            <div key={macro.label} className="macrobar-item">
                                <div className="macro-header">
                                    <span>{macro.label}</span>
                                    <span>{macro.value.toFixed(0)} / {macro.target}</span>
                                </div>
                                <div className="macro-bar-wrapper">
                                    <div className="macro-bar" style={{ width: `${progress}%` }}></div>
                                </div>
                            </div>
                        );
                    })}

                    <button className="macro-button" onClick={() => router.push("/nutritionGoals")}>
                        Macro Settings
                    </button>
                </div>
            ) : (
                <>
                    <p className="macro-empty">No nutrition goal set.</p>
                    <button className="macro-button" onClick={() => router.push("/nutritionGoals")}>
                        Macro Settings
                    </button>
                </>
            )}
        </div>
    );
}


