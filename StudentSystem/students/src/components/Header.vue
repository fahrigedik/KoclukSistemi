<template>
    <header>
        <div class="container headerContainer">
            <div class="brand">
                <router-link to="/">
                    <v-icon icon="mdi-star-check-outline" class="header-icon"/>
                    <span>&nbsp; Coachly</span>
                </router-link>
            </div>
            <div class="routerDiv">
                <ul class="userAction">
                    <li>
                        <router-link to="/" class="nav-link1">Anasayfa &nbsp;</router-link>
                        <a v-if="userStore.isAuthenticated" 
                           @click="handleLogout" 
                           class="nav-link2 cursor-pointer">
                            &nbsp;Çıkış Yap
                        </a>
                    </li>
               </ul>
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