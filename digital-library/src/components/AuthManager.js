import { useState } from "react";
import { AxiosInstance } from "../AxiosInstance";
import { redirect } from "react-router-dom";

export const LoginUser = async (username, password) => {
    try {
        const response = await AxiosInstance.post('/authentication/login', { username, password });
        // Handle successful login
        localStorage.setItem('token', response.data.token);
        AxiosInstance.defaults.headers.common['Authorization'] = "Bearer " + localStorage.getItem("token");
        console.log(response.data.token);
    } catch (error) {
        // Handle login error
        console.error(error);
    }
}

export const RegisterUser = () => {

}

export const LogoutUser = () => {
    localStorage.removeItem('token');
    window.location.reload(true);
}


export let isLogged =  localStorage.getItem('token') != null;

// export const getLogin = () => {
//     const [isLogged, setLogged] = useState(localStorage.getItem('token') != null);
// }

class AuthService{
    LoginUser = async (username, password) => {
        try {
            const response = await AxiosInstance.post('/authentication/login', { username, password });
            // Handle successful login
            localStorage.setItem('token', response.data.token);
            AxiosInstance.defaults.headers.common['Authorization'] = "Bearer " + localStorage.getItem("token")
            console.log(response.data);
        } catch (error) {
            // Handle login error
            console.error(error);
        }
    }

    RegisterUser = () => {

    }

    Logout = () => {
        AxiosInstance.defaults.headers.common['Authorization'] = null
    }

    //[isLogged, setLogged] = useState(localStorage.getItem('token') != null);
}