import React, { useState } from 'react';
import axios from 'axios';

const RegisterPage = () => {
    const [email, setEmail] = useState('');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleRegister = async (e) => {
    e.preventDefault();
    try {
        const response = await axios.post('/register', { email, username, password });
        // Handle successful registration
        console.log(response.data);
    } catch (error) {
        // Handle registration error
        console.error(error);
    }
    };

    return (
    <div>
        <h2>Register</h2>
        <form onSubmit={handleRegister}>
        <input
            type="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
        />
        <input
            type="text"
            placeholder="Username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
        />
        <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
        />
        <button type="submit">Register</button>
        </form>
    </div>
    );
};

export default RegisterPage;