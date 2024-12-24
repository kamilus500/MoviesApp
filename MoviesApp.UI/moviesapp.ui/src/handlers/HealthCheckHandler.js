import HealthCheckService from "@/services/HealthCheckService"
import ToastService from "@/services/ToastService"

export default {
    async check() {
        try {
            let response = await HealthCheckService.checkApi();
            return response.status == 200;
        } catch(error) {
            ToastService.showError('Cannot connect with Api');
        }
    }
}