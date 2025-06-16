"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import LoginForm from "@/components/auth/loginForm";
import RegisterForm from "@/components/auth/registerForm";
import "./landing.css";

export default function LandingPage() {
    const [showLogin, setShowLogin] = useState(true);
    const router = useRouter();

    return (
        <div className="landing-page">
            <div className="auth-box">
                <h1 className="title">MealFrame</h1>
                {showLogin ? (
                    <>
                        <LoginForm onSuccess={() => router.push("/home")} />
                        <p className="toggle-text">
                            Don't have an account?{" "}
                            <button onClick={() => setShowLogin(false)} className="toggle-button">
                                Register here
                            </button>
                        </p>
                    </>
                ) : (
                    <>
                        <RegisterForm onSuccess={() => router.push("/home")} />
                        <p className="toggle-text">
                            Already have an account?{" "}
                            <button onClick={() => setShowLogin(true)} className="toggle-button">
                                Log in
                            </button>
                        </p>
                    </>
                )}
            </div>
        </div>
    );
}

