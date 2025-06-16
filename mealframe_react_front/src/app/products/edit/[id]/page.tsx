"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import { ProductService } from "@/services/ProductService";
import { IProduct } from "@/types/domain/IProduct";
import { IProductAdd } from "@/types/domain/IProductAdd";
import ProductForm from "@/components/forms/ProductForm";

export default function EditProduct() {
    const { id } = useParams();
    const router = useRouter();
    const [product, setProduct] = useState<IProduct | null>(null);
    const service = new ProductService();

    useEffect(() => {
        const fetch = async () => {
            const result = await service.getAsync(id as string);
            if (!result.errors && result.data) {
                setProduct(result.data);
            }
        };
        fetch();
    }, [id]);

    const handleSave = async (data: IProductAdd, _id?: string) => {
        const result = await service.updateAsync(id as string, {
            ...data,
            id: id as string,
        });

        if (!result.errors) router.push("/products");
    };

    if (!product) {
        return <p className="text-white">Loading...</p>;
    }

    return <ProductForm initial={product} onSubmit={handleSave} />;
}


