import axios from 'axios';

const instance = axios.create({
    baseURL: 'https://localhost:7076/api',
    headers: {
        'Content-Type': 'application/json',
        'accept': '*/*'
    }
});

instance.interceptors.request.use(config => {
    const token = localStorage.getItem('accessToken');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

instance.interceptors.response.use(
    response => response,
    error => {
        if (error.response?.status === 401) {
            // Handle token refresh or logout logic here
            localStorage.removeItem('accessToken');
            localStorage.removeItem('refreshToken');
            localStorage.removeItem('tokenExpiration');
        }
        return Promise.reject(error);
    }
);

export default instance;