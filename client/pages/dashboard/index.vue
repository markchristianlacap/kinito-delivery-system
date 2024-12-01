<script setup lang="ts">
const useUserState = useUser()
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
          <q-card flat bordered class="q-pa-md q-mb-md row items-center justify-between" style="max-width: 300px;">
            <q-icon name="pending_actions" color="positive" size="lg" />
            <div>
              <div class="text-h5">
                230
              </div>
              <div class="text-subtitle2">
                Total Pending Deliveries
              </div>
            </div>
          </q-card>
          <q-card flat bordered class="q-pa-md q-mb-md row items-center justify-between" style="max-width: 300px;">
            <q-icon name="work_history" color="accent" size="lg" />
            <div>
              <div class="text-h5">
                2,000
              </div>
              <div class="text-subtitle2">
                Done Delivered
              </div>
            </div>
          </q-card>
        </q-card-section>
      </q-card>
    </q-page>
  </div>
</template>
