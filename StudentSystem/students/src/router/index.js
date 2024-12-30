import { createRouter, createWebHistory } from 'vue-router'
import Home from '@/components/Home.vue'
import Login from '@/components/user/Login.vue'
import MainPage from '@/components/user/dashboard/MainPage.vue'
import Dashboard from '@/components/user/dashboard/Dashboard.vue'
import Profile from '@/components/user/dashboard/Profile.vue'
import Taskes from '@/components/user/dashboard/admin/Taskes.vue'
import AddTask from '@/components/user/dashboard/admin/AddTask.vue'
import EditTask from '@/components/user/dashboard/admin/EditTask.vue'
//import {isAuth,isLoggedIn} from '@/composables/auth'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
    },
    {
      path: '/login',
      name: 'login',
      component: Login,
    //beforeEnter:isLoggedIn,
    },
    {
      path: '/user/dashboard',
      name: 'mainpage',
      //beforeEnter:isAuth,
      component: MainPage,children:[
        {path:'',component:Dashboard,name:'dashboard'},
        {path:'profile',component:Profile,name:'profile'},
        {path:'taskes',component:Taskes,name:'taskes'},
        {path:'taskes/add',component:AddTask,name:'taskes_add'},
        {path:'taskes/edit/:id',component:EditTask,name:'taskes_edit'}
      ]
    },

  ],
})

export default router
