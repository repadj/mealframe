"use client";

import 'bootstrap/dist/css/bootstrap.min.css';
import "./globals.css";
import BootstrapActivation from "@/helpers/BootstrapActivation";
import {AccountContext, IAccountInfo} from "@/context/AccountContext";
import {useEffect, useState} from "react";
import { usePathname } from "next/navigation";
import Sidebar from "@/components/sidebars/Sidebar";

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
    const pathname = usePathname();
    const [accountInfo, setAccountInfo] = useState<IAccountInfo | undefined>();
    const showSidebar = pathname !== "/";

    const updateAccountInfo = (value: IAccountInfo | undefined) => {
        setAccountInfo(value);
        if (value) {
            localStorage.setItem("_jwt", value.jwt!);
            localStorage.setItem("_refreshToken", value.refreshToken!);
        } else {
            localStorage.removeItem("_jwt");
            localStorage.removeItem("_refreshToken");
        }
    };

    useEffect(() => {
        const jwt = localStorage.getItem("_jwt");
        const refreshToken = localStorage.getItem("_refreshToken");

        if (jwt && refreshToken) {
            setAccountInfo({ jwt, refreshToken })
        }
    }, []);


    return (
        <html lang="en">
        <body>
        <AccountContext.Provider value={{ accountInfo, setAccountInfo: updateAccountInfo }}>
            {showSidebar ? (
                <div>
                    <div className="sidebar">
                        <Sidebar />
                    </div>
                    <div className="outer-container">
                        {children}
                    </div>
                </div>
            ) : ( children) }
            <BootstrapActivation />
        </AccountContext.Provider>
        </body>
        </html>
    );
}
