<template>
  <div class="loginContainer">
    <div v-if="userStore.loading">
      <v-progress-circular indeterminate="primary"/>
    </div>

    <Form :validation-schema="formSchema" @submit="onSubmit" v-if="!userStore.loading">
      <h1 class="text-center">Öğrenci Girişi</h1>
      <div class="form-group">
        <Field name="email" v-slot="{field,errors,errorMessage}">
          <input type="email" class="form-control" placeholder="Email" 
          v-bind="field" :class="{'is-invalid': errors.length !== 0}" />
          <div class="input-alert" v-if="errors.length !== 0">
            {{ errorMessage }}
          </div>
        </Field>
      </div>

      <div class="form-group">
        <Field name="password" v-slot="{field,errors,errorMessage}">
          <input type="password" class="form-control" placeholder="Şifre"
            v-bind="field" :class="{'is-invalid': errors.length !== 0}" />
          <div class="input-alert" v-if="errors.length !== 0">
            {{ errorMessage }}
          </div>
        </Field>
      </div>
      
      <div v-if="userStore.error" class="alert alert-danger">
        {{ userStore.error }}
      </div>

      <button class="btn btn-block mb-3" type="submit">Giriş Yap</button>
    </Form>
  </div>
</template>

<script setup>
import { Field, Form } from 'vee-validate'
import * as yup from 'yup'
import { useUserStore } from '@/stores/user'
import { useToast } from 'vue-toast-notification'

const userStore = useUserStore()
const toast = useToast()

const formSchema = yup.object({
  email: yup.string().required('Email girmek zorunludur!').email('Geçerli bir email giriniz'),
  password: yup.string().required('Şifre girmek zorunludur!')
})

async function onSubmit(values, { resetForm }) {
  try {
    await userStore.login(values)
    resetForm()
    toast.success('Giriş başarılı!')
  } catch (error) {
    toast.error(error.response?.data?.error || 'Giriş başarısız')
    console.error('Login error:', error.response?.data?.error || error.message)
  }
}
</script>