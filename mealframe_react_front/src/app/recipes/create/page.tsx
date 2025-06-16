"use client";

import { useRouter } from "next/navigation";
import { RecipeService } from "@/services/RecipeService";
import RecipeForm from "@/components/forms/RecipeForm";
import {IRecipeAdd} from "@/types/domain/IRecipeAdd";

export default function CreateRecipePage() {
    const router = useRouter();
    const service = new RecipeService();

    const handleSave = async (data: IRecipeAdd) => {
        const result = await service.addAsync(data);
        if (!result.errors) router.push("/recipes");
    };

    return <RecipeForm onSave={handleSave} />;
}