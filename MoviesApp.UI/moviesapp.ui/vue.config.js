const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  devServer: {
    proxy: {
      '/Movies': {
        target: process.env.VUE_APP_API_URL,
        changeOrigin: true,
      },
    },
  },
})
