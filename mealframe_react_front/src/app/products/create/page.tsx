"use client";

import { useRouter } from "next/navigation";
import { ProductService } from "@/services/ProductService";
import { IProductAdd } from "@/types/domain/IProductAdd";
import ProductForm from "@/components/forms/ProductForm";
import { useState } from "react";

export default function ProductCreatePage() {
    const router = useRouter();
    const productService = new ProductService();
    const [errorMessage, setErrorMessage] = useState("");

    const handleCreate = async (data: IProductAdd) => {
        setErrorMessage("Saving...");

        const result = await productService.addAsync(data);

        if (result.errors) {
            setErrorMessage(result.errors.join(", "));
            return;
        }

        setErrorMessage("");
        router.push("/products");
    };

    return (
        <div className="product-create-page">
            {errorMessage && <p className="product-error">{errorMessage}</p>}
            <ProductForm onSubmit={handleCreate} />
        </div>
    );
}
