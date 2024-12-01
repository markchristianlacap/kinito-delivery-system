import { DeliveryStatus } from '~/enums/delivery-status'

export const deliveryStatuses = [
  {
    value: DeliveryStatus.Encoded,
    label: 'Encoded',
    color: 'primary',
  },
  {
    value: DeliveryStatus.Shipped,
    label: 'Shipped',
    color: 'secondary',
  },
  {
    value: DeliveryStatus.Arrive,
    label: 'Arrive',
    color: 'orange',
  },
  {
    value: DeliveryStatus.OutForDelivery,
    label: 'Out For Delivery',
    color: 'green',
  },
  {
    value: DeliveryStatus.Delivered,
    label: 'Delivered',
    color: 'blue',
  },
  {
    value: DeliveryStatus.Rescheduled,
    label: 'Rescheduled',
    color: 'red',
  },
]
