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
                    <li v-if="!userStore.auth">
                <router-link to="/" class="nav-link1">Anasayfa &nbsp;</router-link>
                <router-link to="/login" class="nav-link2">&nbsp;Çıkış Yap</router-link>
                    </li>
               </ul>
            </div>
        </div>
    </header>
</template>

<script setup>
import { useUserStore } from '@/stores/user';
const userStore=useUserStore();
</script>