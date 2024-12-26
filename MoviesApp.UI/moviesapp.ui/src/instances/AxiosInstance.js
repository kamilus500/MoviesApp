import ToastService from "@/services/ToastService";
import axios from "axios";

const apiClient = axios.create({
    baseURL: process.env.VUE_APP_API_URL
});

apiClient.interceptors.response.use(
    (response) => {
        return response
    },
    (error) => {
        if (!error.response) {
            ToastService.showError('Cannot connect with Api');
        } else {
            ToastService.showError(`Something is wrong:  ${error.message}`)
        }
    }
)

export default apiClient;