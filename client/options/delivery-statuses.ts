import { DeliveryStatus } from '~/enums/delivery-status'

export const deliveryStatuses = [
  {
    value: DeliveryStatus.Encoded,
    label: 'Encoded',
    color: 'primary',
    icon: 'edit',
  },
  {
    value: DeliveryStatus.Shipped,
    label: 'Shipped',
    color: 'green',
    icon: 'local_shipping',
  },
  {
    value: DeliveryStatus.Arrive,
    label: 'Arrive',
    color: 'orange',
    icon: 'pin_drop',
  },
  {
    value: DeliveryStatus.OutForDelivery,
    label: 'Out For Delivery',
    color: 'green',
    icon: 'motorcycle',
  },
  {
    value: DeliveryStatus.Delivered,
    label: 'Delivered',
    color: 'blue',
    icon: 'check_circle',
  },
  {
    value: DeliveryStatus.Rescheduled,
    label: 'Rescheduled',
    color: 'red',
    icon: 'schedule',
  },
]
