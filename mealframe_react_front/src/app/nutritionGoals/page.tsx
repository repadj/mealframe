"use client";

import { useContext, useEffect, useState } from "react";
import { AccountContext } from "@/context/AccountContext";
import { useRouter } from "next/navigation";
import { NutritionalGoalService } from "@/services/NutritionalGoalService";
import { INutritionGoal } from "@/types/domain/INutritionGoal";
import Link from "next/link";
import "@/app/nutritionGoals/nutrition.css";

export default function NutritionGoalPage() {
    const { accountInfo } = useContext(AccountContext);
    const router = useRouter();
    const [goal, setGoal] = useState<INutritionGoal | null>(null);
    const [loading, setLoading] = useState(true);

    const service = new NutritionalGoalService();

    useEffect(() => {
        if (!accountInfo?.jwt) {
            router.push("/");
            return;
        }

        const fetchGoal = async () => {
            const result = await service.getAllAsync();
            if (result.errors) {
                console.error("Fetch error:", result.errors);
                return;
            }
            setGoal(result.data?.[0] ?? null);
            setLoading(false);
        };

        fetchGoal();
    }, []);

    if (loading) return <p className="nutrition-loading">Loading...</p>;

    return (
        <>
            {!goal ? (
                <div className="nutrition-empty-container">
                    <div className="nutrition-empty">
                        <h1 className="nutrition-heading">My Nutritional Goal</h1>
                        <p className="mb-4">You haven’t created a nutritional goal yet.</p>
                        <Link href="/nutritionGoals/create" className="button create-goal-button">
                            Create Goal
                        </Link>
                    </div>
                </div>
            ) : (
                <div className="nutrition-goal-container">
                    <h1 className="nutrition-heading">My Nutritional Goal</h1>
                    <div className="nutrition-goal-content">
                        <table className="nutrition-table">
                            <tbody>
                            <tr><td>Calories (Grams)</td><td className="macro">{goal.calorieTarget}</td></tr>
                            <tr><td>Protein (Grams)</td><td className="macro">{goal.proteinTarget}</td></tr>
                            <tr><td>Fat (Grams)</td><td className="macro">{goal.fatTarget}</td></tr>
                            <tr><td>Sugar (Grams)</td><td className="macro">{goal.sugarTarget}</td></tr>
                            <tr><td>Carbs (Grams)</td><td className="macro">{goal.carbsTarget}</td></tr>
                            <tr><td>Salt (Grams)</td><td className="macro">{goal.saltTarget}</td></tr>
                            </tbody>
                        </table>
                        <div className="nutrition-actions">
                            <Link href={`/nutritionGoals/edit/${goal.id}`} className="button">
                                Edit Goal
                            </Link>
                            <Link href={`/nutritionGoals/delete/${goal.id}`} className="button">
                                Delete Goal
                            </Link>
                        </div>
                    </div>
                </div>
            )}
        </>
    );

}

