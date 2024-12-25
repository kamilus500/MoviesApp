import { createApp } from 'vue'
import App from './App.vue'

import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"
import apiClient from './instances/AxiosInstance'
import MovieService from './services/MovieService'
import HealthCheckService from './services/HealthCheckService'

let app = createApp(App);

app.config.globalProperties.$http = apiClient;

MovieService.init(apiClient);
HealthCheckService.init(apiClient);

app.mount('#app');
