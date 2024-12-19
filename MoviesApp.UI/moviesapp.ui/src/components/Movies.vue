<template>
  <LoadingSpinner :isVisible="isLoading" />
  <div class="container">
    <div class="container-buttons">
      <button @click="downloadNewMovies" class="btn btn-success">Download</button>
      <button @click="openDialog(0)" class="btn btn-warning">Create</button>
    </div>
    <table v-if="movies.length > 0" class="table table-striped table-bordered">
      <thead class="table-dark">
        <tr>
          <th scope="col">Title</th>
          <th scope="col">Director</th>
          <th scope="col">Year</th>
          <th scope="col">Rate</th>
          <th></th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="movie in movies" :key="movie.id">
          <td>{{ movie.title }}</td>
          <td>{{ movie.director }}</td>
          <td>{{ movie.year }}</td>
          <td>{{ movie.rate }}</td>
          <td>
            <button @click="openDialog(movie.id, 'edit')" class="btn btn-primary">Edit</button>
          </td>
          <td>
            <button @click="openRemoveDialog(movie.id)" class="btn btn-danger">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
    <p v-else>Loading data...</p>
  </div>

  <div v-if="isDeleteDialogOpen" class="dialog-backdrop">
      <div class="dialog">
        <p>Are you sure to delete this movie?</p>
        <button class="btn btn-primary" @click="confirmAction">Yes</button>
        <button class="btn btn-danger" @click="closeRemoveDialog">Cancel</button>
      </div>
  </div>

  <dialog v-if="isCreateEditDialogOpen" class="dialog-backdrop">
      <div class="dialog">
        <div class="close-button">
          <button @click="closeDialog" class="btn btn-danger">X</button>
        </div>
        <CreateEditMovie @close-Dialog="closeDialog" :mode="this.mode" :movieId="this.selectedMovieId"></CreateEditMovie>
      </div>
  </dialog>

</template>

<script>
import CreateEditMovie from './CreateEditMovie.vue';
import MovieService from '@/services/MovieService';
import ToastService from '@/services/ToastService';
import LoadingSpinner from './LoadingSpinner.vue';

export default {
  name: 'MoviesTable',
  components: {
    CreateEditMovie,
    LoadingSpinner
  },
  data() {
    return {
      selectedMovieId: 0,
      isDeleteDialogOpen: false,
      isCreateEditDialogOpen: false,
      isLoading: false,
      mode: '',
      movies:  [],
    };
  },
  async created() {
    await this.fetchMovies();
  },
  methods: {
    openDialog(movieId, mode) {
      this.mode = mode;
      this.selectedMovieId = movieId;
      this.isCreateEditDialogOpen = true;
    },

    async closeDialog() {
      this.isCreateEditDialogOpen = false;
      await this.fetchMovies();
    },

    openRemoveDialog(movieId) {
      this.selectedMovieId = movieId;
      this.isDeleteDialogOpen = true;
    },

    closeRemoveDialog() {
      this.isDeleteDialogOpen = false;
      this.selectedMovieId = 0;
    },

    async confirmAction() {
      try{
        this.$emit('actionConfirmed');
        await MovieService.delete(this.selectedMovieId);
        await this.fetchMovies();
        this.closeRemoveDialog();
      }
      catch (error) {
        console.log(`Error when deleting movie with id ${this.selectedMovieId}`, error);
        ToastService.showError(error);
      }
    },
      
    async downloadNewMovies() {
      try {
        this.isLoading = true;
        await MovieService.downloadAndSave();
        await this.fetchMovies();
      } catch (error) {
        console.log('Error when download new movies', error);
        ToastService.showError(error);
      } finally {
        this.isLoading = false;
      }
    },

    async fetchMovies() {
      try {
        this.isLoading = true;
        const response = await MovieService.getAll();
        this.movies = response.data;
      } catch (error) {
        console.error('Error when downloading movies:', error);
        ToastService.showError(error);
      } finally {
        this.isLoading = false;
      }
    },
  },
};
</script>

<style scoped>
.dialog-backdrop {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
}

.dialog {
  background: white;
  padding: 20px;
  border-radius: 8px;
  text-align: center;
}

.dialog button {
  margin: 5px;
}

.container-buttons {
  display: flex;
  justify-content: start;
  gap: 1rem;
  margin-bottom: 0.5rem;
}

.close-button {
  display: flex;
  justify-content: end;
}
</style>
