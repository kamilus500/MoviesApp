import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

export default {
    showError(message) {
        toast.error(message, {
            autoClose: 2500
        })
    }
}