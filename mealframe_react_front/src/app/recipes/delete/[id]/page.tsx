'use client';

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import { RecipeService } from "@/services/RecipeService";
import { IRecipeDetail } from "@/types/domain/IRecipeDetail";

export default function DeleteRecipePage() {
    const { id } = useParams();
    const router = useRouter();
    const service = new RecipeService();

    const [recipe, setRecipe] = useState<IRecipeDetail | null>(null);

    useEffect(() => {
        const fetchRecipe = async () => {
            const result = await service.getDetailedAsync(id as string);
            if (result.data) {
                setRecipe(result.data);
            }
        };
        if (id) fetchRecipe();
    }, [id]);

    const handleDelete = async () => {
        const result = await service.deleteAsync(id as string); // assumes you call your API here
        if (!result.errors) {
            router.push("/recipes");
        }
    };

    if (!recipe) return <p className="text-white">Loading...</p>;

    return (
        <div className="delete-recipe-container">
            <h1 className="delete-recipe-title">Delete Recipe</h1>
            <p className="delete-recipe-text">
                Are you sure you want to delete <strong>{recipe.recipeName}</strong>?
            </p>
            <div className="delete-recipe-actions">
                <button onClick={handleDelete} className="button">Confirm</button>
                <button onClick={() => router.push("/recipes")} className="button">Cancel</button>
            </div>
        </div>
    );

}

