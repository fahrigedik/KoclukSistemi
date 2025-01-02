<template>
    <div class="container-fluid">
        <div class="row">
            <nav class="col-md-2 bg-light d-block collapse sidebar">
                <div>
                    <ul style="list-style: none; margin:0; padding: 0;" class="m-0">
                        <li class="nav-item">
                            <router-link class="nav-link" :to="{name:'dashboard'}">
                                <v-icon icon="mdi-view-dashboard" />
                                <span>Sayfam</span>
                            </router-link>
                        </li>
                        <li class="nav-item">
                            <router-link class="nav-link" :to="{name:'pomodoro'}">
                                <v-icon icon="mdi-timer" />
                                <span>Pomodoro</span>
                            </router-link>
                        </li>
                        <li class="nav-item">
                            <router-link class="nav-link" :to="{name:'profile'}">
                                <v-icon icon="mdi-account" />
                                <span>Profilim</span>
                            </router-link>
                        </li>
                        <li class="nav-item mt-auto">
                            <a class="nav-link logout-link" href="#" @click="userStore.logout">
                                <v-icon icon="mdi-logout" color="error"/>
                                <span>Çıkış Yap</span>
                            </a>
                        </li>
                    </ul>
                </div>
            </nav>
            <div class="col-md-10 ms-auto px-4 dashboardTop">
                <router-view></router-view>
            </div>
        </div>
    </div>
</template>

<script setup>
import { useUserStore } from '@/stores/user'

const userStore = useUserStore()
</script>

<style scoped>
.nav-link {
    display: flex;
    align-items: center;
    gap: 0.8rem;
    padding: 0.8rem 1rem;
    color: #2c3e50;
    transition: all 0.3s ease;
    border-radius: 8px;
    margin: 4px 0;
}

.logout-link {
    color: #dc3545;
}

.logout-link:hover {
    background-color: #ffebee;
}

.nav-item {
    margin: 4px 8px;
}
</style>