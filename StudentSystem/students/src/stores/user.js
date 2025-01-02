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
        userId: null,
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
                    this.userId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']
                    
                    this.isAuthenticated = true
                    this.startTokenExpirationCheck()
                    
                    if (this.userRole === 'student') {
                        router.push('/user/dashboard')
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

        async refreshToken() {
            try {
                const response = await authService.refreshToken()
                if (response.data) {
                    this.accessToken = response.data.accessToken
                    this.refreshToken = response.data.refreshToken
                    this.tokenExpiration = response.data.accessTokenExpiration
                    return true
                }
                return false
            } catch (error) {
                console.error('Token refresh failed:', error)
                this.logout()
                return false
            }
        },

        startTokenExpirationCheck() {
            // Check token expiration every minute
            setInterval(() => {
                this.checkTokenExpiration()
            }, 60000)
        },

        checkTokenExpiration() {
            const expiration = this.tokenExpiration
            if (!expiration) {
                this.logout()
                return
            }

            const expirationDate = new Date(expiration)
            const now = new Date()

            if (now >= expirationDate) {
                this.logout()
            }
        },

        logout() {
            this.accessToken = null
            this.refreshToken = null
            this.tokenExpiration = null
            this.userRole = null
            this.userId = null
            this.error = null
            this.isAuthenticated = false
            
            localStorage.removeItem('accessToken')
            localStorage.removeItem('refreshToken')
            localStorage.removeItem('tokenExpiration')
            
            router.push('/login')
        }
    }
})