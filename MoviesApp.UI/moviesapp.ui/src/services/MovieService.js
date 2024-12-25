let _apiClient;

export default {
    init(apiClient) {
      _apiClient = apiClient;
    },

    async getAll() {
      return _apiClient.get('/Movies');
    },
  
    async getById(id) {
      return _apiClient.get(`/Movie/${id}`);
    },
  
    async create(movie) {
      return _apiClient.post('/Create', movie);
    },
  
    async update(movie) {
      return _apiClient.put(`/Update`, movie);
    },
      
    async delete(movieId) {
      return _apiClient.delete(`/Remove/${movieId}`);
    },

    async downloadAndSave() {
      return _apiClient.get('/DownloadAndSave');
    }
};