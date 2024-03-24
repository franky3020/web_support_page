<template>
  <div class="common-layout">
    <el-container>
      <el-header class="flex justify-center items-center">
        <div class="flex justify-center">
          <h1 class="text-2xl">Support Page</h1>
        </div>
      </el-header>
      <el-main>
        <div class="flex justify-center">
          <el-form
            v-if="!hasSubmit"
            class="w-9/12"
            :model="form"
            label-width="auto"
            style="max-width: 800px"
          >
            <el-form-item class="my-4" label="Your Eamil">
              <el-input v-model="form.email" />
            </el-form-item>
            <el-form-item class="my-7" label="Message">
              <el-input v-model="form.message" type="textarea" :rows="5" />
            </el-form-item>
            <div class="flex justify-center my-20">
              <el-button
                v-loading.fullscreen.lock="fullscreenLoading"
                type="primary"
                @click="onSubmit"
                >Send Message</el-button
              >
            </div>
          </el-form>
          <el-result
            v-if="hasSubmit"
            icon="success"
            title="Success send massge"
            sub-title="I will reply to your email"
          ></el-result>
        </div>
      </el-main>
    </el-container>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref } from 'vue'
import axios from 'axios'

// do not use same name with ref
const form = reactive({
  email: '',
  message: ''
})

let hasSubmit = ref(false)
let fullscreenLoading = ref(false)

const onSubmit = () => {
  console.log('submit!', form.email, form.message)
  fullscreenLoading.value = true

  axios
    .post('hhttps://frankyya.com/support_page_api/WeatherForecast/supportMessage', {
      Email: form.email,
      Message: form.message
    })
    .then(() => {
      fullscreenLoading.value = false
      hasSubmit.value = true
    })
}
</script>
