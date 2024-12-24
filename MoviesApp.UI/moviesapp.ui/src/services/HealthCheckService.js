const apiClient = axios.create({
    baseURL: process.env.VUE_APP_API_URL
  });
  
import axios from "axios";

export default {
    async checkApi() {
        return await apiClient.get('/check');
    }
}