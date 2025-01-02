import axios from 'axios'

import { jwtDecode } from 'jwt-decode'

const USER_API_URL = 'https://localhost:7076/api'

const userApi = axios.create({
    baseURL: USER_API_URL,
    headers: {
        'Content-Type': 'application/json',
        'accept': '*/*'
    }
})

userApi.interceptors.request.use(config => {
    const token = localStorage.getItem('accessToken')
    if (token) {
        config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

export const userService = {
    async getUsersByIds() {
        try {
            const token = localStorage.getItem('accessToken')
            if (!token) {
                throw new Error('Token bulunamadı')
            }

            const decodedToken = jwtDecode(token)
            const userId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']

            if (!userId) {
                throw new Error('Kullanıcı ID bulunamadı')
            }

            const response = await userApi.post('/User/GetUsersByIds', [userId], {
                headers: {
                    'Content-Type': 'application/json'
                }
            })

            if (!response.data) {
                throw new Error('Kullanıcı bilgisi alınamadı')
            }

            return response.data
        } catch (error) {
            console.error('GetUsersByIds error:', error)
            throw error
        }
    }
}