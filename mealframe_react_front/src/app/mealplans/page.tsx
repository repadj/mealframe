"use client"

import { useEffect, useState } from "react";
import { MealPlanService } from "@/services/MealPlanService";
import { IMealPlanDetail } from "@/types/domain/IMealPlan";
import Link from "next/link";
import "@/app/mealplans/mealplans.css";
import { MealTypeMap, UnitMap } from "@/helpers/mealEnums";

export default function MealPlanListPage() {
    const [mealPlans, setMealPlans] = useState<IMealPlanDetail[]>([]);
    const [searchTerm, setSearchTerm] = useState("");
    const service = new MealPlanService();

    useEffect(() => {
        const fetchMealPlans = async () => {
            const result = await service.getAllDetailedAsync();
            if (result.data) setMealPlans(result.data);
        };
        fetchMealPlans();
    }, []);

    const filteredPlans = mealPlans.filter(plan =>
        plan.planName.toLowerCase().includes(searchTerm.toLowerCase())
    );

    return (
        <div className="mealplan-page">
            <h1 className="mealplan-title">Meal Plans</h1>

            <div className="mealplan-controls">
                <Link href="/mealplans/create" className="button mealplan-create-button">
                    Create Meal Plan
                </Link>
                <input
                    type="text"
                    placeholder="Search Meal Plans..."
                    className="mealplan-search-input"
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                />
            </div>

            <div className="mealplan-scroll-wrapper">
                <div className="mealplan-scroll-container">
                    {filteredPlans
                        .slice()
                        .sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime())
                        .map((plan) => {
                            const groupedEntries: Record<number, IMealPlanDetail["mealEntries"]> = {};
                            plan.mealEntries.forEach((entry) => {
                                const key = Number(entry.mealType);
                                if (!groupedEntries[key]) groupedEntries[key] = [];
                                groupedEntries[key].push(entry);
                            });

                            return (
                                <div key={plan.id} className="mealplan-card">
                                    <h2 className="mealplan-name">{plan.planName}</h2>
                                    <p className="mealplan-date">{plan.date}</p>

                                    {plan.mealEntries.length === 0 ? (
                                        <p className="mealplan-empty">Empty plan</p>
                                    ) : (
                                        <div className="mealplan-entry-list">
                                            {Object.entries(groupedEntries).map(([mealType, entries]) => (
                                                <div key={mealType}>
                                                    <h4 className="mealplan-entry-heading">{MealTypeMap[Number(mealType)]}</h4>
                                                    {entries.map((entry) => (
                                                        <div key={entry.id} className="mealplan-entry-item">
                                                            <span className="mealplan-entry-name">{entry.productName || entry.recipeName || "Unnamed"}</span>
                                                            <span className="mealplan-entry-amount">{entry.amount} {UnitMap[entry.unit]}</span>
                                                        </div>
                                                    ))}
                                                </div>
                                            ))}
                                        </div>
                                    )}

                                    <Link href={`/mealplans/${plan.id}`} className="mealplan-view-more">
                                        View More →
                                    </Link>
                                </div>
                            );
                        })}
                </div>
            </div>
        </div>
    );
}



