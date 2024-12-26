import MovieService from "@/services/MovieService"

export default {
    async getAll() {
        let response = await MovieService.getAll();
        if (!response) {
            return [];
        }
        return response.data;
    },

    async getById(movieId) {
        let response = await MovieService.getById(movieId);
        if (!response) {
            return [];
        }
        return response.data;
    },

    async createOrUpdate(movie, mode) {
        if (mode == 'edit') {
            await MovieService.update(movie);
        } else {
            await MovieService.create(movie); 
        }
    },

    async delete(movieId) {
        await MovieService.delete(movieId);
        let response = await MovieService.getAll();
        if (!response) {
            return null;
        }
        return response.data;
    },

    async downloadAndSave() {
        await MovieService.downloadAndSave();
        let response = await MovieService.getAll();
        if (!response) {
            return null;
        }
        return response.data;
    }
}