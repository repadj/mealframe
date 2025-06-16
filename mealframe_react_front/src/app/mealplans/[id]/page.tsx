"use client";

import { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import { MealPlanService } from "@/services/MealPlanService";
import { IMealPlanDetail } from "@/types/domain/IMealPlan";
import { IMealPlanMacroSummary } from "@/types/domain/IMealPlanMacroSummary";
import "@/app/mealplans/mealplans.css";
import { MealTypeMap, UnitMap } from "@/helpers/mealEnums";
import { useRouter } from "next/navigation";

export default function MealPlanDetailPage() {
    const { id } = useParams<{ id: string }>();
    const [mealPlan, setMealPlan] = useState<IMealPlanDetail | null>(null);
    const [macros, setMacros] = useState<IMealPlanMacroSummary | null>(null);
    const service = new MealPlanService();
    const router = useRouter();

    useEffect(() => {
        const fetchData = async () => {
            if (!id) return;
            const [planResult, macrosResult] = await Promise.all([
                service.getByIdDetailedAsync(id),
                service.getMacrosAsync(id)
            ]);

            if (planResult.data) setMealPlan(planResult.data);
            if (macrosResult.data) setMacros(macrosResult.data);
        };
        fetchData();
    }, [id]);

    if (!mealPlan) {
        return <p className="mealplan-empty">Loading...</p>;
    }

    const groupedEntries: Record<number, IMealPlanDetail["mealEntries"]> = {};
    mealPlan.mealEntries.forEach((entry) => {
        const key = Number(entry.mealType);
        if (!groupedEntries[key]) groupedEntries[key] = [];
        groupedEntries[key].push(entry);
    });

    return (
        <div className="mealplan-single">
            <h1 className="mealplan-title">{mealPlan.planName}</h1>
            <p className="mealplan-single-date">Date: {mealPlan.date}</p>

            {mealPlan.mealEntries.length === 0 ? (
                <p className="mealplan-empty">This meal plan has no entries.</p>
            ) : (
                <div>
                    {Object.entries(groupedEntries).map(([mealType, entries]) => (
                        <div key={mealType} className="mealplan-group">
                            <h2 className="mealplan-entry-heading">{MealTypeMap[Number(mealType)]}</h2>
                            <ul className="mealplan-entry-list">
                                {entries.map((entry) => (
                                    <li key={entry.id} className="mealplan-entry-item">
                                        <span className="mealplan-entry-name">{entry.productName || entry.recipeName || "Unnamed"}</span>
                                        <span className="mealplan-entry-amount">{entry.amount} {UnitMap[entry.unit]}</span>
                                    </li>
                                ))}
                            </ul>
                        </div>
                    ))}
                </div>
            )}

            {macros && (
                <div className="mealplan-macros">
                    <h2 className="mealplan-macros-title">Nutritional Breakdown</h2>
                    <ul className="macro-listt">
                        <li><strong>Calories:</strong> {macros.calories.toFixed(0)} kcal</li>
                        <li><strong>Protein:</strong> {macros.protein.toFixed(1)} g</li>
                        <li><strong>Carbs:</strong> {macros.carbs.toFixed(1)} g</li>
                        <li><strong>Fat:</strong> {macros.fat.toFixed(1)} g</li>
                        <li><strong>Sugar:</strong> {macros.sugar.toFixed(1)} g</li>
                        <li><strong>Salt:</strong> {macros.salt.toFixed(1)} g</li>
                    </ul>
                </div>
            )}

            <div className="mealplan-actions">
                <button onClick={() => router.push(`/mealplans/edit/${id}`)} className="button">Edit</button>
                <button onClick={() => router.push(`/mealplans/delete/${id}`)} className="button">Delete</button>
            </div>
        </div>
    );
}

