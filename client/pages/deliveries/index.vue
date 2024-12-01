<script setup lang="ts">
import type { QTableProps } from 'quasar'

const deliveryStatus = useDeliveryStatus()
const deliveries = useRequestTable(r => api.get('/deliveries', { params: r }).then(r => r.data), { search: '' })
const columns: QTableProps['columns'] = [
  {
    label: 'Tracking Number',
    field: 'trackingNumber',
    name: 'trackingNumber',
    align: 'left',
  },

  {
    label: 'Date',
    field: 'date',
    name: 'date',
    align: 'left',
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
onMounted(() => deliveries.submit())
</script>

<template>
  <QPage>
    <QCard flat bordered>
      <QCardSection>
        <div class="flex items-center justify-between">
          <p class="text-h6">
            List of Deliveries
          </p>
          <QBtn label="Create" color="primary" to="/deliveries/form" icon-right="task_alt" />
        </div>
        <q-table
          v-model:pagination="deliveries.pagination"
          :rows="deliveries.response.rows"
          flat
          :columns="columns"
        >
          <template #body-cell-status="props">
            <q-td :props="props">
              <q-badge :color="deliveryStatus.getColor(props.row.deliveryStatus)" :label="props.row.deliveryStatusDesc" />
            </q-td>
          </template>
          <template #body-cell-trackingNumber="props">
            <q-td :props="props">
              <q-btn flat dense :label="props.row.trackingNumber" :to="`/deliveries/${props.row.id}`" no-caps color="primary" class="underline" />
            </q-td>
          </template>
        </q-table>
      </QCardSection>
    </QCard>
  </QPage>
</template>
