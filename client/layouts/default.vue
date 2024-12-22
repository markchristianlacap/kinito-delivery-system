<script setup lang="ts">
const leftDrawerOpen = ref(true)
const router = useRouter()
const useUserState = useUser()
function toggleDrawer() {
  leftDrawerOpen.value = !leftDrawerOpen.value
}

function openProfile() {
  // Logic for opening the user profile
}

async function logout() {
  await useUserState.logout()
  router.push('/')
}
</script>

<template>
  <q-layout view="hHh lpR fFf">
    <!-- Sidebar -->
    <q-drawer
      v-model="leftDrawerOpen"
      show-if-above side="left" bordered
    >
      <q-list padding>
        <q-item v-ripple clickable to="/dashboard">
          <q-item-section avatar>
            <q-icon name="dashboard" />
          </q-item-section>
          <q-item-section>
            Dashboard
          </q-item-section>
        </q-item>
        <q-item-label header>
          Deliveries
        </q-item-label>
        <q-separator spaced />
        <q-item v-ripple clickable to="/deliveries">
          <q-item-section avatar>
            <q-icon name="work_history" />
          </q-item-section>
          <q-item-section>
            List of Deliveries
          </q-item-section>
        </q-item>
        <q-item v-ripple clickable to="/deliveries/to-ship">
          <q-item-section avatar>
            <q-icon name="local_shipping" />
          </q-item-section>
          <q-item-section>
            To Ship
          </q-item-section>
        </q-item>
        <q-item v-ripple clickable to="/deliveries/to-arrive">
          <q-item-section avatar>
            <q-icon name="check_circle" />
          </q-item-section>
          <q-item-section>
            To Arrive
          </q-item-section>
        </q-item>
        <q-separator spaced />

        <q-item v-ripple clickable to="/recipients">
          <q-item-section avatar>
            <q-icon name="group" />
          </q-item-section>
          <q-item-section>
            Recipients
          </q-item-section>
        </q-item>
        <q-item v-ripple clickable to="/admin/users">
          <q-item-section avatar>
            <q-icon name="admin_panel_settings" />
          </q-item-section>
          <q-item-section>
            User Management
          </q-item-section>
        </q-item>

        <q-item v-ripple clickable to="/reports">
          <q-item-section avatar>
            <q-icon name="bar_chart" />
          </q-item-section>
          <q-item-section>
            Reports
          </q-item-section>
        </q-item>

        <q-item v-ripple clickable to="/settings">
          <q-item-section avatar>
            <q-icon name="settings" />
          </q-item-section>
          <q-item-section>
            Settings
          </q-item-section>
        </q-item>
      </q-list>
    </q-drawer>

    <!-- Top Bar -->
    <q-header elevated class="bg-primary text-white">
      <q-toolbar>
        <q-btn flat dense round icon="menu" @click="toggleDrawer" />

        <q-toolbar-title>Dashboard</q-toolbar-title>

        <q-btn dense flat round icon="account_circle" @click="openProfile" />
        <q-btn dense flat round icon="logout" @click="logout" />
      </q-toolbar>
    </q-header>

    <!-- Main Content Area -->
    <q-page-container>
      <slot />
    </q-page-container>
  </q-layout>
</template>

<style scoped>
/* Add any custom styling here */
</style>
