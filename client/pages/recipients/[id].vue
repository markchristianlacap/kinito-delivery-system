<script setup lang="ts">
import type { QTable } from 'quasar'

const route = useRoute()
const id = route.params.id as string
const recipient = useRequest(() => api.get(`/recipients/${id}`).then(r => r.data))
const deliveries = useRequestTable(() => api.get(`/recipients/${id}/deliveries`).then(r => r.data))
const deliveryStatus = useDeliveryStatus()
const cols: QTable['columns'] = [
  {
    label: 'Reference Number',
    field: 'referenceNumber',
    name: 'referenceNumber',
    align: 'left',
    style: 'width: 120px',
  },
  {
    label: 'Arrival Date',
    field: 'date',
    name: 'date',
    align: 'left',
    style: 'width: 120px',
    format: (value: string) => formatDate(value),
  },
  {
    label: 'Recipient',
    field: 'recipientName',
    name: 'recipientName',
    align: 'left',
  },
  {
    label: 'Contact Number',
    field: 'contactNumber',
    name: 'contactNumber',
    align: 'left',
  },
  {
    label: 'Package Type',
    field: 'packageTypeName',
    name: 'packageTypeName',
    align: 'left',
  },
  {
    label: 'Size',
    field: 'sizeTypeName',
    name: 'sizeTypeName',
    align: 'left',

  },
  {
    label: 'Amount',
    field: 'amount',
    name: 'amount',
    align: 'left',
    format: (value: number) => formatAmount(value),
  },
  {
    label: 'Tracking Number',
    field: 'trackingNumber',
    name: 'trackingNumber',
    align: 'left',
  },
  {
    label: 'Address',
    field: 'address',
    name: 'address',
    align: 'left',
  },
  {
    label: 'Status',
    field: 'status',
    name: 'status',
    align: 'center',
  },
]

onMounted(() => {
  recipient.submit()
  deliveries.submit()
})
</script>

<template>
  <q-page v-if="recipient.response" padding>
    <p class="text-h6">
      <q-btn flat dense color="primary" icon="arrow_back" @click="$router.back()" />
      {{ recipient.response.lastName }}, {{ recipient.response.firstName }}
    </p>
    <p class="my-4 text-lg">
      Personal Details
    </p>
    <ul class="list-inside space-y-2">
      <li>
        Last Name: {{ recipient.response.lastName }}
      </li>
      <li>
        First Name: {{ recipient.response.firstName }}
      </li>
      <li>
        Middle Name: {{ recipient.response.middleName }}
      </li>
      <li>
        Address: {{ recipient.response.address }}
      </li>
      <li>
        Contact Number: {{ recipient.response.contactNumber }}
      </li>
      <li>
        Email: {{ recipient.response.email }}
      </li>
    </ul>
    <p class="my-4 text-lg">
      Deliveries
    </p>
    <q-table
      v-model:pagination="deliveries.pagination"
      :rows="deliveries.response.rows"
      flat
      :columns="cols"
      @request="() => deliveries.submit()"
    >
      <template #body-cell-status="props">
        <q-td :props="props">
          <q-badge :color="deliveryStatus.getColor(props.row.deliveryStatus)" :label="props.row.deliveryStatusDesc" />
        </q-td>
      </template>
      <template #body-cell-referenceNumber="props">
        <q-td :props="props">
          <q-btn flat dense :label="props.row.referenceNumber" :to="`/deliveries/${props.row.id}`" no-caps color="primary" class="underline" />
        </q-td>
      </template>
    </q-table>
  </q-page>
</template>
