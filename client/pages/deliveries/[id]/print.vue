<script setup lang="ts">
definePageMeta({
  layout: 'empty',
})
const route = useRoute()
const id = route.params.id
const delivery = useRequest(() => api.get(`/deliveries/${id}`).then(r => r.data))
onMounted(async () => {
  await delivery.submit()
  window.print()
})
</script>

<template>
  <div
    v-if="delivery.response" class="border pa-sm"
  >
    <div class="flex justify-between border-b pb-lg">
      <img src="/images/kinito-dark.png" class="w-150px">
      <div>
        <p class="text-md">
          Date Of Arrival: {{ formatDate(delivery.response.date) }}
        </p>
        <p class="text-lg font-bold">
          Total Amount: {{ formatAmount(delivery.response.amount) }}
        </p>
        <p>Package Type: {{ delivery.response.packageTypeName }}</p>
        <p>Size: {{ delivery.response.sizeTypeName }}</p>
      </div>
    </div>
    <div class="flex justify-center">
      <barcode class="h-100px" :code="delivery.response.referenceNumber" />
    </div>
    <div class="border pa-sm">
      <p>Recipient Details</p>
      <p class="text-lg font-bold">
        {{ delivery.response.recipientName }}
      </p>
      <p>
        Contact #: {{ delivery.response.contactNumber }}
      </p>
      <p class="text-lg">
        {{ delivery.response.address }}
      </p>
    </div>
    <q-markup-table flat bordered class="mt-sm" separator="cell" dense>
      <thead>
        <tr>
          <th>Return</th>
          <th>1st</th>
          <th>2nd</th>
          <th>3rd</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td colspan="4">
            1st Reason
          </td>
        </tr>
        <tr>
          <td colspan="4">
            2nd Reason
          </td>
        </tr>
        <tr>
          <td colspan="4">
            3rd Reason
          </td>
        </tr>
      </tbody>
    </q-markup-table>
  </div>
</template>
