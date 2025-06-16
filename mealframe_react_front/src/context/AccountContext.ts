"use client"

import {createContext} from "react";

export interface IAccountInfo {
    jwt?: string,
    refreshToken?: string,
}

export interface IAccountState {
    accountInfo: IAccountInfo | undefined;
    setAccountInfo: (value: IAccountInfo | undefined) => void;
}

export const AccountContext = createContext<IAccountState>({
    accountInfo: undefined,
    setAccountInfo: () => {},
});
