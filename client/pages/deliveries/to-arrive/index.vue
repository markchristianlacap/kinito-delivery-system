<script setup lang="ts">
import type { QTableProps } from 'quasar'

const dialog = ref(false)
const deliveries = useRequestTable(r => api.get('/deliveries/to-arrive', { params: r }).then(r => r.data), { arrivalDate: useToday(), isShipped: null as null | boolean })
const columns: QTableProps['columns'] = [
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
onMounted(() => deliveries.submit())
watchDeep(deliveries.request, () => deliveries.submit())
</script>

<template>
  <QPage>
    <QCard flat bordered>
      <QCardSection>
        <div class="flex items-center justify-between">
          <div class="flex items-center gap-sm">
            <p class="text-h6">
              To Arrive Deliveries
            </p>
          </div>
          <div class="flex items-center gap-sm">
            <q-btn label="Scan" icon-right="qr_code_scanner" color="primary" no-caps @click="dialog = true" />
          </div>
        </div>
        <div class="mt-sm flex items-center justify-between gap-sm">
          <q-input
            v-model="deliveries.request.arrivalDate"
            type="date"
            label="Select Arrival Date"
            class="w-32" hide-bottom-space
          />

          <q-btn-group flat>
            <q-btn
              label="All" icon="list" color="secondary" :outline="deliveries.request.isShipped === null"
              @click="deliveries.request.isShipped = null"
            />
            <q-btn
              label="Shipped" icon="local_shipping" color="secondary" :outline="deliveries.request.isShipped === false"
              @click="deliveries.request.isShipped = false"
            />

            <q-btn
              label="Arrived" icon="done" color="secondary" :outline="deliveries.request.isShipped === true"
              @click="deliveries.request.isShipped = true"
            />
          </q-btn-group>
        </div>
        <q-table
          v-model:pagination="deliveries.pagination"
          :rows="deliveries.response.rows"
          flat
          :columns="columns"
        >
          <template #body-cell-status="props">
            <q-td :props="props">
              <q-badge
                :color="props.row.isShipped ? 'positive' : 'secondary'"
                :label="props.row.isShipped ? 'Arrived' : 'Shipped'"
              />
            </q-td>
          </template>
          <template #body-cell-referenceNumber="props">
            <q-td :props="props">
              <q-btn
                flat
                dense
                :label="props.row.referenceNumber"
                :to="`/deliveries/${props.row.id}`" no-caps color="primary" class="underline"
              />
            </q-td>
          </template>
        </q-table>
        <to-arrive-barcode-scanner v-model:dialog="dialog" @stop="() => deliveries.submit()" />
      </QCardSection>
    </QCard>
  </QPage>
</template>
