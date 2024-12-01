import { PackageType } from '~/enums/package-type'

export const packageTypes = [
  {
    value: PackageType.Pouch,
    label: 'Pouch',
  },
  {
    value: PackageType.Box,
    label: 'Box',
  },
  {
    value: PackageType.FlatBox,
    label: 'Flat Box',
  },
  {
    value: PackageType.LongBox,
    label: 'Long Box',
  },
  {
    value: PackageType.LongBoxOversize,
    label: 'Long Box Oversize',
  },
  {
    value: PackageType.Oversize,
    label: 'Oversize',
  },
]
