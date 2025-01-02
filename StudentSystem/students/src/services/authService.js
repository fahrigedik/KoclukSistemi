import axios from 'axios'

const API_URL = 'https://localhost:7076/api'

const authApi = axios.create({
    baseURL: API_URL,
    headers: {
        'Content-Type': 'application/json'
    }
})

export const authService = {
    async login(credentials) {
        try {
            const response = await authApi.post('/Auth/CreateToken', credentials)
            const tokenData = response.data.data
            if (tokenData) {
                this.setTokens(tokenData)
            }
            return response.data
        } catch (error) {
            throw error
        }
    },

    async refreshToken() {
        try {
            const currentRefreshToken = localStorage.getItem('refreshToken')
            const response = await authApi.post('/Auth/CreateTokenByRefreshToken', {
                token: currentRefreshToken
            })
            
            const tokenData = response.data.data
            if (tokenData) {
                this.setTokens(tokenData)
            }
            return response.data
        } catch (error) {
            console.error('Refresh token error:', error)
            throw error
        }
    },

    setTokens(tokenData) {
        localStorage.setItem('accessToken', tokenData.accessToken)
        localStorage.setItem('refreshToken', tokenData.refreshToken)
        localStorage.setItem('tokenExpiration', tokenData.accessTokenExpiration)
    },

    shouldRefreshToken() {
        const expiration = localStorage.getItem('tokenExpiration')
        if (!expiration) return true
        
        const expirationDate = new Date(expiration)
        const now = new Date()
        const fiveMinutes = 5 * 60 * 1000 // 5 minutes in milliseconds

        return expirationDate.getTime() - now.getTime() < fiveMinutes
    }
}

// Add axios interceptor
axios.interceptors.request.use(async config => {
    const token = localStorage.getItem('accessToken')
    
    if (token) {
        if (authService.shouldRefreshToken()) {
            try {
                await authService.refreshToken()
                const newToken = localStorage.getItem('accessToken')
                config.headers.Authorization = `Bearer ${newToken}`
            } catch (error) {
                console.error('Token refresh failed:', error)
                window.location.href = '/login'
                return Promise.reject(error)
            }
        } else {
            config.headers.Authorization = `Bearer ${token}`
        }
    }
    
    return config
})