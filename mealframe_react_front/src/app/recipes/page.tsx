'use client';

import { useEffect, useState } from "react";
import Link from "next/link";
import { RecipeService } from "@/services/RecipeService";
import { IRecipe } from "@/types/domain/IRecipe";
import { IResultObject } from "@/types/IResultObject";
import "@/app/recipes/recipes.css"
import {IProduct} from "@/types/domain/IProduct";

export default function RecipesPage() {
    const [recipes, setRecipes] = useState<IRecipe[]>([]);
    const service = new RecipeService();
    const [filteredProducts, setFilteredProducts] = useState<IRecipe[]>([]);
    const [searchTerm, setSearchTerm] = useState('');

    useEffect(() => {
        const fetch = async () => {
            const result: IResultObject<IRecipe[]> = await service.getAllAsync();
            if (result.data) {
                setRecipes(result.data);
            }
        };
        fetch();
    }, []);

    useEffect(() => {
        const filtered = recipes.filter(r =>
            r.recipeName.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setFilteredProducts(filtered);
    }, [searchTerm, recipes]);

    return (
        <div className="recipe-page-container">
            <h1 className="app-title">Your Recipes</h1>

            <div className="recipe-page-controls">
                <Link href="/recipes/create" className="button product-add-button mb-3">
                    Create Recipe
                </Link>

                <input
                    type="text"
                    placeholder="Search recipes..."
                    className="recipe-search-input"
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                />
            </div>

            {recipes.length === 0 ? (
                <p className="text-white">You have no recipes yet.</p>
            ) : (
                <div className="recipe-grid">
                    {filteredProducts.map((recipe) => (
                        <Link key={recipe.id} href={`/recipes/${recipe.id}`} className="recipe-card">
                            <div>
                                <img
                                    src={recipe.pictureUrl}
                                    alt={recipe.recipeName}
                                    className="recipe-image"
                                    onError={(e) => {
                                        const target = e.currentTarget;
                                        target.onerror = null;
                                        target.src = "/default-meal.png";
                                    }}
                                />
                                <h3 className="recipe-title">{recipe.recipeName}</h3>
                                <p className="recipe-description">{recipe.description.slice(0, 38)}...</p>
                                <p className="recipe-meta">
                                    {recipe.cookingTime} min • {recipe.servings} servings
                                </p>
                            </div>
                        </Link>
                    ))}
                </div>
            )}
        </div>
    );

}
