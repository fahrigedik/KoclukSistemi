<template>
    <div>
        <div v-if="loading">Loading...</div>
        <div v-else-if="error">{{ error }}</div>
        <div v-else>
            <v-data-table
                :headers="headers"
                :items="sessions"
                class="elevation-1"
            >
                <template v-slot:item.sessionDate="{ item }">
                    {{ formatDate(item.sessionDate) }}
                </template>
                <template v-slot:item.sessionTime="{ item }">
                    {{ formatTime(item.sessionTime) }}
                </template>
            </v-data-table>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { coachingSessionService } from '@/services/coachingSessionService'

const sessions = ref([])
const loading = ref(true)
const error = ref(null)

const headers = [
    { title: 'ID', key: 'id', align: 'start' },
    { title: 'Tarih', key: 'sessionDate' },
    { title: 'Saat', key: 'sessionTime' },
    { title: 'Konu', key: 'sessionTopic' },
    { title: 'Notlar', key: 'sessionNotes' },
    { title: 'Konum', key: 'sessionLocation' },
    { title: 'Durum', key: 'sessionStatus' }
]

const formatDate = (date) => {
    if (!date || date === '0001-01-01T00:00:00') return '-'
    return new Date(date).toLocaleDateString('tr-TR')
}

const formatTime = (time) => {
    if (!time) return '-'
    return time.substring(0, 5)
}

const fetchSessions = async () => {
    try {
        const response = await coachingSessionService.getSessions()
        sessions.value = response.data
    } catch (err) {
        error.value = err.message
        console.error('Fetch error:', err)
    } finally {
        loading.value = false
    }
}

onMounted(() => {
    fetchSessions()
})
</script>