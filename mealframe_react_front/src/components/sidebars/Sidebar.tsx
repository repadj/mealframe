"use client";

import { useContext } from "react";
import { AccountContext } from "@/context/AccountContext";
import { useRouter } from "next/navigation";
import { AccountService } from "@/services/AccountService";

export default function Sidebar() {
    const { accountInfo, setAccountInfo } = useContext(AccountContext);
    const router = useRouter();
    const accountService = new AccountService();

    const logout = async () => {
        if (accountInfo?.refreshToken) {
            await accountService.logoutAsync(accountInfo.refreshToken);
        }
        setAccountInfo?.(undefined);
        router.push("/");
    };

    return (
        <div>
            <h2 className="app-title">MealFrame</h2>
            <div className="sidebar-avatar">
                <div className="avatar-circle">
                    <svg width="135" height="135" viewBox="0 0 24 24" fill="none" stroke="#679f67" strokeWidth="1.2" strokeLinecap="round" strokeLinejoin="round">
                        <circle cx="12" cy="7" r="4" />
                        <path d="M4 20c1.5-4 5.5-6 8-6s6.5 2 8 6" />
                    </svg>
                </div>
            </div>
            <p className="welcome-text">Welcome!</p>

            <div className="nav-buttons">
                <button className="button logout-button" onClick={() => router.push("/home")}>
                    Dashboard
                </button>
                <button className="button logout-button" onClick={() => router.push("/products")}>
                    Products
                </button>
                <button className="button logout-button" onClick={() => router.push("/recipes")}>
                    Recipes
                </button>
                <button className="button logout-button" onClick={() => router.push("/mealplans")}>
                    Meal Plans
                </button>
                <button className="button logout-button" onClick={() => router.push("/shoppinglist")}>
                    Shopping List
                </button>
            </div>

            <button className="button logout-button" onClick={logout}>
                Log Out
            </button>
        </div>
    );
}

