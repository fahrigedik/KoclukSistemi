import axios from 'axios'
import { jwtDecode } from "jwt-decode"

const GOAL_API_URL = 'https://localhost:7080/api'

const goalApi = axios.create({
    baseURL: GOAL_API_URL,
    headers: {
        'Content-Type': 'application/json',
        'accept': '*/*'
    }
})

goalApi.interceptors.request.use(config => {
    const token = localStorage.getItem('accessToken')
    if (token) {
        config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

export const goalService = {
    async getGoals() {
        try {
            const token = localStorage.getItem('accessToken')
            const decodedToken = jwtDecode(token)
            const studentId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']
            
            console.log('Requesting goals for studentId:', studentId)
            
            const response = await goalApi.get(`/Goal?studentId=${studentId}`)
            console.log('API Response:', response)
            return response.data
        } catch (error) {
            console.error('API Error:', error)
            throw error
        }
    },
    async toggleGoalComplete(goalId) {
        try {
            const response = await goalApi.patch(`/Goal/${goalId}/complete`)
            console.log('Toggle complete response:', response)
            return response.data
        } catch (error) {
            console.error('Toggle complete error:', error)
            throw error
        }
    },
    async toggleGoalWorking(goalId) {
        try {
            const response = await goalApi.patch(`/Goal/${goalId}/working`)
            console.log('Toggle working response:', response)
            return response.data
        } catch (error) {
            console.error('Toggle working error:', error)
            throw error
        }
    }
    
}