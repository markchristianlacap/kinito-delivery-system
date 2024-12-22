<script setup lang="ts">
const useUserState = useUser()
const statuses = useRequest(() => api.get('/dashboard/delivery-statuses').then(res => res.data))
const deliveryStatus = useDeliveryStatus()
onMounted(() => {
  statuses.submit()
})
</script>

<template>
  <div>
    <q-page class="q-pa-md">
      <q-card flat bordered class="q-mb-md">
        <q-card-section>
          <div class="text-h6">
            Welcome to Your Dashboard <span v-if="useUserState">, {{ useUserState.user.value?.firstName }}!</span>
          </div>
          <div class="text-subtitle2 text-grey">
            Quick overview of key metrics
          </div>
        </q-card-section>

        <q-separator />

        <!-- Main dashboard cards -->
        <q-card-section>
          <div class="flex flex-wrap gap-sm">
            <q-card v-for="status in statuses.response || []" :key="status.id" flat bordered style="min-width: 300px;">
              <q-card-section>
                <div class="flex justify-between">
                  <q-icon :name="deliveryStatus.getIcon(status.deliveryStatus)" color="positive" size="lg" />
                  <div>
                    <div :class="`text-${deliveryStatus.getColor(status.deliveryStatus)} text-h5 font-bold`">
                      {{ status.count }}
                    </div>
                    <div class="text-subtitle2">
                      {{ status.deliveryStatusDesc }}
                    </div>
                  </div>
                </div>
              </q-card-section>
            </q-card>
          </div>
        </q-card-section>
      </q-card>
    </q-page>
  </div>
</template>
