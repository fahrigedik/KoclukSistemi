<template>
    <div>
        <div v-if="loading">Loading...</div>
        <div v-else-if="error" class="error-message">{{ error }}</div>
        <div v-else>
            <v-data-table
                :headers="headers"
                :items="goals"
                class="elevation-1"
                :item-class="getRowClass"
            >
                <template v-slot:item.isCompleted="{ item }">
                    <div class="d-flex justify-center">
                        <v-checkbox
                            v-model="item.isCompleted"
                            @click="toggleComplete(item)"
                            :color="item.isCompleted ? 'success' : ''"
                            :loading="item.updating"
                            density="compact"
                        ></v-checkbox>
                    </div>
                </template>
                <template v-slot:item.isWorking="{ item }">
                    <div class="d-flex justify-center">
                        <v-checkbox
                            v-model="item.isWorking"
                            @click="toggleWorking(item)"
                            :color="item.isWorking ? 'warning' : ''"
                            :loading="item.updatingWork"
                            density="compact"
                        ></v-checkbox>
                    </div>
                </template>
                <template v-slot:item.dueDate="{ item }">
                    {{ formatDate(item.dueDate) }}
                </template>
                <template v-slot:item.creationDate="{ item }">
                    {{ formatDate(item.creationDate) }}
                </template>
                <template v-slot:item.status="{ item }">
                    <v-chip
                        :color="getStatusColor(item.status)"
                        text-color="white"
                        size="small"
                    >
                        {{ item.status }}
                    </v-chip>
                </template>
            </v-data-table>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { goalService } from '@/services/goalService'
import { useToast } from 'vue-toast-notification'

const toast = useToast()
const goals = ref([])
const loading = ref(true)
const error = ref(null)

const headers = [
    { title: 'ID', key: 'id', align: 'start', width: '80px' },
    { title: 'Tamamlandı', key: 'isCompleted', align: 'center', width: '100px' },
    { title: 'Devam Ediyor', key: 'isWorking', align: 'center', width: '120px' },
    { title: 'Hedef', key: 'goalTitle' },
    { title: 'Açıklama', key: 'goalDescription' },
    { title: 'Durum', key: 'status', align: 'center', width: '120px' },
    { title: 'Hedef Türü', key: 'goalTypeName' },
    { title: 'Bitiş Tarihi', key: 'dueDate', align: 'center' },
    { title: 'Oluşturma Tarihi', key: 'creationDate', align: 'center' }
]

const formatDate = (date) => {
    if (!date || date === '0001-01-01T00:00:00') return '-'
    return new Date(date).toLocaleDateString('tr-TR')
}

const getStatusColor = (status) => {
    switch (status) {
        case 'Başlanmadı': return 'grey'
        case 'Çalışılıyor': return 'blue'
        case 'Tamamlandı': return 'green'
        default: return 'grey'
    }
}

const getRowClass = (item) => {
    return {
        'completed-row': item.isCompleted,
        'working-row': item.isWorking && !item.isCompleted,
        'uncompleted-row': !item.isCompleted && !item.isWorking
    }
}

const toggleComplete = async (goal) => {
    const previousState = goal.isCompleted
    try {
        goal.updating = true
        await goalService.toggleGoalComplete(goal.id)
        await fetchGoals()
        toast.success('Hedef durumu güncellendi')
    } catch (err) {
        goal.isCompleted = previousState
        toast.error('Güncelleme başarısız')
        console.error('Update error:', err)
    } finally {
        goal.updating = false
    }
}

const toggleWorking = async (goal) => {
    const previousState = goal.isWorking
    try {
        goal.updatingWork = true
        await goalService.toggleGoalWorking(goal.id)
        await fetchGoals()
        toast.success('Çalışma durumu güncellendi')
    } catch (err) {
        goal.isWorking = previousState
        toast.error('Güncelleme başarısız')
        console.error('Update error:', err)
    } finally {
        goal.updatingWork = false
    }
}

const fetchGoals = async () => {
    try {
        const response = await goalService.getGoals()
        goals.value = response.data
    } catch (err) {
        error.value = err.message
        console.error('Fetch error:', err)
    } finally {
        loading.value = false
    }
}

onMounted(() => {
    fetchGoals()
})
</script>

<style scoped>
.error-message {
    color: red;
    padding: 10px;
    margin: 10px 0;
}

:deep(.completed-row) {
    background-color: rgba(76, 175, 80, 0.1) !important;
}

:deep(.working-row) {
    background-color: rgba(255, 193, 7, 0.1) !important;
}

:deep(.uncompleted-row) {
    background-color: white;
}

:deep(.v-data-table-header__checkbox) {
    justify-content: center;
}

:deep(.v-data-table) {
    border-radius: 8px;
}

:deep(.v-checkbox-btn) {
    margin: 0;
    padding: 0;
}
</style>