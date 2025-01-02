<template>
    <div class="profile-container">
        <h2 class="text-h4 mb-6">Profil Bilgilerim</h2>
        <div v-if="loading" class="d-flex justify-center">
            <v-progress-circular size="50" color="primary" indeterminate></v-progress-circular>
        </div>
        <div v-else-if="error" class="error-alert">
            <v-alert type="error" variant="tonal">{{ error }}</v-alert>
        </div>
        <v-card v-else elevation="2" class="profile-card">
            <v-card-item>
                <v-avatar class="mb-4" size="120" color="grey-lighten-1">
                    <v-icon size="64" color="white">mdi-account</v-icon>
                </v-avatar>
                <v-card-title class="text-h5 mb-4">{{ userProfile.name }} {{ userProfile.surname }}</v-card-title>
            </v-card-item>
            <v-divider></v-divider>
            <v-card-text>
                <v-container>
                    <v-row>
                        <v-col cols="12" md="6">
                            <div class="profile-item">
                                <v-icon color="primary" class="me-2">mdi-account</v-icon>
                                <div class="profile-detail">
                                    <div class="text-caption">Kullanıcı Adı</div>
                                    <div class="text-body-1">{{ userProfile.userName }}</div>
                                </div>
                            </div>
                            <div class="profile-item">
                                <v-icon color="primary" class="me-2">mdi-email</v-icon>
                                <div class="profile-detail">
                                    <div class="text-caption">Email</div>
                                    <div class="text-body-1">{{ userProfile.email }}</div>
                                </div>
                            </div>
                            <div class="profile-item">
                                <v-icon color="primary" class="me-2">mdi-map-marker</v-icon>
                                <div class="profile-detail">
                                    <div class="text-caption">Şehir</div>
                                    <div class="text-body-1">{{ userProfile.city || '-' }}</div>
                                </div>
                            </div>
                        </v-col>
                        <v-col cols="12" md="6">
                            <div class="profile-item">
                                <v-icon color="primary" class="me-2">mdi-phone</v-icon>
                                <div class="profile-detail">
                                    <div class="text-caption">Telefon</div>
                                    <div class="text-body-1">{{ userProfile.phoneNumber || '-' }}</div>
                                </div>
                            </div>
                            <div class="profile-item">
                                <v-icon color="primary" class="me-2">mdi-calendar</v-icon>
                                <div class="profile-detail">
                                    <div class="text-caption">Doğum Tarihi</div>
                                    <div class="text-body-1">{{ formatDate(userProfile.birthDate) }}</div>
                                </div>
                            </div>
                            <div class="profile-item">
                                <v-icon color="primary" class="me-2">mdi-gender-male-female</v-icon>
                                <div class="profile-detail">
                                    <div class="text-caption">Cinsiyet</div>
                                    <div class="text-body-1">{{ formatGender(userProfile.gender) }}</div>
                                </div>
                            </div>
                        </v-col>
                    </v-row>
                </v-container>
            </v-card-text>
        </v-card>
    </div>
</template>

<style scoped>
.profile-container {
    max-width: 1000px;
    margin: 0 auto;
    padding: 20px;
}

.profile-card {
    border-radius: 16px;
    text-align: center;
}

.profile-item {
    display: flex;
    align-items: center;
    padding: 16px;
    border-radius: 8px;
    background-color: #f5f5f5;
    margin-bottom: 16px;
    transition: all 0.3s ease;
}

.profile-item:hover {
    background-color: #e3f2fd;
    transform: translateY(-2px);
}

.profile-detail {
    flex: 1;
    text-align: left;
}

.error-alert {
    max-width: 500px;
    margin: 0 auto;
}
</style>
<script setup>
import { ref, onMounted } from 'vue'
import { useUserStore } from '@/stores/user'
import { userService } from '@/services/userService'

const userStore = useUserStore()
const loading = ref(true)
const error = ref(null)
const userProfile = ref(null)

const formatDate = (date) => {
    if (!date || date === '0001-01-01T00:00:00') return '-'
    return new Date(date).toLocaleDateString('tr-TR')
}

const formatGender = (gender) => {
    switch (gender) {
        case 0: return 'Belirtilmemiş'
        case 1: return 'Erkek'
        case 2: return 'Kadın'
        default: return '-'
    }
}

const fetchUserProfile = async () => {
    try {
        const userId = userStore.userId
        // ID'yi array içinde gönder
        const response = await userService.getUsersByIds([userId])
        if (response.data && response.data.length > 0) {
            userProfile.value = response.data[0]
        } else {
            error.value = 'Kullanıcı bilgileri bulunamadı'
        }
    } catch (err) {
        error.value = 'Profil bilgileri yüklenemedi'
        console.error('Profile fetch error:', err)
    } finally {
        loading.value = false
    }
}

onMounted(() => {
    fetchUserProfile()
})
</script>

