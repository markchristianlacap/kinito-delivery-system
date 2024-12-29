<script setup lang="ts">
import type { QTableProps } from 'quasar'

const dialog = ref(false)
const deliveries = useRequestTable(r => api.get('/deliveries/to-arrive', { params: r }).then(r => r.data), { arrivalDate: useToday(), isArrived: null as null | boolean, search: null as string | null })
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
            <q-input
              v-model="deliveries.request.search" label="Search" class="w-96" hide-bottom-space dense
              placeholder="Search by Ref. #, Recipient Name, Address, Tracking #"
            />
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
              label="All" icon="list" color="secondary" :outline="deliveries.request.isArrived === null"
              @click="deliveries.request.isArrived = null"
            />
            <q-btn
              label="Shipped" icon="local_shipping" color="secondary" :outline="deliveries.request.isArrived === false"
              @click="deliveries.request.isArrived = false"
            />

            <q-btn
              label="Arrived" icon="done" color="secondary" :outline="deliveries.request.isArrived === true"
              @click="deliveries.request.isArrived = true"
            />
          </q-btn-group>
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
              <q-badge
                :color="props.row.isArrived ? 'positive' : 'secondary'"
                :label="props.row.isArrived ? 'Arrived' : 'Shipped'"
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
