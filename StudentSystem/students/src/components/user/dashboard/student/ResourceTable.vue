<template>
    <div>
        <div v-if="loading">Loading...</div>
        <div v-else-if="error">{{ error }}</div>
        <div v-else>
            <v-data-table
                :headers="headers"
                :items="resources"
                class="elevation-1"
            >
                <template v-slot:item.creationDate="{ item }">
                    {{ formatDate(item.creationDate) }}
                </template>
                <template v-slot:item.tags="{ item }">
                    <v-chip v-for="tag in item.tags" :key="tag" class="ma-1">
                        {{ tag }}
                    </v-chip>
                </template>
            </v-data-table>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { coachingResourceService } from '@/services/coachingResourceService'

const resources = ref([])
const loading = ref(true)
const error = ref(null)

const headers = [
    { title: 'ID', key: 'id', align: 'start' },
    { title: 'Başlık', key: 'title' },
    { title: 'Açıklama', key: 'description' },
    { title: 'URL', key: 'url' },
    { title: 'Kaynak Tipi', key: 'resourceTypeName' },
    { title: 'Oluşturma Tarihi', key: 'creationDate' },
    { title: 'Etiketler', key: 'tags' }
]

const formatDate = (date) => {
    if (!date || date === '0001-01-01T00:00:00') return '-'
    return new Date(date).toLocaleDateString('tr-TR')
}

const fetchResources = async () => {
    try {
        const response = await coachingResourceService.getResources()
        resources.value = response.data
        console.log('Fetched resources:', resources.value)
    } catch (err) {
        error.value = err.message
        console.error('Fetch error:', err)
    } finally {
        loading.value = false
    }
}

onMounted(() => {
    console.log('Component mounted, fetching resources...')
    fetchResources()
})
</script>