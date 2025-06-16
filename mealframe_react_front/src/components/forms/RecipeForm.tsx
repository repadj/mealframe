"use client";

import { useForm, useFieldArray } from "react-hook-form";
import { IRecipeAdd } from "@/types/domain/IRecipeAdd";
import { useEffect, useState } from "react";
import { ProductService } from "@/services/ProductService";
import { IProduct } from "@/types/domain/IProduct";
import { IRecipeDetail } from "@/types/domain/IRecipeDetail";
import "@/app/recipes/recipes.css";

type Props = {
    onSave: (data: IRecipeAdd) => void;
    initial?: IRecipeAdd | IRecipeDetail;
};

export default function RecipeForm({ onSave, initial }: Props) {
    const { register, handleSubmit, control, watch, reset } = useForm<IRecipeAdd>({
        defaultValues: initial ?? {
            recipeName: "",
            description: "",
            cookingTime: 0,
            servings: 0,
            pictureUrl: "",
            public: false,
            ingredients: [],
        },
    });

    const { fields, append, remove } = useFieldArray({
        control,
        name: "ingredients",
    });

    const [products, setProducts] = useState<IProduct[]>([]);
    const [imageLoaded, setImageLoaded] = useState(true);

    useEffect(() => {
        if (initial) {
            reset(initial);
        }
    }, [initial, reset]);

    useEffect(() => {
        const fetchProducts = async () => {
            const result = await new ProductService().getAllAsync();
            if (result.data) {
                setProducts(result.data);
            }
        };

        fetchProducts();
    }, []);

    useEffect(() => {
        if (initial && products.length > 0) {
            reset(initial);
        }
    }, [initial, products, reset]);

    useEffect(() => {
        setImageLoaded(true);
    }, [watch("pictureUrl")]);

    return (
        <form onSubmit={handleSubmit(onSave)} className="recipe-form-wrapper">
            <h2 className="form-title">Create Recipe</h2>

            <div className="recipe-form-grid-2col">
                <div className="form-left">
                    <div className="form-group">
                        <label className="form-label">Recipe Name</label>
                        <input {...register("recipeName", { required: true })} className="recipe-input" />
                    </div>
                    <div className="form-group">
                        <label className="form-label">Cooking Time (min)</label>
                        <input type="number" {...register("cookingTime", { required: true, valueAsNumber: true })} className="recipe-input" />
                    </div>
                    <div className="form-group">
                        <label className="form-label">Servings</label>
                        <input type="number" {...register("servings", { required: true, valueAsNumber: true })} className="recipe-input" />
                    </div>
                    <div className="recipe-checkbox">
                        <input type="checkbox" {...register("public")} />
                        <label className="form-label mt-2">Public recipe</label>
                    </div>
                </div>

                <div className="form-group">
                    <label className="form-label">Description</label>
                    <textarea {...register("description", { required: true })} className="recipe-input description-input" />
                </div>
            </div>

            <div className="form-group">
                <label className="form-label">Image URL</label>
                <input
                    {...register("pictureUrl", { required: true })}
                    placeholder="https://example.com/image.jpg"
                    className="recipe-input"
                />

                {watch("pictureUrl") && imageLoaded && (
                    <img
                        src={watch("pictureUrl")}
                        alt="Image Preview"
                        className="image-preview"
                        onError={() => setImageLoaded(false)}
                    />
                )}
            </div>

            {fields.length > 0 && (
                <>
                    <h3 className="ingredients-title">Ingredients</h3>
                    {fields.map((field, index) => (
                        <div key={field.id} className="ingredient-list">
                            <div className="ingredient-item">
                                <select {...register(`ingredients.${index}.productId`)} required className="product-input">
                                    <option value="">Select product</option>
                                    {products.map((product) => (
                                        <option key={product.id} value={product.id}>
                                            {product.productName}
                                        </option>
                                    ))}
                                </select>
                                <input
                                    type="number"
                                    step="any"
                                    {...register(`ingredients.${index}.amount` as const, { valueAsNumber: true })}
                                    placeholder="Amount"
                                    className="product-input"
                                />
                                <span className="ingredient-unit">Grams</span>
                                <button type="button" onClick={() => remove(index)} className="ingredient-delete">×</button>
                            </div>
                        </div>
                    ))}
                </>
            )}


            <button type="button" onClick={() => append({ productId: "", amount: 0, unit: "Grams" })} className="button ingredient-button">
                Add Ingredient
            </button>

            <button type="submit" className="button">
                Save Recipe
            </button>
        </form>

    );
}

