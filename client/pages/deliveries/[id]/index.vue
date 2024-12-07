<script setup lang="ts">
const route = useRoute()
const $q = useQuasar()
const id = route.params.id
const deliveryStatus = useDeliveryStatus()
const clipboard = useClipboard()
const delivery = useRequest(() => api.get(`/deliveries/${id}`).then(r => r.data))
function copyTrackingNumber() {
  clipboard.copy(delivery.response.trackingNumber)
  $q.notify({
    color: 'primary',
    message: 'Tracking Number Copied to clipboard',
  })
}
function copyReferenceNumber() {
  clipboard.copy(delivery.response.referenceNumber)
  $q.notify({
    color: 'primary',
    message: 'Reference Number Copied to clipboard',
  })
}
function print() {
  window.open(`/deliveries/${id}/print`, '_blank', 'location=yes,height=600,width=800')
}
onMounted(() => delivery.submit())
</script>

<template>
  <q-page>
    <q-card v-if="delivery.response" flat bordered class="q-pa-md">
      <q-card-section>
        <div class="flex items-center justify-between">
          <p class="text-h6">
            <q-btn flat dense to="/deliveries/" color="primary" icon="arrow_back" />
            Delivery Details
          </p>
          <q-btn no-caps icon-right="print" label="Print" color="primary" @click="print()" />
        </div>
        <div class="flex justify-between gap-sm">
          <div class="flex-1">
            <p class="text-lg">
              Reference Number
              <span>
                <q-btn
                  no-caps
                  dense
                  flat
                  icon-right="content_copy"
                  :label="delivery.response.referenceNumber"
                  color="primary"
                  @click="copyReferenceNumber()"
                />
              </span>
            </p>
            <barcode :code="delivery.response.referenceNumber" />
            <q-list separator padding>
              <q-item>
                <q-item-section>
                  <q-item-label>
                    Tracking Number
                  </q-item-label>
                  <q-item-label class="text-bold text-primary">
                    <q-btn no-caps dense flat icon-right="content_copy" :label="delivery.response.trackingNumber" @click="copyTrackingNumber()" />
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label>
                    Arrival Date
                  </q-item-label>
                  <q-item-label class="text-bold">
                    {{ formatDate(delivery.response.date) }}
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label>
                    Recipient
                  </q-item-label>
                  <q-item-label class="text-bold">
                    {{ delivery.response.recipientName }}
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label>
                    Amount
                  </q-item-label>
                  <q-item-label class="text-bold text-secondary text-xl">
                    {{ formatAmount(delivery.response.amount) }}
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label>
                    Contact Number
                  </q-item-label>
                  <q-item-label class="text-bold">
                    {{ delivery.response.contactNumber }}
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label>
                    Address
                  </q-item-label>
                  <q-item-label class="text-bold">
                    {{ delivery.response.address }}
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label>
                    Package Type
                  </q-item-label>
                  <q-item-label class="text-bold">
                    {{ delivery.response.packageTypeName }}
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label>
                    Size
                  </q-item-label>
                  <q-item-label class="text-bold">
                    {{ delivery.response.sizeTypeName }}
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label>
                    Status
                  </q-item-label>
                  <q-item-label class="text-bold">
                    <q-badge :color="deliveryStatus.getColor(delivery.response.deliveryStatus)" :label="delivery.response.deliveryStatusDesc" />
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label>
                    Created At
                  </q-item-label>
                  <q-item-label class="text-bold">
                    {{ formatDate(delivery.response.createdAt) }}
                  </q-item-label>
                </q-item-section>
              </q-item>
            </q-list>
          </div>
          <div class="min-w-400px">
            <!-- timeline  -->
            <p class="mb-sm text-lg font-bold">
              Delivery History
            </p>
            <q-timeline>
              <q-timeline-entry
                v-for="history in delivery.response.histories"
                :key="history.deliveryStatus"
                :title="history.deliveryStatusDesc"
                :subtitle="formatDate(history.createdAt)"
              >
                <div>
                  {{ history.remarks }}
                </div>
              </q-timeline-entry>
            </q-timeline>
          </div>
        </div>
      </q-card-section>
    </q-card>
  </q-page>
</template>
