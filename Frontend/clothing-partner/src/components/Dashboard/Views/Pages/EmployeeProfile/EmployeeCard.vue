<template>
  <div class="card card-user">
    <div class="image">
      <img src="static/img/background/damir-bosnjak.jpg" alt="...">
    </div>
    <div class="card-body">
      <div class="author">
        <a href="#">
          <img class="avatar border-gray" :src="image" alt="...">
          <h5 class="title">{{selectedEmployee.fullName}} | </h5>
        </a>
        <p class="description">
          @{{selectedEmployee.companyEmail}}
        </p>
      </div>
      <p class="description text-center">
        "You are awesome
        <br> No complains
        <br>Keep going"
      </p>
    </div>
    <div class="card-footer">
      <hr>
      <div class="button-container">
        <!-- <div class="row">
          <div class="col-lg-3 col-md-6 col-6 ml-auto">
            <h5>12
              <br>
              <small>Files</small>
            </h5>
          </div>
          <div class="col-lg-4 col-md-6 col-6 ml-auto mr-auto">
            <h5>2GB
              <br>
              <small>Used</small>
            </h5>
          </div>
          <div class="col-lg-3 mr-auto">
            <h5>24,6$
              <br>
              <small>Spent</small>
            </h5>
          </div>
        </div> -->
      </div>
    </div>
  </div>
</template>
<script>
import { mapState } from 'vuex';
import axios from 'axios';


  export default {
    data() {
      return {
        image: '',
      }
    },
    computed: {
      ...mapState('employees',['selectedEmployee']),
    },
    mounted() {
    axios
      .get('https://randomuser.me/api/')
      .then(response => {
        console.log('Imagen: ',response.data);
        this.image = response.data.results[0].picture.thumbnail
        
      })
      .catch(error => {
        console.error(error);
      });
  }

  }

</script>
<style>

</style>
