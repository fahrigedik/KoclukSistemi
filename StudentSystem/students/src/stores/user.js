import { defineStore } from 'pinia'
import { authService } from '@/services/authService'
import router from '@/router'
import { jwtDecode } from "jwt-decode"

export const useUserStore = defineStore('user', {
    state: () => ({
        accessToken: localStorage.getItem('accessToken'),
        refreshToken: localStorage.getItem('refreshToken'),
        tokenExpiration: localStorage.getItem('tokenExpiration'),
        userRole: null,
        loading: false,
        error: null,
        isAuthenticated: !!localStorage.getItem('accessToken')
    }),

    actions: {
        async login(credentials) {
            this.loading = true
            this.error = null
            try {
                const response = await authService.login(credentials)
                if (response.data) {
                    const token = response.data.accessToken
                    this.accessToken = token
                    this.refreshToken = response.data.refreshToken
                    this.tokenExpiration = response.data.accessTokenExpiration
                    
                    const decodedToken = jwtDecode(token)
                    this.userRole = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
                    
                    this.isAuthenticated = true
                    
                    if (this.userRole === 'student') {
                        router.push('/user/dashboard')
                    } else {
                        router.push('/')
                    }
                }
                return response
            } catch (error) {
                this.error = error.response?.data?.error || 'Giriş başarısız oldu'
                throw error
            } finally {
                this.loading = false
            }
        },

        logout() {
            this.accessToken = null
            this.refreshToken = null
            this.tokenExpiration = null
            this.userRole = null
            this.error = null
            this.isAuthenticated = false
            localStorage.removeItem('accessToken')
            localStorage.removeItem('refreshToken')
            localStorage.removeItem('tokenExpiration')
            router.push('/login')
        }
    }
})