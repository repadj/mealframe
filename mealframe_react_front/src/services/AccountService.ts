import {BaseService} from "@/services/BaseService";
import {ILoginDto} from "@/types/ILoginDto";
import {IResultObject} from "@/types/IResultObject";
import {AxiosError} from "axios";

export class AccountService extends BaseService {
    async loginAsync(email: string, password: string): Promise<IResultObject<ILoginDto>> {
        const url = "account/login";
        try {
            const loginData = {
                email: email,
                password: password
            }
            const response = await this.axiosInstance.post<ILoginDto>(url + "?jwtExpiresInSeconds=60", loginData);

            if (response.status <= 300) {
                return {
                    statusCode: response.status,
                    data: response.data
                }
            }

            return {
                statusCode: response.status,
                errors: [(response.status.toString() + " " + response.statusText).trim()]
            }
        } catch (error) {
            return {
                statusCode: (error as AxiosError)?.status,
                errors: [(error as AxiosError).code ?? "Unknown error"]
            }
        }
    }

    async registerAsync(
        email: string,
        password: string,
        firstName: string,
        lastName: string
    ): Promise<IResultObject<ILoginDto>> {
        const url = "account/register";
        try {
            const registerData = {
                email,
                password,
                firstName,
                lastName
            };

            const response = await this.axiosInstance.post<ILoginDto>(url + "?jwtExpiresInSeconds=60", registerData);

            if (response.status <= 300) {
                return {
                    statusCode: response.status,
                    data: response.data
                };
            }

            return {
                statusCode: response.status,
                errors: [(response.status.toString() + " " + response.statusText).trim()]
            };
        } catch (error) {
            return {
                statusCode: (error as AxiosError)?.status,
                errors: [(error as AxiosError).code ?? "Unknown error"]
            };
        }
    }

    async logoutAsync(refreshToken: string): Promise<IResultObject<any>> {
        const url = "account/logout";
        try {
            const response = await this.axiosInstance.post(url, {
                refreshToken: refreshToken,
            });

            if (response.status <= 300) {
                return {
                    statusCode: response.status,
                    data: response.data
                };
            }

            return {
                statusCode: response.status,
                errors: [(response.status.toString() + " " + response.statusText).trim()]
            };
        } catch (error) {
            console.error("Logout failed:", error);
            return {
                statusCode: (error as AxiosError)?.status,
                errors: [(error as AxiosError).code ?? "Unknown error"]
            };
        }
    }
}