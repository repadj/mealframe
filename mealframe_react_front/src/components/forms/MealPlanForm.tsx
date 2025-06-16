"use client";

import { useForm, useFieldArray } from "react-hook-form";
import { IMealPlanAdd } from "@/types/domain/IMealPlanAdd";
import { IProduct } from "@/types/domain/IProduct";
import { IRecipe } from "@/types/domain/IRecipe";
import { ProductService } from "@/services/ProductService";
import { RecipeService } from "@/services/RecipeService";
import { MealTypeMap } from "@/helpers/mealEnums";
import { useEffect, useState } from "react";
import "@/app/mealplans/mealplans.css";

type Props = {
    onSave: (data: IMealPlanAdd) => void;
    initial?: IMealPlanAdd;
};

export default function MealPlanForm({ onSave, initial }: Props) {
    const { register, handleSubmit, control, reset, getValues, setValue } = useForm<IMealPlanAdd>({
        defaultValues: initial ?? {
            planName: "",
            date: "",
            mealEntries: [],
        },
    });

    const { fields, append, remove } = useFieldArray({
        control,
        name: "mealEntries",
    });

    const [products, setProducts] = useState<IProduct[]>([]);
    const [recipes, setRecipes] = useState<IRecipe[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            const productService = new ProductService();
            const recipeService = new RecipeService();
            const [fetchedProducts, fetchedRecipes] = await Promise.all([
                productService.getAllAsync(),
                recipeService.getAllAsync(),
            ]);
            setProducts(fetchedProducts.data ?? []);
            setRecipes(fetchedRecipes.data ?? []);
        };

        fetchData();
    }, []);

    useEffect(() => {
        if (initial) {
            reset(initial);
        }
    }, [initial, reset]);

    return (
        <form onSubmit={handleSubmit(onSave)} className="mealplan-form space-y-6">
            <h2 className="mealplan-form-title">
                {initial ? "Edit Meal Plan" : "Create Meal Plan"}
            </h2>

            <div className="mealplan-entry-row">
                <div className="form-group">
                    <label className="form-label">Plan Name</label>
                    <input
                        {...register("planName", { required: true })}
                        className="mealplan-input"
                        placeholder="Enter plan name"
                    />
                </div>
                <div className="form-group">
                    <label className="form-label">Date</label>
                    <input
                        type="date"
                        {...register("date", { required: true })}
                        className="mealplan-input"
                    />
                </div>
            </div>

            <h3 className="mealplan-section-title">Entries</h3>

            <div className="mealplan-entry-list">
                {fields.map((field, index) => {
                    const entry = getValues(`mealEntries.${index}`);
                    const isRecipe = !!entry?.recipeId;

                    return (
                        <div key={field.id} className="mealplan-entry-item">
                            <div className="mealplan-entry-group">
                                <div>
                                    <label className="form-label">Amount (1 for recipe)</label>
                                    <input
                                        type="number"
                                        step="any"
                                        {...register(`mealEntries.${index}.amount` as const, { valueAsNumber: true })}
                                        className="mealplan-input"
                                        disabled={isRecipe}
                                    />
                                </div>
                                <div>
                                    <label className="form-label">Unit (Fixed)</label>
                                    <div className="mealplan-unit">
                                        {entry?.unit || (isRecipe ? "servings" : "grams")}
                                    </div>
                                </div>
                            </div>

                            <div className="mealplan-entry-group">
                                <div>
                                    <label className="form-label">Product</label>
                                    <select
                                        className="mealplan-input"
                                        value={entry?.productId || ""}
                                        onChange={(e) => {
                                            const value = e.target.value || null;
                                            const entries = getValues("mealEntries") || [];
                                            const updatedEntries = [...entries];
                                            updatedEntries[index] = {
                                                ...updatedEntries[index],
                                                productId: value === "" ? null : value,
                                                recipeId: null,
                                                unit: value ? "grams" : "",
                                                amount: updatedEntries[index].amount || 0,
                                            };
                                            reset({ ...getValues(), mealEntries: updatedEntries });
                                        }}
                                    >
                                        <option value="">Select Product</option>
                                        {products.map((product) => (
                                            <option key={product.id} value={product.id}>
                                                {product.productName}
                                            </option>
                                        ))}
                                    </select>
                                </div>

                                <div>
                                    <label className="form-label">Recipe</label>
                                    <select
                                        className="mealplan-input"
                                        value={entry?.recipeId || ""}
                                        onChange={(e) => {
                                            const value = e.target.value || null;
                                            const entries = getValues("mealEntries") || [];
                                            const updatedEntries = [...entries];
                                            updatedEntries[index] = {
                                                ...updatedEntries[index],
                                                recipeId: value === "" ? null : value,
                                                productId: null,
                                                unit: value ? "servings" : "",
                                                amount: value ? 1 : 0, // Force amount to 1 when recipe
                                            };
                                            reset({ ...getValues(), mealEntries: updatedEntries });
                                        }}
                                    >
                                        <option value="">Select Recipe</option>
                                        {recipes.map((recipe) => (
                                            <option key={recipe.id} value={recipe.id}>
                                                {recipe.recipeName}
                                            </option>
                                        ))}
                                    </select>
                                </div>
                            </div>

                            <div className="mealplan-entry-group">
                                <div>
                                    <label className="form-label">Meal Type</label>
                                    <select
                                        {...register(`mealEntries.${index}.mealType` as const, { valueAsNumber: true })}
                                        className="mealplan-input"
                                    >
                                        <option value="">Select Meal Type</option>
                                        {Object.entries(MealTypeMap).map(([key, label]) => (
                                            <option key={key} value={key}>
                                                {label}
                                            </option>
                                        ))}
                                    </select>
                                </div>
                                <div>
                                    <button type="button" onClick={() => remove(index)} className="mealplan-entry-delete">
                                        Remove
                                    </button>
                                </div>
                            </div>
                        </div>
                    );
                })}
            </div>

            <button
                type="button"
                onClick={() => append({ amount: 0, mealType: "", unit: "", productId: null, recipeId: null })}
                className="button mt-3"
            >
                ➕ Add Entry
            </button>

            <button type="submit" className="button">
                Save Meal Plan
            </button>
        </form>
    );
}
