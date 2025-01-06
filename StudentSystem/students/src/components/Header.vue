<template>
    <header>
        <div class="container headerContainer">
            <div class="brand">
                <router-link to="/">
                    <v-icon icon="mdi-star-check-outline" class="header-icon"/>
                    <span>&nbsp; Coachly</span>
                </router-link>
            </div>
            <div class="quote-container" v-if="motivationStore.quote">
                    <v-icon icon="mdi-format-quote-open" class="quote-icon"/>
                    <span class="quote-text">{{ motivationStore.quote }}</span>
                    <v-icon icon="mdi-format-quote-close" class="quote-icon"/>
            </div>
        </div>
    </header>
</template>

<script setup>
import { useUserStore } from '@/stores/user'
import { storeToRefs } from 'pinia'
import { onMounted } from 'vue'
import { useMotivationStore } from '@/stores/motivation'

const motivationStore = useMotivationStore()

onMounted(() => {
    motivationStore.fetchQuote()
    // Her 5 dakikada bir yeni söz al
    setInterval(() => {
        motivationStore.fetchQuote()
    }, 5 * 60 * 1000)
})

const userStore = useUserStore()
const { isAuthenticated } = storeToRefs(userStore)

const handleLogout = () => {
    userStore.logout()
}
</script>

<style scoped>
.quote-container {
    display: flex;
    align-items: center;
    gap: 8px;
    color: white;
    font-style: italic;
    max-width: 600px;
    margin: 0 auto;
}

.quote-icon {
    color: rgba(255, 255, 255, 0.7);
}

.quote-text {
    font-size: 1rem;
}
</style>


