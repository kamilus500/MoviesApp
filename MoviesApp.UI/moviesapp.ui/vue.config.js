const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  devServer: {
    proxy: {
      '/Movies': {
        target: 'https://localhost:7216',
        changeOrigin: true,
      },
    },
  },
})
