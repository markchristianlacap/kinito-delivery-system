export default defineNuxtConfig({
  ssr: false,
  app: {
    head: {
      charset: 'utf-8',
      viewport: 'width=device-width, initial-scale=1',
      title:
        'Kinito Delivery System',
    },
  },
  compatibilityDate: '2024-04-03',
  devtools: { enabled: true },
  css: ['@unocss/reset/tailwind-compat.css', '~/assets/css/main.css'],
  modules: ['@unocss/nuxt', '@nuxt/eslint', 'nuxt-quasar-ui', '@vueuse/nuxt'],
  eslint: {
    config: {
      standalone: false,
    },
  },
  quasar: {
    plugins: ['Notify', 'Dialog'],
    config: {
      brand:
      {
        'primary': '#D32F2F', // A bold and modern crimson red for primary elements
        'secondary': '#37474F', // A sleek, muted blue-gray for secondary highlights
        'accent': '#FF5252', // A vibrant and energetic red-pink for accents

        'dark': '#212121', // A deep charcoal gray for backgrounds
        'dark-page': '#121212', // An even darker shade for page backgrounds

        'positive': '#4CAF50', // A lively green for positive messages
        'negative': '#B71C1C', // A deep, impactful red for negative alerts
        'info': '#0288D1', // A crisp blue for informational content
        'warning': '#FB8C00', // A striking amber-orange for warnings
      },
    },
  },
})
