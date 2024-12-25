let _apiClient;

export default {
    init(apiClient) {
        _apiClient = apiClient;
    },
    async checkApi() {
        return await _apiClient.get('/check');
    }
}