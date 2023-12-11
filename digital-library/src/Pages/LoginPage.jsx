import React, { useState } from 'react';
import { AxiosInstance } from '../AxiosInstance';
import { LoginUser, isLogged } from '../components/AuthManager';
import { redirect, useNavigate } from 'react-router-dom';

export const LoginLoader = async () => {
    return null;
};

export const LoginPage = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    console.log(isLogged);

    if ( isLogged ) {
        window.location.replace("/");
    }

    const handleLogin = async (e) => {
        e.preventDefault();
        LoginUser(username, password);
        window.location.reload();
    };

    return (
    <div>
        <h2>Login</h2>
        <form onSubmit={handleLogin}>
            <input
                type="text"
                placeholder="username"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
            />
            <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />
            <button type="submit">Login</button>
        </form>
    </div>
    );
};