<template>
  <h1>{{ formHeader }}</h1>
  <form @submit.prevent="handleSubmit">
    <div class="field-container">
      <label for="title">Title:</label>
      <input v-model="form.title" type="text" id="title" placeholder="Insert title">
      <span class="text-center" v-if="v$.title.$error">{{ v$.title.$errors[0].$message }}</span>
    </div>
    <div class="field-container">
      <label for="director">Director:</label>
      <input v-model="form.director" type="text" id="director" placeholder="Insert director">
      <span class="text-center" v-if="v$.director.$error">{{ v$.director.$errors[0].$message }}</span>
    </div>
    <div class="field-container">
      <label for="year">Year:</label>
      <input v-model="form.year" type="number" id="year" placeholder="Insert year">
      <span class="text-center" v-if="v$.year.$error">{{ v$.year.$errors[0].$message }}</span>
    </div>
    <div class="field-container">
      <label for="rate">Rate</label>
      <input v-model="form.rate" type="number" placeholder="Insert rate">
      <span class="text-center" v-if="v$.rate.$error">{{ v$.rate.$errors[0].$message }}</span>
    </div>
    
    <button class="btn btn-primary" type="submit">Save</button>
  </form>
</template>

<script>
import { reactive, computed, onMounted } from 'vue'
import { useVuelidate } from '@vuelidate/core'
import { maxLength ,between, required } from '@vuelidate/validators'
import MovieHandler from '@/handlers/MovieHandler';

export default {
  props: {
      movieId: {
        type: Number,
        required
      },
      mode: {
        type: String,
        required
      }
    },
  setup(props, {emit}) {
    let formHeader = props.mode === 'edit' ? 'Edit movie' : 'Create movie';

    const form = reactive({
      title: '',
      director: '',
      year: 0,
      rate: 0
    })

    const rules = computed(() => {
      return {
        title: { 
          required,
          maxLength: maxLength(200)
        },
        director: {
        },
        year: {
          required,
          between: between(1900, 2200)
        },
        rate: {
          between: between(1, 10)
        }
      }
    })

    const fetchMovie = async () => {
      const movie = await MovieHandler.getById(props.movieId);
      form.title = movie.title;
      form.director = movie.director;
      form.year = movie.year;
      form.rate = movie.rate;
    };

    const v$ = useVuelidate(rules, form, { $autoDirty: true })

    const handleSubmit = async () => {
      const isValid = await v$.value.$validate()
      if (!isValid) {
        return
      }

      const movie = {
        id: props.movieId,
        title: form.title,
        director: form.director,
        year: form.year,
        rate: form.rate
      };

      await MovieHandler.createOrUpdate(movie, props.mode);

      emit('close-Dialog');

      emit('refresh-Data');
    }

    if (props.mode == "edit") {
      onMounted(fetchMovie);
    }

    return {
      formHeader,
      form,
      v$,
      handleSubmit
    }
  }
}

</script>

<style lang="css">
form {
  width: 400px;
  margin: 0 auto;
  background-color: #f1f1f1;
  padding: 30px;
  border-radius: 20px;
}
div.field-container {
  display: flex;
  flex-direction: column;
  margin-bottom: 10px;
  height: 118px;
}
label {
  text-align: left;
}
input {
  display: block;
  box-sizing: border-box;
  border: none;
  outline: none;
  border-bottom: 1px solid #ddd;
  border-radius: 20px;
  font-size: 1em;
  padding: 20px;
  margin: 10px 0 5px 0;
  width: 100%;
}
span {
  color: red;
  font-size: 0.8em;
  text-align: left;
}
button {
  background-color: #3498db;
  padding: 10px 20px;
  margin-top: 10px;
  border: none;
  color: white;
}
</style>