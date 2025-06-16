"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { MealPlanService } from "@/services/MealPlanService";
import { IMealPlanDetail } from "@/types/domain/IMealPlan";
import { MealTypeMap, UnitMap } from "@/helpers/mealEnums";
import { format } from "date-fns";
import Macrobar from "@/components/sidebars/Macrobar";
import "@/app/home/home.css";

export default function HomePage() {
    const [mealPlan, setMealPlan] = useState<IMealPlanDetail | null>(null);
    const service = new MealPlanService();
    const router = useRouter();

    useEffect(() => {
        const fetchMealPlan = async () => {
            const result = await service.getAllDetailedAsync();
            const today = new Date().toISOString().split("T")[0];

            if (result.data) {
                const todayPlan = result.data.find(plan => plan.date === today);
                if (todayPlan) setMealPlan(todayPlan);
            }
        };

        fetchMealPlan();
    }, []);

    const groupedEntries: Record<number, IMealPlanDetail["mealEntries"]> = {};
    mealPlan?.mealEntries.forEach((entry) => {
        const key = Number(entry.mealType);
        if (!groupedEntries[key]) groupedEntries[key] = [];
        groupedEntries[key].push(entry);
    });

    return (
        <>
            <div className="main-content home-page">
                <div className="home-container">
                    <h1 className="page-title">Today's Meal Plan</h1>
                    <hr className="home-line"></hr>

                    {mealPlan ? (
                        <div className="home-mealplan">
                            <div className="home-mealplan-header">
                                <h1 className="home-title">{mealPlan.planName}</h1>
                                <p className="home-date">
                                    {format(new Date(mealPlan.date), "dd MMMM yyyy")}
                                </p>
                            </div>

                            <div className="home-entries">
                                {Object.entries(groupedEntries).map(([mealType, entries]) => (
                                    <div key={mealType} className="home-group">
                                        <h2 className="home-mealtype">{MealTypeMap[Number(mealType)]}</h2>
                                        <ul className="home-entry-list">
                                            {entries.map((entry) => (
                                                <li key={entry.id} className="home-entry-item">
                                                    <span className="home-entry-name">
                                                        {entry.productName || entry.recipeName || "Unnamed"}
                                                    </span>

                                                    <div className="home-entry-right">
                                                        {!entry.recipeId && (
                                                            <span className="home-entry-amount">{entry.amount} {UnitMap[entry.unit]}</span>
                                                        )}
                                                        {entry.recipeId && (
                                                            <button
                                                                className="home-recipe-link"
                                                                onClick={() => router.push(`/recipes/${entry.recipeId}`)}
                                                            >
                                                                Recipe
                                                            </button>
                                                        )}
                                                    </div>
                                                </li>

                                            ))}
                                        </ul>
                                    </div>
                                ))}
                            </div>

                            <button onClick={() => router.push(`/mealplans/${mealPlan.id}`)} className="home-button">
                                Meal plan settings
                            </button>
                        </div>
                    ) : (
                        <div className="home-empty">
                            <p>No meal plan was found for today.</p>
                            <button onClick={() => router.push("/mealplans")} className="home-button">
                                Create a Meal Plan
                            </button>
                        </div>
                    )}
                </div>
            </div>


            <div className="right-sidebar">
                <Macrobar />
            </div>
        </>
    );
}


