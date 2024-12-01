<script setup lang="ts">
import type { QTable } from 'quasar'

const dialog = ref(false)
const currentId = ref('')
const recipients = useRequestTable(() => api.get('/recipients').then(r => r.data))
const form = ref({
  lastName: '',
  firstName: '',
  middleName: undefined,
  address: '',
  contactNumber: '',
  email: undefined,
})
const columns: QTable['columns'] = [
  {
    name: 'firstName',
    label: 'First Name',
    field: 'firstName',
    align: 'left',
  },
  {
    name: 'lastName',
    label: 'Last Name',
    field: 'lastName',
    align: 'left',
  },
  {
    name: 'middleName',
    label: 'Middle Name',
    field: 'middleName',
    align: 'left',
  },
  {
    name: 'contactNumber',
    label: 'Contact Number',
    field: 'contactNumber',
    align: 'left',
  },
  {
    name: 'email',
    label: 'Email',
    field: 'email',
    align: 'left',
  },
  {
    name: 'address',
    label: 'Address',
    field: 'address',
    align: 'left',
  },
  {
    name: 'actions',
    label: 'Actions',
    field: 'actions',
    align: 'center',
  },
]
function onEdit(row: any) {
  currentId.value = row.id
  dialog.value = true
  form.value = row
}
onMounted(() => recipients.submit())
</script>

<template>
  <QPage>
    <QCard flat bordered>
      <QCardSection>
        <div class="flex items-center justify-between">
          <p class="text-h6">
            List of Partner Recipients
          </p>
          <QBtn label="Create" color="primary" icon-right="task_alt" @click="dialog = true" />
        </div>
        <q-table v-model:pagination="recipients.pagination" :rows="recipients.response?.rows || []" :columns="columns" flat>
          <template #body-cell-actions="props">
            <q-td :props="props">
              <q-btn
                dense
                color="secondary"
                icon="edit"
                size="sm"
                label="Edit"
                @click="onEdit(props.row)"
              />
            </q-td>
          </template>
        </q-table>
      </QCardSection>
    </QCard>
    <recipients-dialog-form
      :id="currentId"
      v-model:dialog="dialog"
      :form="form" @success="() => recipients.submit()"
    />
  </QPage>
</template>
