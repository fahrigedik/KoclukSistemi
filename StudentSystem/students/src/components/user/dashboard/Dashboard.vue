<template>
    <div>
        <v-container>
            <v-row>
                <v-col cols="12" sm="6" md="3">
                    <v-card
                        color="primary"
                        theme="dark"
                        to="/user/dashboard/goals"
                        class="dashboard-card"
                    >
                        <v-card-title class="text-center">
                            <v-icon size="48" class="mb-2">mdi-target</v-icon>
                            <div>Hedefler</div>
                        </v-card-title>
                        <v-card-text class="text-center text-h4">
                            {{ goals.length }}
                        </v-card-text>
                    </v-card>
                </v-col>

                <v-col cols="12" sm="6" md="3">
                    <v-card
                        color="success"
                        theme="dark"
                        to="/user/dashboard/tasks"
                        class="dashboard-card"
                    >
                        <v-card-title class="text-center">
                            <v-icon size="48" class="mb-2">mdi-checkbox-marked-circle</v-icon>
                            <div>Görevler</div>
                        </v-card-title>
                        <v-card-text class="text-center text-h4">
                            {{ tasks.length }}
                        </v-card-text>
                    </v-card>
                </v-col>

                <v-col cols="12" sm="6" md="3">
                    <v-card
                        color="warning"
                        theme="dark"
                        to="/user/dashboard/sessions"
                        class="dashboard-card"
                    >
                        <v-card-title class="text-center">
                            <v-icon size="48" class="mb-2">mdi-calendar-clock</v-icon>
                            <div>Seanslar</div>
                        </v-card-title>
                        <v-card-text class="text-center text-h4">
                            {{ sessions.length }}
                        </v-card-text>
                    </v-card>
                </v-col>

                <v-col cols="12" sm="6" md="3">
                    <v-card
                        color="info"
                        theme="dark"
                        to="/user/dashboard/resources"
                        class="dashboard-card"
                    >
                        <v-card-title class="text-center">
                            <v-icon size="48" class="mb-2">mdi-book-open-variant</v-icon>
                            <div>Kaynaklar</div>
                        </v-card-title>
                        <v-card-text class="text-center text-h4">
                            {{ resources.length }}
                        </v-card-text>
                    </v-card>
                </v-col>
            </v-row>
        </v-container>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { goalService } from '@/services/goalService'
import { userTaskService } from '@/services/userTaskService'
import { coachingSessionService } from '@/services/coachingSessionService'
import { coachingResourceService } from '@/services/coachingResourceService'

const goals = ref([])
const tasks = ref([])
const sessions = ref([])
const resources = ref([])

const fetchData = async () => {
    try {
        const [goalsRes, tasksRes, sessionsRes, resourcesRes] = await Promise.all([
            goalService.getGoals(),
            userTaskService.getTasks(),
            coachingSessionService.getSessions(),
            coachingResourceService.getResources()
        ])

        goals.value = goalsRes.data
        tasks.value = tasksRes.data
        sessions.value = sessionsRes.data
        resources.value = resourcesRes.data
    } catch (error) {
        console.error('Error fetching dashboard data:', error)
    }
}

onMounted(() => {
    fetchData()
})
</script>

<style scoped>
.dashboard-card {
    transition: transform 0.2s;
    cursor: pointer;
}

.dashboard-card:hover {
    transform: translateY(-5px);
}

.v-card-title {
    flex-direction: column;
}
</style>