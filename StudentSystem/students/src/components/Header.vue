<template>
    <header>
        <div class="container headerContainer">
            <div class="brand">
                <router-link to="/">
                    <v-icon icon="mdi-star-check-outline" class="header-icon"/>
                    <span>&nbsp; Coachly</span>
                </router-link>
            </div>
        </div>
    </header>
</template>

<script setup>
import { useUserStore } from '@/stores/user'
import { storeToRefs } from 'pinia'

const userStore = useUserStore()
const { isAuthenticated } = storeToRefs(userStore)

const handleLogout = () => {
    userStore.logout()
}
</script>