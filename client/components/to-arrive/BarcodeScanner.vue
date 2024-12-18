<script setup lang="ts">
// @ts-expect-error not found type
import { StreamBarcodeReader } from '@teckel/vue-barcode-reader'

const emits = defineEmits(['stop'])
const referenceNumber = ref('')
const $q = useQuasar()
const dialog = defineModel<boolean>('dialog')
function onDecode(result: string) {
  referenceNumber.value = result
  $q.dialog({
    title: `${result} mark as Shipped`,
    persistent: true,
    message: 'Are you sure you want to mark this as shipped?',
    cancel: true,
  }).onOk(async () => {
    await api.put(`/deliveries/to-arrive/${result}`)
    $q.notify({
      message: 'Successfully marked as shipped',
      color: 'positive',
    })
  })
}
function onClose() {
  dialog.value = false
  referenceNumber.value = ''
  emits('stop')
}
</script>

<template>
  <q-dialog v-model="dialog" persistent>
    <q-card class="w-xl">
      <q-card-section>
        <p class="text-h6">
          Scan Barcode
        </p>
        <p v-if="referenceNumber">
          Scanned Reference Number: {{ referenceNumber }}
        </p>
        <StreamBarcodeReader
          torch
          no-front-cameras
          @decode="onDecode"
        />
      </q-card-section>
      <q-card-actions>
        <q-btn label="Stop Scanning" color="primary" @click="onClose" />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>
