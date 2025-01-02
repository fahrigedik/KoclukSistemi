<template>
    <div class="pomodoro-container">
        <div class="timer-display">
            <h2>{{ timerType }}</h2>
            <v-progress-circular
                :rotate="-90"
                :size="200"
                :width="15"
                :model-value="progress"
                :color="timerColor"
            >
                {{ formatTime }}
            </v-progress-circular>
        </div>

        <div class="settings mt-4">
            <v-expansion-panels>
                <v-expansion-panel>
                    <v-expansion-panel-title>Ayarlar</v-expansion-panel-title>
                    <v-expansion-panel-text>
                        <v-row>
                            <v-col cols="12" sm="4">
                                <v-text-field
                                    v-model="customTimes.work"
                                    label="Çalışma Süresi (dk)"
                                    type="number"
                                    min="1"
                                    max="60"
                                    @change="updateTimer"
                                ></v-text-field>
                            </v-col>
                            <v-col cols="12" sm="4">
                                <v-text-field
                                    v-model="customTimes.shortBreak"
                                    label="Kısa Mola (dk)"
                                    type="number"
                                    min="1"
                                    max="30"
                                    @change="updateTimer"
                                ></v-text-field>
                            </v-col>
                            <v-col cols="12" sm="4">
                                <v-text-field
                                    v-model="customTimes.longBreak"
                                    label="Uzun Mola (dk)"
                                    type="number"
                                    min="1"
                                    max="45"
                                    @change="updateTimer"
                                ></v-text-field>
                            </v-col>
                        </v-row>
                    </v-expansion-panel-text>
                </v-expansion-panel>
            </v-expansion-panels>
        </div>

        <div class="mode-selector mt-4">
            <v-btn-toggle v-model="currentMode" mandatory>
                <v-btn value="work" :color="currentMode === 'work' ? 'primary' : ''">
                    Çalışma
                </v-btn>
                <v-btn value="shortBreak" :color="currentMode === 'shortBreak' ? 'success' : ''">
                    Kısa Mola
                </v-btn>
                <v-btn value="longBreak" :color="currentMode === 'longBreak' ? 'info' : ''">
                    Uzun Mola
                </v-btn>
            </v-btn-toggle>
        </div>

        <div class="controls mt-4">
            <v-btn
                :color="isRunning ? 'error' : 'success'"
                @click="toggleTimer"
                class="mx-2"
                :disabled="!isValid"
            >
                {{ isRunning ? 'Duraklat' : 'Başlat' }}
            </v-btn>
            <v-btn color="primary" @click="resetTimer" class="mx-2">
                Sıfırla
            </v-btn>
        </div>

        <div class="stats mt-4">
            <p>Tamamlanan Pomodoro: {{ completedPomodoros }}</p>
        </div>
    </div>
</template>

<script setup>
import { ref, computed, watch, onBeforeUnmount } from 'vue'

const customTimes = ref({
    work: '25',
    shortBreak: '5',
    longBreak: '15'
})

const currentMode = ref('work')
const timeLeft = ref(parseInt(customTimes.value.work) * 60)
const isRunning = ref(false)
const completedPomodoros = ref(0)
let timerInterval = null

const isValid = computed(() => {
    return Object.values(customTimes.value).every(time => {
        const num = parseInt(time)
        return !isNaN(num) && num > 0
    })
})

const modes = computed(() => ({
    work: parseInt(customTimes.value.work) * 60,
    shortBreak: parseInt(customTimes.value.shortBreak) * 60,
    longBreak: parseInt(customTimes.value.longBreak) * 60
}))

const formatTime = computed(() => {
    const minutes = Math.floor(timeLeft.value / 60)
    const seconds = timeLeft.value % 60
    return `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`
})

const progress = computed(() => {
    const total = modes.value[currentMode.value]
    return ((total - timeLeft.value) / total) * 100
})

const timerType = computed(() => {
    switch (currentMode.value) {
        case 'work': return 'Çalışma Zamanı'
        case 'shortBreak': return 'Kısa Mola'
        case 'longBreak': return 'Uzun Mola'
    }
})

const timerColor = computed(() => {
    switch (currentMode.value) {
        case 'work': return 'primary'
        case 'shortBreak': return 'success'
        case 'longBreak': return 'info'
    }
})

watch(currentMode, () => {
    resetTimer()
})

const updateTimer = () => {
    if (isValid.value) {
        resetTimer()
    }
}

const toggleTimer = () => {
    if (!isValid.value) return

    if (isRunning.value) {
        clearInterval(timerInterval)
    } else {
        timerInterval = setInterval(() => {
            if (timeLeft.value > 0) {
                timeLeft.value--
            } else {
                handleTimerComplete()
            }
        }, 1000)
    }
    isRunning.value = !isRunning.value
}

const resetTimer = () => {
    clearInterval(timerInterval)
    isRunning.value = false
    timeLeft.value = modes.value[currentMode.value]
}

const handleTimerComplete = () => {
    clearInterval(timerInterval)
    isRunning.value = false
    playNotificationSound()
    
    if (currentMode.value === 'work') {
        completedPomodoros.value++
        currentMode.value = completedPomodoros.value % 4 === 0 ? 'longBreak' : 'shortBreak'
    } else {
        currentMode.value = 'work'
    }
    timeLeft.value = modes.value[currentMode.value]
}

const playNotificationSound = () => {
    const audio = new Audio('/notification.mp3')
    audio.play().catch(err => console.error('Ses çalma hatası:', err))
}

onBeforeUnmount(() => {
    clearInterval(timerInterval)
})
</script>

<style scoped>
.pomodoro-container {
    max-width: 600px;
    margin: 2rem auto;
    text-align: center;
    padding: 2rem;
}

.timer-display {
    margin-bottom: 2rem;
}

.controls {
    margin: 1rem 0;
}

.stats {
    margin-top: 2rem;
    font-size: 1.2rem;
}
</style>