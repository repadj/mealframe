"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import { ProductService } from "@/services/ProductService";
import { IProduct } from "@/types/domain/IProduct";

export default function DeleteProductPage() {
    const { id } = useParams();
    const router = useRouter();
    const service = new ProductService();

    const [product, setProduct] = useState<IProduct | null>(null);
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        const fetchProduct = async () => {
            const result = await service.getAsync(id as string);
            if (result.data) {
                setProduct(result.data);
            }
        };
        fetchProduct();
    }, [id]);

    const handleDelete = async () => {
        setLoading(true);
        setError(null);
        try {
            const result = await service.deleteAsync(id as string);
            if (!result.errors) {
                router.push("/products");
            } else {
                setError("This product is used in one or more recipes and cannot be deleted.");
            }
        } catch (err: any) {
            console.error("Delete failed:", err);
            setError("This product is used in one or more recipes and cannot be deleted.");
        } finally {
            setLoading(false);
        }
    };

    if (!product) return <p className="text-white">Loading...</p>;

    return (
        <div className="product-delete-container">
            <h1 className="product-delete-title">Delete Product</h1>
            <p className="product-delete-text">
                Make sure <strong>{product.productName}</strong> is not used in any recipes!!
            </p>

            {error && <p className="product-error">{error}</p>}

            <div className="product-delete-actions">
                <button onClick={handleDelete} className="button delete-button" disabled={loading}>
                    {loading ? "Deleting..." : "Confirm"}
                </button>
                <button onClick={() => router.push("/products")} className="button">
                    Cancel
                </button>
            </div>
        </div>
    );
}

