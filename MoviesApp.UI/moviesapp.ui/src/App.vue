<template>
  <h1 class="title">My Movies</h1>
  <Movies/>
</template>

<script>
import Movies from './components/Movies.vue'

export default {
  name: 'App',
  components: {
    Movies
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
}
.title {
  margin: 1rem 0;
}
</style>
