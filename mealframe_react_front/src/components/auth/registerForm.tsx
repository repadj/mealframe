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
    firstName: string;
    lastName: string;
};

export default function RegisterForm({ onSuccess }: Props) {
    const accountService = new AccountService();
    const { setAccountInfo } = useContext(AccountContext);
    const [errorMessage, setErrorMessage] = useState("");

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<Inputs>();

    const onSubmit: SubmitHandler<Inputs> = async (data) => {
        try {
            const result = await accountService.registerAsync(
                data.email,
                data.password,
                data.firstName,
                data.lastName
            );

            if (result.errors) {
                setErrorMessage(result.errors.join(", "));
                return;
            }

            setAccountInfo?.({
                jwt: result.data!.jwt,
                refreshToken: result.data!.refreshToken,
            });

            setErrorMessage("");
            onSuccess?.();
        } catch (error) {
            setErrorMessage("Registration failed - " + (error as Error).message);
        }
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <h2 className="text-center mb-4">Create a new account</h2>
            {errorMessage && <p className="text-danger">{errorMessage}</p>}

            <div className="form-group mb-3">
                <label>First Name</label>
                <input
                    type="text"
                    {...register("firstName", { required: true })}
                    className="custom-input"
                    placeholder="Mike"
                />
                {errors.firstName && <span className="text-danger">Required</span>}
            </div>

            <div className="form-group mb-3">
                <label>Last Name</label>
                <input
                    type="text"
                    {...register("lastName", { required: true })}
                    className="custom-input"
                    placeholder="Fruit"
                />
                {errors.lastName && <span className="text-danger">Required</span>}
            </div>

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
                    {...register("password", { required: true, minLength: 6 })}
                    className="custom-input"
                    placeholder="Very hard password"
                />
                {errors.password && (
                    <span className="text-danger">Minimum 6 characters</span>
                )}
            </div>

            <button type="submit" className="btn btn-primary w-100">
                Register
            </button>
        </form>
    );
}