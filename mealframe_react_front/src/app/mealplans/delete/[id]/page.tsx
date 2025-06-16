"use client";

import { useParams, useRouter } from "next/navigation";
import { MealPlanService } from "@/services/MealPlanService";
import { useEffect, useState } from "react";
import Link from "next/link";

export default function DeleteMealPlanPage() {
    const { id } = useParams();
    const router = useRouter();
    const service = new MealPlanService();

    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        setLoading(false);
    }, []);

    const handleDelete = async () => {
        if (!id) return;

        const result = await service.deleteAsync(id as string);
        if (!result.errors) {
            router.push("/mealplans");
        } else {
            setError("Failed to delete the meal plan. Please try again.");
        }
    };

    return (
        <div className="mealplan-delete-container">
            <h1 className="mealplan-delete-title">Delete Meal Plan</h1>

            {loading ? (
                <p className="text-white">Loading...</p>
            ) : (
                <>
                    <p className="mealplan-delete-text">
                        Are you sure you want to delete this meal plan?
                    </p>
                    {error && <p className="mealplan-delete-error">{error}</p>}
                    <div className="mealplan-actions">
                        <button onClick={handleDelete} className="button">
                            Yes, Delete
                        </button>
                        <Link href="/mealplans" className="button">
                            Cancel
                        </Link>
                    </div>
                </>
            )}
        </div>
    );

}
