import MovieService from "@/services/MovieService"
import ToastService from "@/services/ToastService";

export default {
    async getAll() {
        try {
            let response = await MovieService.getAll();
            return response.data;
        } catch(error) {
            console.error('Error when loading movies:', error);
            ToastService.showError(error);
        }
    },

    async getById(movieId) {
        try {
            let response = await MovieService.getById(movieId);
            return response.data;
        } catch(error) {
            console.error(`Error when load movie ${movieId}:`, error);
            ToastService.showError(error);
        }
    },

    async createOrUpdate(movie, mode) {
        try {
            if (mode == 'edit') {
                await MovieService.update(movie);
            } else {
                await MovieService.create(movie); 
            }
        } catch(error) {
            console.error('Error when saving movie:', error);
            ToastService.showError(error);
        }
    },

    async delete(movieId) {
        try {
            await MovieService.delete(movieId);
            let movies = await MovieService.getAll();
            return movies.data;
        } catch(error) {
            console.error(`Error when deleting movie by Id ${movieId}:`, error);
            ToastService.showError(error);
        }
    },

    async downloadAndSave() {
        try {
            await MovieService.downloadAndSave();
            let movies = await MovieService.getAll();
            return movies.data;
        } catch(error) {
            console.error('Error when downloading movies:', error);
            ToastService.showError(error);
        }
    }
}