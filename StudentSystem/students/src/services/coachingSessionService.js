import axios from 'axios'
import { jwtDecode } from "jwt-decode"

const SESSION_API_URL = 'https://localhost:7080/api'

const sessionApi = axios.create({
    baseURL: SESSION_API_URL,
    headers: {
        'Content-Type': 'application/json',
        'accept': '*/*'
    }
})

sessionApi.interceptors.request.use(config => {
    const token = localStorage.getItem('accessToken')
    if (token) {
        config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

export const coachingSessionService = {
    async getSessions() {
        try {
            const token = localStorage.getItem('accessToken')
            const decodedToken = jwtDecode(token)
            const studentId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']
            
            console.log('Requesting sessions for studentId:', studentId)
            
            const response = await sessionApi.get(`/CoachingSession?studentId=${studentId}`)
            console.log('API Response:', response)
            return response.data
        } catch (error) {
            console.error('API Error:', error)
            throw error
        }
    }
}