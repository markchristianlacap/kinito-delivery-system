<script setup lang="ts">
import VueBarcode from '@chenfengyuan/vue-barcode'

const props = defineProps<{
  code: string
}>()
</script>

<template>
  <VueBarcode :value="props.code" />
</template>
