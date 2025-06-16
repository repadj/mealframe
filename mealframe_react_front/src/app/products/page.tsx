'use client';

import { useEffect, useState } from "react";
import { IProduct } from "@/types/domain/IProduct";
import { ICategory } from "@/types/domain/ICategory";
import { ProductService } from "@/services/ProductService";
import { CategoryService } from "@/services/CategoryService";
import Link from "next/link";
import "@/app/products/products.css"

export default function ProductPage() {
    const [products, setProducts] = useState<IProduct[]>([]);
    const [categories, setCategories] = useState<ICategory[]>([]);
    const [filteredProducts, setFilteredProducts] = useState<IProduct[]>([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [loading, setLoading] = useState(true);

    const productService = new ProductService();
    const categoryService = new CategoryService();

    useEffect(() => {
        const fetchData = async () => {
            const [productResult, categoryResult] = await Promise.all([
                productService.getAllAsync(),
                categoryService.getAllAsync()
            ]);
            if (productResult.data) {
                setProducts(productResult.data);
                setFilteredProducts(productResult.data);
            }
            if (categoryResult.data) setCategories(categoryResult.data);
            setLoading(false);
        };
        fetchData();
    }, []);

    useEffect(() => {
        const filtered = products.filter(p =>
            p.productName.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setFilteredProducts(filtered);
    }, [searchTerm, products]);

    const getCategoryName = (categoryId: string) => {
        return categories.find(c => c.id === categoryId)?.categoryName ?? "Unknown";
    };

    if (loading) return <p className="text-white">Loading...</p>;

    return (
        <div className="product-page">
            <h1 className="product-title">Products</h1>

            <div className="product-controls">
                <Link href="/products/create" className="button product-add-button">Add Product</Link>

                <input
                    type="text"
                    placeholder="Search products..."
                    className="product-search-input"
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                />
            </div>

            <div className="product-grid">
                {filteredProducts.map(p => (
                    <div key={p.id} className="product-card">
                        <div className="product-header">
                            <h2 className="product-name">{p.productName}</h2>
                            <span className="product-category">{getCategoryName(p.categoryId)}</span>
                        </div>

                        <div className="product-macros">
                            <div><strong>Calories:</strong> {p.calories}</div>
                            <div><strong>Protein:</strong> {p.protein}g</div>
                            <div><strong>Carbs:</strong> {p.carbs}g</div>
                            <div><strong>Fat:</strong> {p.fat}g</div>
                            <div><strong>Sugar:</strong> {p.sugar}g</div>
                            <div><strong>Salt:</strong> {p.salt}g</div>
                        </div>

                        <div className="product-actions">
                            <Link href={`/products/edit/${p.id}`} className="button small">Edit</Link>
                            <Link href={`/products/delete/${p.id}`} className="button small">Delete</Link>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}
