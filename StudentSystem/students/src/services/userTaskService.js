import axios from 'axios'
import { jwtDecode } from "jwt-decode"

const TASK_API_URL = 'https://localhost:7080/api'

const taskApi = axios.create({
    baseURL: TASK_API_URL,
    headers: {
        'Content-Type': 'application/json',
        'accept': '*/*'
    }
})

taskApi.interceptors.request.use(config => {
    const token = localStorage.getItem('accessToken')
    if (token) {
        config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

export const userTaskService = {
    async getTasks() {
        try {
            const token = localStorage.getItem('accessToken')
            const decodedToken = jwtDecode(token)
            const studentId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']
            
            const response = await taskApi.get(`/UserTask?studentId=${studentId}`)
            return response.data
        } catch (error) {
            throw error
        }
    },

    async toggleTaskComplete(taskId) {
        try {
            const response = await taskApi.patch(`/UserTask/${taskId}/complete`)
            return response.data
        } catch (error) {
            throw error
        }
    },

    async toggleTaskWorking(taskId) {
        try {
            const response = await taskApi.patch(`/UserTask/${taskId}/working`)
            return response.data
        } catch (error) {
            throw error
        }
    }
}