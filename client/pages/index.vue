<script setup lang="ts">
definePageMeta({
  layout: 'empty',
})
const router = useRouter()
const showPassword = ref(false)
const form = useForm({
  email: '',
  password: '',
  remember: false,
})
async function onSubmit() {
  await form.submit(async () => {
    await api.post('/login', form.fields)
    router.push('/dashboard')
  })
}
</script>

<template>
  <div class="mx-auto mt-5xl max-w-120 w-full">
    <QCard>
      <QCardSection>
        <QForm @submit.prevent="onSubmit">
          <p class="text-xl text-primary font-bold">
            Kinito Delivery System
          </p>
          <p class="mb-xl flex items-center text-xl text-gray-7 font-bold">
            <QIcon name="login" left size="md" />
            <span>
              Login to your account
            </span>
          </p>
          <QBanner
            v-if="form.hasError('generalErrors')"
            class="text-negative mb-sm border-1 border-red rounded bg-red-100"
          >
            {{ form.getError("generalErrors") }}
          </QBanner>
          <QInput
            v-model="form.fields.email"
            label="Email"
            :error-message="form.getError('email')"
            :error="form.hasError('email')"
            placeholder="Type your email"
            type="email"
          >
            <template #prepend>
              <QIcon name="email" />
            </template>
          </QInput>
          <QInput
            v-model="form.fields.password"
            label="Password"
            :error-message="form.getError('password')"
            :error="form.hasError('password')"
            placeholder="Type your password"
            :type="showPassword ? 'text' : 'password'"
          >
            <template #prepend>
              <QIcon name="lock" />
            </template>
            <template #append>
              <QIcon
                :name="showPassword ? 'visibility' : 'visibility_off'"
                class="cursor-pointer"
                @click="showPassword = !showPassword"
              />
            </template>
          </QInput>
          <div class="flex items-center justify-between">
            <QCheckbox v-model="form.fields.remember" label="Remember me" />
            <NuxtLink to="/forgot-password" class="text-primary font-bold">
              Forgot password?
            </NuxtLink>
          </div>
          <div class="mt-xl">
            <QBtn
              color="primary"
              type="submit"
              :loading="form.loading"
              icon-right="task_alt"
              label="Login"
            />
          </div>
        </QForm>
      </QCardSection>
    </QCard>
  </div>
</template>
