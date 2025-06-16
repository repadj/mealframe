import axios, {AxiosInstance} from "axios";
import {ILoginDto} from "@/types/ILoginDto";

export abstract class BaseService {
    protected axiosInstance: AxiosInstance;

    private static refreshTokenPromise: Promise<string> | null = null;

    constructor() {
        this.axiosInstance
            = axios.create({
            baseURL: "http://localhost:5171/api/v1.0/",
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json",
            },
        });

        this.axiosInstance.interceptors.request.use(
            (config) => {
                const token = localStorage.getItem("_jwt");
                if (token) {
                    config.headers.Authorization = `Bearer ${token}`;
                }
                return config;
            },
            (error) => {
                return Promise.reject(error);
            }
        );

        this.axiosInstance.interceptors.response.use(
            (response) => {
                return response;
            },

            async (error) => {
                const originalRequest = error.config;
                if (error.response?.status === 401 && !originalRequest._retry) {
                    originalRequest._retry = true;

                    if (!BaseService.refreshTokenPromise) {
                        BaseService.refreshTokenPromise = (async () => {
                            const jwt = localStorage.getItem("_jwt");
                            const refreshToken = localStorage.getItem("_refreshToken");

                            const response = await axios.post<ILoginDto>(
                                "http://localhost:5171/api/v1.0/account/renewRefreshToken?jwtExpiresInSeconds=60",
                                { jwt, refreshToken }
                            );

                            const newJwt = response.data.jwt;
                            const newRefreshToken = response.data.refreshToken;

                            localStorage.setItem("_jwt", newJwt);
                            localStorage.setItem("_refreshToken", newRefreshToken);

                            return newJwt;
                        })();
                    }

                    try {
                        const newToken = await BaseService.refreshTokenPromise;
                        BaseService.refreshTokenPromise = null;

                        originalRequest.headers.Authorization = `Bearer ${newToken}`;
                        return this.axiosInstance(originalRequest);
                    } catch (refreshError) {
                        console.warn("Refresh token expired. Logging out...");
                        BaseService.refreshTokenPromise = null;
                        localStorage.removeItem("_jwt");
                        localStorage.removeItem("_refreshToken");
                        window.location.href = "/";
                        return Promise.reject(refreshError);
                    }
                }
                return Promise.reject(error);
            }
        );
    }
}
