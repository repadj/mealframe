"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { ShoppingListService } from "@/services/ShoppingListService";
import { IShoppingList } from "@/types/domain/IShoppingList";
import { UnitMap } from "@/helpers/mealEnums";
import "@/app/shoppinglist/shoppinglist.css";

export default function ShoppingListPage() {
    const [shoppingList, setShoppingList] = useState<IShoppingList | null>(null);
    const [checkedItems, setCheckedItems] = useState<Record<string, boolean>>({});
    const [loading, setLoading] = useState(true);
    const service = new ShoppingListService();
    const router = useRouter();

    // Load checked state from localStorage
    useEffect(() => {
        const savedChecks = localStorage.getItem("shoppingListChecked");
        if (savedChecks) {
            setCheckedItems(JSON.parse(savedChecks));
        }
    }, []);

    // Save checked state to localStorage on change
    useEffect(() => {
        localStorage.setItem("shoppingListChecked", JSON.stringify(checkedItems));
    }, [checkedItems]);

    useEffect(() => {
        const fetchShoppingList = async () => {
            const result = await service.getDetailedAsync();
            if (result.data) {
                setShoppingList(result.data);
            } else {
                setShoppingList(null); // Explicitly set to null
            }
            setLoading(false);
        };
        fetchShoppingList();
    }, []);

    const toggleCheck = (itemId: string) => {
        setCheckedItems((prev) => ({
            ...prev,
            [itemId]: !prev[itemId],
        }));
    };

    if (loading) {
        return <p className="text-white">Loading your shopping list...</p>;
    }

    return (
        <>
            {shoppingList ? (
                <div className="shoppinglist-page">
                    <h1 className="shoppinglist-title">{shoppingList.sListName}</h1>

                    {shoppingList.shoppingItems && shoppingList.shoppingItems.length > 0 ? (
                        <ul className="shoppinglist-items">
                            {Object.entries(
                                shoppingList.shoppingItems.reduce<Record<string, typeof shoppingList.shoppingItems>>((acc, item) => {
                                    const category = item.productCategory ?? "Other";
                                    if (!acc[category]) acc[category] = [];
                                    acc[category].push(item);
                                    return acc;
                                }, {})
                            ).map(([category, items]) => (
                                <li key={category} className="shoppinglist-category-group">
                                    <h3 className="shoppinglist-category-title">{category}</h3>
                                    <ul className="shoppinglist-category-items">
                                        {items.map((item) => (
                                            <li key={item.id} className={`shoppinglist-item ${checkedItems[item.id] ? "checked" : ""}`}>
                                                <input
                                                    type="checkbox"
                                                    className="shoppinglist-checkbox"
                                                    checked={checkedItems[item.id] || false}
                                                    onChange={() => toggleCheck(item.id)}
                                                />
                                                <div className="shoppinglist-item-details">
                                                    <div className="shoppinglist-item-header">
                                                        <span className="shoppinglist-product-name">{item.productName}</span>
                                                    </div>
                                                    <div className="shoppinglist-item-quantity">
                                                        Quantity: <span className="shoppinglist-amount">{item.amount}</span>
                                                        <span className="shoppinglist-unit">{UnitMap[item.unit]}</span>
                                                    </div>
                                                </div>
                                            </li>
                                        ))}
                                    </ul>
                                </li>
                            ))}
                        </ul>
                    ) : (
                        <p className="text-gray-400 mt-4">No items in this shopping list.</p>
                    )}

                    <button
                        className="generate-button"
                        onClick={() => router.push("/shoppinglist/create")}
                    >
                        Generate New Shopping List
                    </button>
                </div>
            ) : (
                <div className="shoppinglist-empty">
                    <p className="text-gray-400 mb-4">No shopping list found.</p>
                    <button
                        className="generate-button"
                        onClick={() => router.push("/shoppinglist/create")}
                    >
                        Create New Shopping List
                    </button>
                </div>
            )}
        </>
    );

}



