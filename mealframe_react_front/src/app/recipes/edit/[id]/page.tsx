"use client";

import {useParams, useRouter} from "next/navigation";
import {RecipeService} from "@/services/RecipeService";
import {useEffect, useState} from "react";
import {IRecipeAdd} from "@/types/domain/IRecipeAdd";
import {IRecipe} from "@/types/domain/IRecipe";
import RecipeForm from "@/components/forms/RecipeForm";

export default function EditRecipePage() {
    const { id } = useParams();
    const router = useRouter();
    const service = new RecipeService();

    const [initialRecipe, setInitialRecipe] = useState<IRecipeAdd | null>(null);

    useEffect(() => {
        const fetch = async () => {
            const result = await service.getDetailedAsync(id as string);
            if (result.data) {
                const detailed = result.data;
                const mapped: IRecipeAdd = {
                    recipeName: detailed.recipeName,
                    description: detailed.description,
                    cookingTime: detailed.cookingTime,
                    servings: detailed.servings,
                    pictureUrl: detailed.pictureUrl,
                    public: detailed.public,
                    ingredients: detailed.ingredients.map(i => ({
                        productId: i.productId,
                        amount: i.amount,
                        unit: i.unit
                    }))
                };
                setInitialRecipe(mapped);
            }
        };
        fetch();
    }, [id]);

    const handleSave = async (data: IRecipeAdd) => {
        const result = await service.updateAsync(id as string, { ...data, id } as IRecipe);
        if (!result.errors) {
            router.push("/recipes");
        }
    };

    if (!initialRecipe) return <p className="text-white">Loading...</p>;

    return <RecipeForm onSave={handleSave} initial={initialRecipe} />;
}

