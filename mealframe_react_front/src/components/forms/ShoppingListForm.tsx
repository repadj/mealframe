"use client";

import { useForm } from "react-hook-form";
import { IShoppingListAdd } from "@/types/domain/IShoppingListAdd";
import { IMealPlan } from "@/types/domain/IMealPlan";
import "@/app/shoppinglist/shoppinglist.css";

type Props = {
    onSave: (data: IShoppingListAdd) => void;
    mealPlans: IMealPlan[];
};

export default function ShoppingListForm({ onSave, mealPlans }: Props) {
    const { register, handleSubmit } = useForm<IShoppingListAdd>({
        defaultValues: {
            mealPlanIds: []
        },
    });

    return (
        <form onSubmit={handleSubmit(onSave)} className="shoppinglist-form">
            <h2 className="shoppinglist-form-title">Select Meal Plans</h2>

            <div className="shoppinglist-mealplans-grid">
                {mealPlans.map((mp) => (
                    <label key={mp.id} className="shoppinglist-mealplan-card">
                        <input type="checkbox" value={mp.id} {...register("mealPlanIds")} className="shoppinglist-checkbox" />
                        <div className="shoppinglist-mealplan-content">
                            <h3 className="shoppinglist-mealplan-name">{mp.planName}</h3>
                            <p className="shoppinglist-mealplan-date">{mp.date}</p>
                        </div>
                    </label>
                ))}
            </div>

            <button type="submit" className="generate-button">
                Generate Shopping List
            </button>
        </form>
    );
}

