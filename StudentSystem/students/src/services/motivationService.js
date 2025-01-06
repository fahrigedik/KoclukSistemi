import axios from 'axios'

const motivationApi = axios.create({
    baseURL: 'http://localhost:3000/api'
})

export const motivationService = {
    async getQuote() {
        try {
            const response = await motivationApi.get('/motivasyon-sozu')
            return response.data
        } catch (error) {
            console.error('Motivation quote error:', error)
            throw error
        }
    }
}