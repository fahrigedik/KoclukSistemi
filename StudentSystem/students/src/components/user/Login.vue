<template>
  <div class="loginContainer">

    <div v-if="userStore.loading">
      <v-progress-circular indeterminate="primary"/>
    </div>

    <Form :validation-schema="formSchema" @submit="onSubmit" v-if="!userStore.loading">
      <h1 class="text-center">Öğrenci Girişi</h1>
      <div class="form-group">
        <Field name="email" v-slot="{field,errors,errorMessage}">
          <input type="email" class="from-control" placeholder="Email" 
          v-bind="field" :class="{'is-invalid': errors.length !== 0}" />
          <div class="input-alert" v-if="errors.length !== 0">
            {{ errorMessage }}
          </div>
         </Field>
      </div>

      <div class="form-group">
        <Field name="password" v-slot="{field,errors,errorMessage}">
          <input type="password" class="from-control" placeholder="Şifre"
            v-bind="field" :class="{'is-invalid': errors.length !== 0}" />
          <div class="input-alert" v-if="errors.length !== 0">
            {{ errorMessage }}
          </div>
          </Field>
      </div>
      <button class="btn btn-block mb-3 ">Giriş Yap</button>
    </Form>

  </div>
</template>


<script setup>
import {Field, Form} from 'vee-validate';
import * as yup from 'yup';
import { useUserStore } from '@/stores/user.js';
const userStore = useUserStore();

const formSchema = yup.object({
  email: yup.string().required('Email girmek zorunludur!').email('Geçerli bir email giriniz'),
  password: yup.string().required('Şifre girmek zorunludur!')
})



function onSubmit(values,{resetForm}){
  userStore.login(values);
  resetForm();
 
}
// const isLoggedScreen=ref(true)


// function onSubmit(values,{resetForm}){
//   if(isLoggedScreen.value){
//     userStore.register(values)
//   }
//   else{
//     userStore.register(values)
//   }
 
//}
</script>