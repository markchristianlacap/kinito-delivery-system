import type { DeliveryStatus } from '~/enums/delivery-status'
import { deliveryStatuses } from '~/options/delivery-statuses'

export function useDeliveryStatus() {
  return {
    getColor(status: DeliveryStatus) {
      const item = deliveryStatuses.find(s => s.value === status)
      return item?.color
    },
    getIcon(status: DeliveryStatus) {
      const item = deliveryStatuses.find(s => s.value === status)
      return item?.icon
    },
  }
}
