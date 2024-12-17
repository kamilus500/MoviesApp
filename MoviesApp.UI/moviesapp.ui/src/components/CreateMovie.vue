<template>
  <h1>Create Movie</h1>
  <form @submit.prevent="handleSubmit">
    <div class="field-container">
      
      <label for="title">Title:</label>
      <input v-model="form.title" type="text" id="title" placeholder="Insert title">
      
      <span v-if="v$.title.$error">{{ v$.title.$errors[0].$message }}</span>
    </div>
    <div class="field-container">
      
      <label for="director">Director:</label>
      <input v-model="form.director" type="text" id="director" placeholder="Insert director">
      
      <span v-if="v$.director.$error">{{ v$.director.$errors[0].$message }}</span>
    </div>
    <div class="field-container">
      
      <label for="year">Year:</label>
      <input v-model="form.year" type="number" id="year" placeholder="Insert year">
      
      <span v-if="v$.year.$error">{{ v$.year.$errors[0].$message }}</span>
    </div>
    <div class="field-container">
      <label for="rate">Rate</label>
      <input v-model="form.rate" type="number" placeholder="Insert rate">
      
      <span v-if="v$.rate.$error">{{ v$.rate.$errors[0].$message }}</span>
    </div>
    
    <button type="submit">Create</button>
  </form>
</template>

<script>
import { reactive, computed } from 'vue'
import { useVuelidate } from '@vuelidate/core'
import { required } from '@vuelidate/validators'
import axios from 'axios';

export default {
  setup(_, {emit}) {
    const form = reactive({
      title: '',
      director: '',
      year: 0,
      rate: 0
    })

    const rules = computed(() => {
      return {
        title: { required },
        director: {
          required,
        },
        year: {
          required
        },
        rate: {
          required
        }
      }
    })

    const v$ = useVuelidate(rules, form)

    const create = async (movie) => {
      await axios.post('https://localhost:7216/Create', movie);
    }

    const handleSubmit = async () => {
      
      const result = v$.value.$validate()
      if (!result) {
        return
      }

      await create({
        id: 0,
        title: form.title,
        year: form.year,
        director: form.director,
        rate: form.rate
      });

      emit('close-createDialog');
    }

    return {
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