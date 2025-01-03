export function formatAmount(amount: number) {
  return new Intl.NumberFormat('en-PH', {
    style: 'currency',
    currency: 'PHP',
  }).format(amount)
}
export function formatNumber(number: number) {
  return new Intl.NumberFormat('en-PH').format(number)
}
export function formatDate(date: string) {
  if (!date)
    return
  const d = new Date(date)
  return d.toLocaleDateString()
}
export function formatName(row: any) {
  const mi = row.middleName ? row.middleName.charAt(0).toUpperCase() : ''
  return `${row.lastName}, ${row.firstName} ${mi}. ${row.extensionName}`
}
