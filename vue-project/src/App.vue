<template>
  <div class="common-layout">
    <el-container>
      <el-header class="flex justify-center items-center">
        <div class="flex justify-center ">
          <h1 class="text-2xl ">Support Page</h1>
        </div>
      </el-header>
      <el-main>
        <div class="flex justify-center">
          <el-form class="w-9/12" :model="form" label-width="auto" style="max-width: 800px">
            <el-form-item class="my-4" label="Your Eamil">
              <el-input v-model="form.email" />
            </el-form-item>
            <el-form-item class="my-7" label="Message">
              <el-input v-model="form.desc" type="textarea" :rows="5"/>
            </el-form-item>
            <div class="flex justify-center my-20">
              <el-button type="primary" @click="onSubmit">Send Message</el-button>
            </div>
          </el-form>
        </div>
      </el-main>
    </el-container>
  </div>
</template>

<script lang="ts" setup>
import { reactive } from 'vue'

// do not use same name with ref
const form = reactive({
  email: '',
  region: '',
  date1: '',
  date2: '',
  delivery: false,
  type: [],
  resource: '',
  desc: ''
})

const onSubmit = () => {
  console.log('submit!')
}
</script>
