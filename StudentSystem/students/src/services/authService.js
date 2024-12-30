import axios from 'axios'

const API_URL = 'https://localhost:7076/api'

export const authService = {
    async login(credentials) {
        try {
            const response = await axios.post(`${API_URL}/Auth/CreateToken`, credentials)
            const tokenData = response.data.data
            if (tokenData) {
                localStorage.setItem('accessToken', tokenData.accessToken)
                localStorage.setItem('refreshToken', tokenData.refreshToken)
                localStorage.setItem('tokenExpiration', tokenData.accessTokenExpiration)
            }
            return response.data
        } catch (error) {
            throw error
        }
    }
}