"use client";
import { SubmitHandler, useForm } from "react-hook-form";
import { useContext, useState } from "react";
import { AccountService } from "@/services/AccountService";
import { AccountContext } from "@/context/AccountContext";

type Props = {
    onSuccess?: () => void;
};

type Inputs = {
    email: string;
    password: string;
};

export default function LoginForm({ onSuccess }: Props) {
    const accountService = new AccountService();
    const { setAccountInfo } = useContext(AccountContext);
    const [errorMessage, setErrorMessage] = useState("");

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<Inputs>({
        defaultValues: {
            email: "",
            password: "",
        },
    });

    const onSubmit: SubmitHandler<Inputs> = async (data) => {
        try {
            const result = await accountService.loginAsync(data.email, data.password);
            if (result.errors) {
                setErrorMessage(result.statusCode + " " + result.errors[0]);
                return;
            }

            setAccountInfo?.({
                jwt: result.data!.jwt,
                refreshToken: result.data!.refreshToken,
            });

            setErrorMessage("");
            onSuccess?.();
        } catch (error) {
            setErrorMessage("Login failed - " + (error as Error).message);
        }
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <h2 className="text-center mb-4">Log in with an existing account</h2>
            {errorMessage && <p className="text-danger">{errorMessage}</p>}

            <div className="form-group mb-3">
                <label>Email</label>
                <input
                    type="email"
                    {...register("email", { required: true })}
                    className="custom-input"
                    placeholder="Fruit@example.com"
                />
                {errors.email && <span className="text-danger">Required</span>}
            </div>

            <div className="form-group mb-3">
                <label>Password</label>
                <input
                    type="password"
                    {...register("password", { required: true })}
                    className="custom-input"
                    placeholder="Very secret password"
                />
                {errors.password && <span className="text-danger">Required</span>}
            </div>

            <button type="submit" className="btn btn-primary w-100">
                Log In
            </button>
        </form>
    );
}