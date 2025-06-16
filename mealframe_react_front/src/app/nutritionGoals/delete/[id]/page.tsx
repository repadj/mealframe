"use client";

import { useRouter, useParams } from "next/navigation";
import { useContext, useEffect, useState } from "react";
import { AccountContext } from "@/context/AccountContext";
import { NutritionalGoalService } from "@/services/NutritionalGoalService";
import "@/app/nutritionGoals/nutrition.css";

export default function DeleteNutritionGoalPage() {
    const { accountInfo } = useContext(AccountContext);
    const router = useRouter();
    const params = useParams();
    const goalId = params?.id as string;

    const service = new NutritionalGoalService();

    useEffect(() => {
        if (!accountInfo?.jwt) {
            router.push("/");
        }
    }, [accountInfo, router]);

    const handleDelete = async () => {
        if (!goalId) return;
        const result = await service.deleteAsync(goalId);
        router.push("/nutritionGoals");
    };

    const handleCancel = () => {
        router.push("/nutritionGoals");
    };

    return (
        <div className="nutrition-delete-container">
            <h1 className="nutrition-heading">Deleting nutritional goal!</h1>
            <p className="mb-4">Are you sure you want to delete this nutrition goal?</p>
            <div className="nutrition-actions">
                <button className="button" onClick={handleDelete}>Yes, Delete</button>
                <button className="button" onClick={handleCancel}>Cancel</button>
            </div>
        </div>
    );
}
