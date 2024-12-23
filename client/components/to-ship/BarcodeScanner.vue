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
    title: `${result} mark as to Shipped`,
    persistent: true,
    message: 'Are you sure you want to mark this as to shipped?',
    cancel: true,
  }).onOk(async () => {
    await api.put(`/deliveries/to-ship/${result}`)
    $q.notify({
      message: 'Successfully marked as shipped',
      color: 'positive',
    })
  })
}
function onClose() {
  emits('stop')
  dialog.value = false
  referenceNumber.value = ''
}
</script>

<template>
  <q-dialog v-model="dialog" persistent>
    <q-card>
      <div class="p-sm">
        <div class="flex items-center justify-between">
          <p class="text-h6">
            Scan Barcode to mark as Shipped
          </p>
          <q-btn flat color="negative" icon="close" @click="onClose" />
        </div>
        <p v-if="referenceNumber">
          Scanned Reference Number: {{ referenceNumber }}
        </p>
      </div>
      <StreamBarcodeReader
        @decode="onDecode"
      />
    </q-card>
  </q-dialog>
</template>
