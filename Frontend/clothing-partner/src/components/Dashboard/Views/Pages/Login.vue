<template>
  <div class="login-page">
    <!-- <app-navbar></app-navbar> -->
    <div class="wrapper wrapper-full-page">
      <div class="full-page login-page section-image">
        <!--   you can change the color of the filter page using: data-color="blue | azure | green | orange | red | purple" -->
        <div class="content">
          <div class="container">
            <div class="col-lg-4 col-md-6 ml-auto mr-auto">
              <form @submit.prevent="login">
                <card type="login">
                  <h3 slot="header" class="header text-center">Login</h3>

                  <fg-input v-model="form.username" addon-left-icon="nc-icon nc-single-02" @input="clearError"
                            placeholder="First Name..."></fg-input>

                  <fg-input v-model="form.password" addon-left-icon="nc-icon nc-key-25" placeholder="Password" @input="clearError"
                            type="password"></fg-input>

                  <br>
                  <div class="text-danger text-center" v-if="error"> {{ error }}</div>
            <!--       <p-checkbox>
                    Subscribe to newsletter
                  </p-checkbox> -->

                  <p-button native-type="submit" slot="footer" type="warning" round block class="mb-3">Login</p-button>
                </card>
              </form>
            </div>
          </div>
        </div>
        <!-- <app-footer></app-footer> -->
        <div class="full-page-background" style="background-image: url(static/img/background/background-2.jpg) "></div>
      </div>
    </div>
  </div>
</template>
<script>
  import { Card, Checkbox, Button } from 'src/components/UIComponents';

  import { mapActions} from 'vuex'

  // import AppNavbar from './Layout/AppNavbar'
  // import AppFooter from './Layout/AppFooter'

  export default {
    components: {
      Card,
      // AppNavbar,
      // AppFooter,
      [Checkbox.name]: Checkbox,
      [Button.name]: Button
    },
    methods: {
      ...mapActions(['loginUser']),
      toggleNavbar() {
        document.body.classList.toggle('nav-open')
      },
      closeMenu() {
        document.body.classList.remove('nav-open')
        document.body.classList.remove('off-canvas-sidebar')
      },
      login() {
        // handle login here
        // calling "loginUser" in Vuex
        this.loginUser({ username: this.form.username, password: this.form.password })
          .then((response) => {
            console.log('Response en Login: ', response)
            if (response.success) {
              // El inicio de sesión fue exitoso, redirigir a la página del perfil del usuario
              this.$router.push({ name: 'User Page' });
            } else {
              // Manejar un inicio de sesión fallido
              // alert('Inicio de sesión fallido. Por favor, verifica tus credenciales.');
              this.error = 'Login failed !!'
            }
          })
          .catch((error) => {
            console.error('Error al iniciar sesión', error);
          });
      },
      clearError(){
        this.error = ''
      },
    },
    data() {
      return {
        form: {
          username: '',
          password: ''
        },
        error: ''
      }
    },
    beforeDestroy() {
      this.closeMenu()
    }
  }
</script>
<style>
</style>
