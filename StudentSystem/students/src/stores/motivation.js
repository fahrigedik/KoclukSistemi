import { defineStore } from 'pinia'
import { ref } from 'vue'
import { motivationService } from '@/services/motivationService'

export const useMotivationStore = defineStore('motivation', () => {
    const quote = ref('')
    const loading = ref(false)
    const error = ref(null)

    const fetchQuote = async () => {
        loading.value = true
        try {
            const response = await motivationService.getQuote()
            quote.value = response.Quote
        } catch (err) {
            error.value = 'Motivasyon sözü alınamadı'
            console.error(err)
        } finally {
            loading.value = false
        }
    }

    return { quote, loading, error, fetchQuote }
})