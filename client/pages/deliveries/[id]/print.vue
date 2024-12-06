<script setup lang="ts">
definePageMeta({
  layout: 'empty',
})
const route = useRoute()
const id = route.params.id
const delivery = useRequest(() => api.get(`/deliveries/${id}`).then(r => r.data))
onMounted(() => delivery.submit())
</script>

<template>
  <div
    v-if="delivery.response" class="border pa-sm"
  >
    <div class="flex justify-between border-b pb-lg">
      <img src="/images/kinito-dark.png" class="w-150px">
      <div>
        <p class="text-lg">
          Date Of Arrival: {{ formatDate(delivery.response.date) }}
        </p>
        <p class="text-xl font-bold">
          Total Amount: {{ formatAmount(delivery.response.amount) }}
        </p>
      </div>
    </div>
    <div class="flex justify-center">
      <barcode :code="delivery.response.referenceNumber" />
    </div>
    <div class="border pa-sm">
      <p class="text-lg font-bold">
        Address
      </p>
      <p class="text-lg">
        {{ delivery.response.address }}
      </p>
    </div>
  </div>
</template>
