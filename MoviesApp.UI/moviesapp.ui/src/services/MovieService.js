import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:7216',
  timeout: 2000,
});

export default {
    async getAll() {
      return apiClient.get('/Movies');
    },
  
    async getById(id) {
      return apiClient.get(`/Movie/${id}`);
    },
  
    async create(movie) {
      return apiClient.post('/Create', movie);
    },
  
    async update(movie) {
      return apiClient.put(`/Update`, movie);
    },
      
    async delete(movieId) {
      return apiClient.delete(`/Remove/${movieId}`);
    },

    async downloadAndSave() {
      return apiClient.get('/DownloadAndSave');
    }
};