<script setup lang="ts">
import type { QTableProps } from 'quasar'

const deliveryStatus = useDeliveryStatus()
const deliveries = useRequestTable(r => api.get('/deliveries', { params: r }).then(r => r.data), { search: '' })

const columns: QTableProps['columns'] = [
  {
    label: 'Reference Number',
    field: 'referenceNumber',
    name: 'referenceNumber',
    align: 'left',
    style: 'width: 120px',
    sortable: true,
  },
  {
    label: 'Arrival Date',
    field: 'date',
    name: 'date',
    align: 'left',
    style: 'width: 120px',
    format: (value: string) => formatDate(value),
    sortable: true,
  },
  {
    label: 'Recipient',
    field: 'recipientName',
    name: 'recipientName',
    align: 'left',
    sortable: true,
  },
  {
    label: 'Contact Number',
    field: 'contactNumber',
    name: 'contactNumber',
    align: 'left',
    sortable: true,
  },
  {
    label: 'Package Type',
    field: 'packageTypeName',
    name: 'packageTypeName',
    align: 'left',
    sortable: true,
  },
  {
    label: 'Size',
    field: 'sizeTypeName',
    name: 'sizeTypeName',
    align: 'left',
    sortable: true,

  },
  {
    label: 'Amount',
    field: 'amount',
    name: 'amount',
    align: 'left',
    format: (value: number) => formatAmount(value),
    sortable: true,
  },
  {
    label: 'Tracking Number',
    field: 'trackingNumber',
    name: 'trackingNumber',
    align: 'left',
    sortable: true,
  },
  {
    label: 'Address',
    field: 'address',
    name: 'address',
    align: 'left',
    sortable: true,
  },
  {
    label: 'Status',
    field: 'status',
    name: 'status',
    align: 'center',
    sortable: true,
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
          <div class="flex items-center gap-sm">
            <q-input
              v-model="deliveries.request.search" label="Search" class="w-96" hide-bottom-space dense
              placeholder="Search by Ref. #, Recipient Name, Address, Tracking #"
            />
            <QBtn label="Create" color="primary" to="/deliveries/form" icon-right="task_alt" />
          </div>
        </div>
        <q-table
          v-model:pagination="deliveries.pagination"
          :rows="deliveries.response.rows"
          flat
          :columns="columns"
          :filter="deliveries.request"
          @request="r => deliveries.onRequest(r)"
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
      </QCardSection>
    </QCard>
  </QPage>
</template>
