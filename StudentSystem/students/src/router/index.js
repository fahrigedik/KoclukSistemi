import { createRouter, createWebHistory } from 'vue-router'
import Home from '@/components/Home.vue'
import Login from '@/components/user/Login.vue'
import Sidebar from '@/components/user/dashboard/Sidebar.vue'
import Dashboard from '@/components/user/dashboard/Dashboard.vue'
import Profile from '@/components/user/dashboard/student/Profile.vue'
import { jwtDecode } from "jwt-decode"

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
      beforeEnter: (to, from, next) => {
        const token = localStorage.getItem('accessToken')
        if (token) {
          next('/user/dashboard')
        } else {
          next()
        }
      }
    },
    {
      path: '/user/dashboard',
      name: 'sidebar',
      component: Sidebar,
      meta: { requiresAuth: true, roles: ['student'] },
      children:[
        {
          path:'',
          component: Dashboard,
          name:'dashboard',
          meta: { requiresAuth: true, roles: ['student'] }
        },
        {
          path:'profile',
          component: Profile,
          name:'profile',
          meta: { requiresAuth: true, roles: ['student'] }
        },
        {
          path: 'resources',
          name: 'resources',
          component: () => import('@/components/user/dashboard/student/ResourceTable.vue'),
          meta: { requiresAuth: true, roles: ['student'] }
        },
        {
          path:'sessions',
          component: () => import('@/components/user/dashboard/student/SessionTable.vue'),
          name:'sessions',
          meta: { requiresAuth: true, roles: ['student'] }
        },
        {
          path: 'goals',
          component: () => import('@/components/user/dashboard/student/GoalTable.vue'),
          name: 'goals',
          meta: { requiresAuth: true, roles: ['student'] }
        },
        {
          path: 'tasks',
          component: () => import('@/components/user/dashboard/student/TaskTable.vue'),
          name: 'tasks',
          meta: { requiresAuth: true, roles: ['student'] }
        },
        {
          path: 'pomodoro',
          name: 'pomodoro',
          component: () => import('@/components/user/dashboard/Pomodoro.vue'),
          meta: { requiresAuth: true, roles: ['student'] }
        },
        {
          path: 'profile',
          name: 'profile',
          component: () => import('@/components/user/dashboard/student/Profile.vue'),
          meta: { requiresAuth: true, roles: ['student'] }
        }
      ]
    },
  ]
})

router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('accessToken')
  
  if (to.meta.requiresAuth && !token) {
    next('/login')
    return
  }

  if (to.meta.roles && token) {
    const decodedToken = jwtDecode(token)
    const userRole = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    
    if (!to.meta.roles.includes(userRole)) {
      next('/')
      return
    }
  }
  
  next()
})

export default router