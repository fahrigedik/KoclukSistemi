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
            <p class="header__weather">{{ weatherData }}</p>
        </div>
    </header>
</template>

<script setup>
import { useUserStore } from '@/stores/user'
import { storeToRefs } from 'pinia'
import { onMounted, ref } from 'vue'
import { useMotivationStore } from '@/stores/motivation'

const motivationStore = useMotivationStore()
const weatherData = ref(null);

onMounted(async () => {
  motivationStore.fetchQuote()
  // Her 5 dakikada bir yeni söz al
  setInterval(() => {
    motivationStore.fetchQuote()
  }, 5 * 60 * 1000)

  // Hava durumu verilerini çek
  try {
    const apiKey = 'eafdfd60a1e7a86e0a934396fe938933';
    const apiUrl = `http://api.weatherapi.com/v1/current.json?key=e57f4c7e663f4101aad130254250601&q=Manisa&aqi=no`;
    const response = await fetch(apiUrl);
    const data = await response.json();
    console.log(data);

    // Hava durumu verilerini işle
    weatherData.value = `${data.current.temp_c}°C`;
  } catch (error) {
    console.error('Hava durumu verileri alınamadı:', error);
    weatherData.value = 'Hava durumu bilgisi mevcut değil';
  }
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


