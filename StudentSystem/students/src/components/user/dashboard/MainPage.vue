<template>
    <div class="container-fluid">
        <div class="row">
            <nav class="col-md-2 bg-light d-block collapse sidebar">
                <div>
                    <ul class="nav">
                        <li class="nav-item">
                            <router-link class="nav-link" :to="{name:'dashboard'}">
                                <v-icon icon="mdi-view-dashboard"/>
                                <span>Sayfam</span>
                            </router-link>
                        </li>
                        <li class="nav-item">
                            <router-link class="nav-link" :to="{name:'profile'}">
                                <v-icon icon="mdi-account"/>
                                <span>Profilim</span>
                            </router-link>
                        </li>
                        <li class="nav-item">
                            <router-link class="nav-link" :to="{name:'taskes'}">
                                <v-icon icon="mdi-note-outline"/>
                                <span>Görevlerim</span>
                            </router-link>
                        </li>
                        <li class="nav-item">
                            <router-link class="nav-link" :to="{name:'taskes_add'}">
                                <v-icon icon="mdi-note-edit-outline"/>
                                <span>Görevi Ekle</span>
                            </router-link>
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
