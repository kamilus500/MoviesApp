<template>
  <div class="container">
    <div class="container-buttons">
      <button @click="downloadNewMovies" class="btn btn-success">Download</button>
      <button @click="openCreateDialog" class="btn btn-warning">Create</button>
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
            <button class="btn btn-primary">Edit</button>
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

  <dialog v-if="isCreateDialogOpen" class="dialog-backdrop">
      <div class="dialog">
        <div class="close-button">
          <button @click="closeCreateDialog" class="btn btn-danger">Close</button>
        </div>
        <CreateMovie @close-createDialog="closeCreateDialog"></CreateMovie>
      </div>
  </dialog>

</template>

<script>
import axios from 'axios';
import CreateMovie from './CreateMovie.vue';

export default {
  name: 'MoviesTable',
  components: {
    CreateMovie
  },
  data() {
    return {
      selectedMovieId: 0,
      isDeleteDialogOpen: false,
      isCreateDialogOpen: false,
      movies:  [],
    };
  },
  created() {
    this.fetchMovies();
  },
  methods: {
    openCreateDialog() {
      this.isCreateDialogOpen = true;
    },
    async closeCreateDialog() {
      this.isCreateDialogOpen = false;
      await this.fetchMovies();
    },
    openRemoveDialog(movieId) {
      this.selectedMovieId = movieId;
      this.isDeleteDialogOpen = true;
    },
    closeRemoveDialog() {
      this.isDeleteDialogOpen = false;
    },
    async confirmAction() {
      this.$emit('actionConfirmed');
      await axios.delete(`https://localhost:7216/Remove/${this.selectedMovieId}`);
      await this.fetchMovies();
      this.closeRemoveDialog();
    },
      
    async downloadNewMovies() {
      try {
        await axios.get(`https://localhost:7216/Download`);
        await this.fetchMovies();
      } catch (error) {
        console.log('Error when download new movies', error);
      }
    },

    async fetchMovies() {
      try {
        const response = await axios.get('https://localhost:7216/Movies');
        this.movies = response.data;
      } catch (error) {
        console.error('Error when downloading movies:', error);
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
