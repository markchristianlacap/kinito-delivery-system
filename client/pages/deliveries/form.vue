<script setup lang="ts">
const recipientDialog = ref(false)
const $q = useQuasar()
const form = useForm({
  date: new Date().toISOString().split('T')[0],
  trackingNumber: '',
  contactNumber: '',
  address: '',
  recipientId: '',
  amount: 0,
  packageTypeId: '',
  sizeId: '',
})
const recipients = useRequest(r => api.get('/options/recipients', { params: r }).then(r => r.data))
const sizes = useRequest(() => api.get('/options/size-types').then(r => r.data))
const packageTypes = useRequest(() => api.get('/options/package-types').then(r => r.data))
function onSubmit() {
  form.submit(async (data) => {
    await api.post('/deliveries', data)
    $q.notify({
      type: 'positive',
      message: 'Delivery created successfully',
    })
    navigateTo('/deliveries')
  })
}
function onNewRecipient() {
  recipientDialog.value = true
}
function onNewRecipientSuccess(data: any) {
  form.fields.recipientId = data.id
  recipientDialog.value = false
  recipients.request.id = data.id
  recipients.submit()
}
async function onRecipientSearch(value: string | undefined) {
  recipients.request.search = value
  await recipients.submit()
}
onMounted(() => {
  sizes.submit()
  packageTypes.submit()
})
</script>

<template>
  <QPage>
    <QCard flat bordered class="q-pa-md">
      <QCardSection>
        <p class="text-lg">
          New Delivery
        </p>
        <QForm @submit.prevent="onSubmit">
          <QInput
            v-model="form.fields.date"
            label="Date of Arrival"
            :error-message="form.getError('date')"
            :error="form.hasError('date')"
            type="date"
          >
            <template #prepend>
              <QIcon name="event" />
            </template>
          </QInput>
          <QInput
            v-model="form.fields.trackingNumber"
            label="Tracking Number"
            :error-message="form.getError('trackingNumber')"
            :error="form.hasError('trackingNumber')"
          />
          <q-select
            v-model="form.fields.recipientId"
            label="Recipient"
            :error-message="form.getError('recipientId')"
            map-options
            emit-value
            :error="form.hasError('recipientId')"
            :options="recipients.response || []"
            option-value="id"
            option-label="fullName"
            clearable
            :input-debounce="300"
            use-input
            placeholder="Type to Search Recipient"
            @input-value="onRecipientSearch"
          >
            <template #append>
              <q-btn icon="add" size="sm" color="primary" @click="onNewRecipient" />
            </template>
            <template #no-option>
              <q-item>
                <q-item-section>
                  <q-item-label>
                    No results found <q-btn size="sm" class="ml-sm" label="Create New Instead" color="primary" @click="onNewRecipient" />
                  </q-item-label>
                </q-item-section>
              </q-item>
            </template>
          </q-select>
          <QInput
            v-model="form.fields.contactNumber"
            label="Contact Number"
            :error-message="form.getError('contactNumber')"
            :error="form.hasError('contactNumber')"
          />
          <QInput
            v-model="form.fields.address"
            type="textarea"
            label="Address"
            :error-message="form.getError('address')"
            :error="form.hasError('address')"
          />
          <div class="grid grid-cols-2 gap-sm">
            <q-select
              v-model="form.fields.packageTypeId"
              :options="packageTypes.response || []"
              option-value="id"
              option-label="name"
              label="Package Type"
              emit-value
              map-options
              :error-message="form.getError('packageTypeId')"
              clearable
              :error="form.hasError('packageTypeId')"
            />
            <q-select
              v-model="form.fields.sizeId"
              :options="sizes.response || []"
              option-value="id"
              option-label="name"
              label="Size"
              :error-message="form.getError('sizeId')"
              map-options
              emit-value
              :error="form.hasError('sizeId')"
            />
          </div>
          <QInput
            v-model="form.fields.amount"
            label="Amount"
            :error-message="form.getError('amount')"
            :error="form.hasError('amount')"
            type="number"
          />

          <div class="flex items-center justify-between">
            <QBtn
              color="primary"
              type="submit"
              :loading="form.loading"
              icon-right="task_alt"
              label="Create"
            />
          </div>
        </QForm>
      </QCardSection>
    </QCard>
    <recipients-dialog-form v-model:dialog="recipientDialog" @success="onNewRecipientSuccess" />
  </QPage>
</template>
