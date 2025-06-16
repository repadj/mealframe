"use client";

import { useForm } from "react-hook-form";
import { INutritionGoalAdd } from "@/types/domain/INutritionGoalAdd";
import { useEffect } from "react";
import "@/app/nutritionGoals/nutrition.css";

type Props = {
    goal?: INutritionGoalAdd;
    onSave: (goal: INutritionGoalAdd) => void;
};

export default function NutritionGoalForm({ goal, onSave }: Props) {
    const { register, handleSubmit, reset } = useForm<INutritionGoalAdd>({
        defaultValues: goal || {
            calorieTarget: 0,
            proteinTarget: 0,
            fatTarget: 0,
            sugarTarget: 0,
            carbsTarget: 0,
            saltTarget: 0,
        }
    });

    useEffect(() => {
        if (goal) reset(goal);
    }, [goal, reset]);

    const onSubmit = (data: INutritionGoalAdd) => {
        onSave(data);
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="nutrition-form">
            <h2 className="form-title">
                {goal ? "Change" : "Create"} your nutrition goal
            </h2>

            <div className="nutrition-form-grid">
                {["calorie", "protein", "fat", "sugar", "carbs", "salt"].map((macro) => (
                    <div className="nutrition-form-group" key={macro}>
                        <label className="nutrition-form-label">{macro} target (grams)</label>
                        <input
                            type="number"
                            step="any"
                            {...register(`${macro}Target` as keyof INutritionGoalAdd, { required: true, valueAsNumber: true })}
                            className="nutrition-input"
                        />
                    </div>
                ))}
            </div>

            <button type="submit" className="button submit-button">
                {goal ? "Update" : "Save"}
            </button>
        </form>
    );


}
