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
                <v-card-title class="text-h5 mb-4 d-flex align-center">
                    {{ userProfile.name }} {{ userProfile.surname }}
                    <v-btn icon="mdi-pencil" size="small" variant="text" @click="toggleEdit" class="ms-2">
                    </v-btn>
                </v-card-title>
            </v-card-item>

            <v-divider></v-divider>

            <v-card-text>
                <v-form v-model="isValid" @submit.prevent="handleSubmit">
                    <v-container>
                        <v-row>
                            <v-col cols="12" md="6">
                                <v-text-field
                                    v-model="editedProfile.userName"
                                    label="Kullanıcı Adı"
                                    :readonly="!isEditing"
                                    variant="outlined"
                                    density="comfortable"
                                ></v-text-field>
                                <v-text-field
                                    v-model="editedProfile.name"
                                    label="Ad"
                                    :readonly="!isEditing"
                                    variant="outlined"
                                    density="comfortable"
                                ></v-text-field>
                                <v-text-field
                                    v-model="editedProfile.surname"
                                    label="Soyad"
                                    :readonly="!isEditing"
                                    variant="outlined"
                                    density="comfortable"
                                ></v-text-field>
                                <v-text-field
                                    v-model="editedProfile.email"
                                    label="Email"
                                    :readonly="!isEditing"
                                    variant="outlined"
                                    density="comfortable"
                                    type="email"
                                ></v-text-field>
                            </v-col>
                            <v-col cols="12" md="6">
                                <v-text-field
                                    v-model="editedProfile.phoneNumber"
                                    label="Telefon"
                                    :readonly="!isEditing"
                                    variant="outlined"
                                    density="comfortable"
                                ></v-text-field>
                                <v-text-field
                                    v-model="editedProfile.city"
                                    label="Şehir"
                                    :readonly="!isEditing"
                                    variant="outlined"
                                    density="comfortable"
                                ></v-text-field>
                                <v-text-field
                                    v-model="editedProfile.tckn"
                                    label="TCKN"
                                    :readonly="!isEditing"
                                    variant="outlined"
                                    density="comfortable"
                                ></v-text-field>
                                <v-select
                                    v-model="editedProfile.gender"
                                    :items="genderOptions"
                                    label="Cinsiyet"
                                    :readonly="!isEditing"
                                    variant="outlined"
                                    density="comfortable"
                                ></v-select>
                            </v-col>
                        </v-row>

                        <v-row v-if="isEditing">
                            <v-col class="text-center">
                                <v-btn
                                    color="primary"
                                    class="me-4"
                                    type="submit"
                                    :loading="updating"
                                    :disabled="!isValid"
                                >
                                    Güncelle
                                </v-btn>
                                <v-btn @click="cancelEdit">
                                    İptal
                                </v-btn>
                            </v-col>
                        </v-row>
                    </v-container>
                </v-form>
            </v-card-text>
        </v-card>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useUserStore } from '@/stores/user'
import { userService } from '@/services/userService'

const userStore = useUserStore()
const loading = ref(true)
const error = ref(null)
const userProfile = ref(null)
const isEditing = ref(false)
const isValid = ref(true)
const updating = ref(false)
const editedProfile = ref({})

const genderOptions = [
    { title: 'Belirtilmemiş', value: 0 },
    { title: 'Erkek', value: 1 },
    { title: 'Kadın', value: 2 }
]

const toggleEdit = () => {
    if (!isEditing.value) {
        editedProfile.value = { ...userProfile.value }
    }
    isEditing.value = !isEditing.value
}

const cancelEdit = () => {
    isEditing.value = false
    editedProfile.value = { ...userProfile.value }
}

const handleSubmit = async () => {
    if (!isValid.value) return

    updating.value = true
    try {
        await userService.updateUser(editedProfile.value)
        userProfile.value = { ...editedProfile.value }
        isEditing.value = false
    } catch (err) {
        error.value = 'Profil güncellenirken bir hata oluştu'
        console.error('Update error:', err)
    } finally {
        updating.value = false
    }
}

const fetchUserProfile = async () => {
    try {
        const response = await userService.getUsersByIds()
        if (response.data && response.data.length > 0) {
            userProfile.value = response.data[0]
            editedProfile.value = { ...response.data[0] }
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

<style scoped>
.profile-container {
    max-width: 1000px;
    margin: 0 auto;
    padding: 20px;
}

.profile-card {
    border-radius: 16px;
}

.error-alert {
    max-width: 500px;
    margin: 0 auto;
}
</style>