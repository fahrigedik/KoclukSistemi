<template>
    <div>
        <h1>Haftanın Programını Ekle</h1>
        <hr/>
        <Form :validation-schema="formSchema" @submit="onSubmit">
      <div class="mb-4">
        <Field name="lesson" v-slot="{field,errors,errorMessage}">
          <input type="email" class="from-control" placeholder="Ders Adı Giriniz" 
          v-bind="field" :class="{'is-invalid': errors.length !== 0}" />
          <div class="input-alert" v-if="errors.length !== 0">
            {{ errorMessage }}
          </div>
         </Field>
      </div>

      <div class="mb-4">
        <Field name="subject" v-slot="{field,errors,errorMessage}">
          <input type="text" class="from-control" placeholder="Konu Adı Giriniz"
            v-bind="field" :class="{'is-invalid': errors.length !== 0}" />
          <div class="input-alert" v-if="errors.length !== 0">
            {{ errorMessage }}
          </div>
          </Field>
      </div>
      <div class="mb-4">
        <Field name="summery" v-slot="{field,errors,errorMessage}">
          <input type="text" class="from-control" placeholder="Çalışmanızı Özetler misin?"
            v-bind="field" :class="{'is-invalid': errors.length !== 0}" />
          <div class="input-alert" v-if="errors.length !== 0">
            {{ errorMessage }}
          </div>
          </Field>
      </div>
      <div class="mb-4">
        <Field name="analysis" v-slot="{field,errors,errorMessage}">
          <select v-bind="field" class="form-select" :class="
            {'is-invalid': errors.length !== 0}">
            <option value="Öğrenme Oranını Puanla">Haftanın Verimliliğini Puanla</option> 
            <option v-for="analysis in analysisArray" :value="analysis"
            :key="analysis" >{{ analysis }}</option>
        </select>
          <div class="input-alert" v-if="errors.length !== 0">
            {{ errorMessage }}
          </div>
          </Field>
      </div>
      <div class="mb-4">
        <Field name="img" v-slot="{field,errors,errorMessage}">
          <input type="text" class="from-control" placeholder="Haftalık Çalışma Programını Ekle"
            v-bind="field" :class="{'is-invalid': errors.length !== 0}" />
          <div class="input-alert" v-if="errors.length !== 0">
            {{ errorMessage }}
          </div>
          </Field>
      </div>
      <v-btn type="submit" variant="outlined">Haftanı Ekle</v-btn>
    </Form>

    </div>
</template>

<script setup>
import {Field, Form} from 'vee-validate';
import * as yup from 'yup';
const analysisArray=[0,1,2,3,4,5]

function onSubmit(values, {resetForm}){
    console.log(values)
}
</script>