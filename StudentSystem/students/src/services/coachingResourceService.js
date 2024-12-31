import axios from 'axios'
import { jwtDecode } from "jwt-decode"

const COACHING_API_URL = 'https://localhost:7080/api'

const coachingApi = axios.create({
    baseURL: COACHING_API_URL,
    headers: {
        'Content-Type': 'application/json',
        'accept': '*/*'
    }
})

coachingApi.interceptors.request.use(config => {
    const token = localStorage.getItem('accessToken')
    if (token) {
        config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

export const coachingResourceService = {
    async getResources() {
        try {
            const token = localStorage.getItem('accessToken')
            const decodedToken = jwtDecode(token)
            const studentId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']
            
            console.log('Requesting with studentId:', studentId)
            
            const response = await coachingApi.get(`/CoachingResource?studentId=${studentId}`)
            console.log('API Response:', response)
            return response.data
        } catch (error) {
            console.error('API Error:', error)
            throw error
        }
    }
}