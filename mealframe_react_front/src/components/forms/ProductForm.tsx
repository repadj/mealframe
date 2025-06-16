"use client";

import { useForm } from "react-hook-form";
import { IProduct } from "@/types/domain/IProduct";
import { IProductAdd } from "@/types/domain/IProductAdd";
import { ICategory } from "@/types/domain/ICategory";
import { useEffect, useState } from "react";
import { CategoryService } from "@/services/CategoryService";
import "@/app/products/products.css"

interface Props {
    initial?: IProduct;
    onSubmit: (data: IProductAdd, id?: string) => void;
}

export default function ProductForm({ initial, onSubmit }: Props) {
    const { register, handleSubmit, reset } = useForm<IProduct>({
        defaultValues: initial ?? {
            productName: "",
            calories: 0,
            protein: 0,
            carbs: 0,
            fat: 0,
            sugar: 0,
            salt: 0,
            categoryId: "",
        },
    });

    const [categories, setCategories] = useState<ICategory[]>([]);

    useEffect(() => {
        const fetchCategories = async () => {
            const result = await new CategoryService().getAllAsync();
            if (result.data) {
                setCategories(result.data);
            }
        };
        fetchCategories();
    }, []);

    useEffect(() => {
        if (initial && categories.length > 0) {
            reset(initial);
        }
    }, [initial, categories, reset]);

    const handleFormSubmit = (data: IProduct) => {
        const { id, ...productData } = data;
        onSubmit(productData, id);
    };

    return (
        <form onSubmit={handleSubmit(handleFormSubmit)} className="product-form">
            <h2 className="product-form-title text-center mb-4">
                {initial ? "Edit" : "Create"} Product
            </h2>

            <div className="form-group mb-3">
                <label className="form-label">Product's Name</label>
                <input
                    className="product-input"
                    {...register("productName", { required: true })}
                    placeholder="Enter product name"
                />
            </div>

            <div className="product-macro-grid">
                {["calories", "protein", "carbs", "fat", "sugar", "salt"].map((field) => (
                    <div className="form-group product-macro-item" key={field}>
                        <label className="form-label text-capitalize">{field} (Grams)</label>
                        <input
                            type="number"
                            step="any"
                            className="product-input"
                            {...register(field as keyof IProduct, {
                                required: true,
                                valueAsNumber: true,
                            })}
                        />
                    </div>
                ))}
            </div>

            <div className="form-group mb-4">
                <label className="form-label">Category</label>
                <select className="product-input" {...register("categoryId", { required: true })}>
                    <option value="">Select category</option>
                    {categories.map((cat) => (
                        <option key={cat.id} value={cat.id}>
                            {cat.categoryName}
                        </option>
                    ))}
                </select>
            </div>

            <button type="submit" className="button product-submit-button">
                {initial ? "Update" : "Save"}
            </button>
        </form>

    );

}
