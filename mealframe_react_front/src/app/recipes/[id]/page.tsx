'use client';

import { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import { RecipeService } from "@/services/RecipeService";
import { IRecipeDetail } from "@/types/domain/IRecipeDetail";
import { IRecipeMacroSummary } from "@/types/domain/IRecipeMacroSummary";
import Link from "next/link";
import "@/app/recipes/recipes.css";

export default function RecipeDetailPage() {
    const { id } = useParams();
    const [recipe, setRecipe] = useState<IRecipeDetail | null>(null);
    const [macros, setMacros] = useState<IRecipeMacroSummary | null>(null);
    const service = new RecipeService();

    useEffect(() => {
        const fetchData = async () => {
            if (!id) return;
            const recipeResult = await service.getDetailedAsync(id as string);
            if (recipeResult.data) setRecipe(recipeResult.data);

            const macrosResult = await service.getMacrosAsync(id as string);
            if (macrosResult.data) setMacros(macrosResult.data);
        };
        fetchData();
    }, [id]);

    if (!recipe) return <p className="text-white">Loading...</p>;

    return (
        <div className="recipe-page-container">
            <h1 className="app-title">{recipe.recipeName}</h1>
            <div className="recipe-detail-wrapper">
                <img
                    src={recipe.pictureUrl}
                    alt={recipe.recipeName}
                    className="recipe-detail-image"
                    onError={(e) => {
                        e.currentTarget.onerror = null;
                        e.currentTarget.src = "/default-meal.png";
                    }}
                />

                <div className="recipe-meta-column">
                    <p><strong>Cooking Time:</strong> {recipe.cookingTime} minutes</p>
                    <p><strong>Servings:</strong> {recipe.servings}</p>
                    <p><strong>Visibility:</strong> {recipe.public ? "Public" : "Private"}</p>
                </div>
            </div>

            <p className="recipe-description-box"><strong>Description:</strong> {recipe.description}</p>


            <h2 className="section-title">Ingredients</h2>
            <ul className="ingredient-list">
                {recipe.ingredients.map((ingredient, index) => (
                    <li key={index} className="ingredient-item">
                        {ingredient.amount} {ingredient.unit} of <strong>{ingredient.productName}</strong>
                    </li>
                ))}
            </ul>

            <h2 className="section-title">Macros per Serving</h2>
            {macros ? (
                <div className="macros-grid">
                    <div className="macro-box"><span>Calories:</span> {macros.calories.toFixed(1)} kcal</div>
                    <div className="macro-box"><span>Protein:</span> {macros.protein.toFixed(1)} g</div>
                    <div className="macro-box"><span>Carbs:</span> {macros.carbs.toFixed(1)} g</div>
                    <div className="macro-box"><span>Fat:</span> {macros.fat.toFixed(1)} g</div>
                    <div className="macro-box"><span>Sugar:</span> {macros.sugar.toFixed(1)} g</div>
                    <div className="macro-box"><span>Salt:</span> {macros.salt.toFixed(1)} g</div>
                </div>
            ) : (
                <p className="text-gray-400">No macros available for this recipe.</p>
            )}

            <div className="recipe-controls">
                <Link href={`/recipes/edit/${id}`} className="button"> Edit Recipe</Link>
                <Link href={`/recipes/delete/${id}`} className="button">Delete Recipe</Link>
            </div>
        </div>
    );
}


