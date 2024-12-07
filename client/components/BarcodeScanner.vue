<script setup lang="ts">
import { StreamBarcodeReader } from '@teckel/vue-barcode-reader'

const decodedText = ref('')
const dialog = defineModel<boolean>('dialog')
function onDecode(result) {
  decodedText.value = result
}

function onLoaded() {
  console.log('loaded')
}
</script>

<template>
  <q-dialog v-model="dialog">
    <q-card class="w-xl">
      <q-card-section>
        <p class="text-h6">
          Scan Barcode
        </p>
        {{ decodedText }}
        <StreamBarcodeReader
          torch
          no-front-cameras
          @decode="onDecode"
          @loaded="onLoaded"
        />
      </q-card-section>
    </q-card>
  </q-dialog>
</template>
