<script setup lang="ts">
const props = defineProps<{
  form?: typeof form.fields
  id?: string
}>()
const emits = defineEmits(['success'])
const $q = useQuasar()
const dialog = defineModel<boolean>('dialog')
const form = useForm({
  lastName: '',
  firstName: '',
  middleName: undefined,
  address: '',
  contactNumber: '',
  email: undefined,
})
function submit() {
  form.submit(async (params) => {
    let response = {}
    if (props.id) {
      const { data } = await api.put(`/recipients/${props.id}`, params)
      response = data
      $q.notify({
        message: 'Recipient updated',
        color: 'positive',
      })
    }
    else {
      const { data } = await api.post('/recipients', params)
      response = data
      $q.notify({
        message: 'Recipient created',
        color: 'positive',
      })
    }
    dialog.value = false
    emits('success', response)
  })
}
watch(() => props.form, (data) => {
  if (!data)
    return
  form.fields = data
})
watch(dialog, () => {
  if (!dialog.value)
    form.reset()
})
</script>

<template>
  <q-dialog v-model="dialog">
    <q-card class="w-xl">
      <q-card-section>
        <p class="text-h6">
          Recipient Form
        </p>
        <q-form @submit="submit">
          <q-input
            v-model="form.fields.lastName" label="Last Name"
            :error="form.hasError('lastName')"
            :error-message="form.getError('lastName')"
          />
          <q-input
            v-model="form.fields.firstName" label="First Name"
            :error="form.hasError('firstName')"
            :error-message="form.getError('firstName')"
          />
          <q-input
            v-model="form.fields.middleName" label="Middle Name"
            :error="form.hasError('middleName')"
            :error-message="form.getError('middleName')"
          />
          <q-input
            v-model="form.fields.contactNumber" label="Contact Number"
            :error="form.hasError('contactNumber')"
            :error-message="form.getError('contactNumber')"
          />
          <q-input
            v-model="form.fields.address" label="Address"
            :error="form.hasError('address')"
            :error-message="form.getError('address')"
            type="textarea"
          />
          <q-input
            v-model="form.fields.email" label="Email"
            :error="form.hasError('email')"
            :error-message="form.getError('email')"
          />
          <div class="flex gap-sm">
            <q-btn label="Close" flat color="negative" @click="dialog = false" />
            <q-btn label="Save" color="primary" icon-right="task_alt" type="submit" />
          </div>
        </q-form>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>
