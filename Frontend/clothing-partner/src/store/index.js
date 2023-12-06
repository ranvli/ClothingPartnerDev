// store/index.js
import Vue from 'vue';
import Vuex from 'vuex';
import userData from '../assets/data/users.json';
import Cookies from 'js-cookie';
import CryptoJS from 'crypto-js';
import axios from 'axios';
import employeesModule from './modules/employees.js'


Vue.use(Vuex);


const store = new Vuex.Store({
  modules: {
    employees: employeesModule,
  },
  state: {
    user: null, 
    token: null,
    //apiBaseUrl: "http://4.157.254.214:8080/api/",
    apiBaseUrl: "https://localhost:7214/api/",
  },
  mutations: {
    setUser(state, user) {
      state.user = user;
    },
    setToken(state, token) {
      state.token = token;
    },
  },
  actions: {
    async loginUser({ commit, state }, { username, password }) {
        try {
          const response = await login(username, password, state);
          console.log('Response loginUser en Store: ',response)
          
          if (response && response.success) {
            const { token } = response.loginResponse.data;

            const encryptedUser = CryptoJS.AES.encrypt(JSON.stringify(username), 'clothingPartner').toString();
            const encryptedToken = CryptoJS.AES.encrypt(token, 'clothingPartner').toString(); 

            Cookies.set('encryptedUser', encryptedUser, { secure: true });
            Cookies.set('encryptedToken', encryptedToken, { secure: true });

            commit('setUser', username);
            commit('setToken', token);
       
            return {success  : true};
          } else {
            console.error('Wrong Credentials'); 
            clearCookies()
            return {success  : false};
          }
        } catch (error) {
          console.error('Error al iniciar sesión', error);
          clearCookies();
          return {success  : false, error};
        }
      },
      logout({ commit }){
        clearCookies();
        commit('setUser', null);
        commit('setToken', null);

      },
      checkStoredUser({ commit }) {

        const encryptedUser = Cookies.get('encryptedUser');
        const encryptedToken = Cookies.get('encryptedToken');

        if (encryptedUser && encryptedToken ) {
          // Descifrar datos y actualizar el estado
          const decryptedUser = CryptoJS.AES.decrypt(encryptedUser, 'clothingPartner').toString(CryptoJS.enc.Utf8);
          const decryptedToken = CryptoJS.AES.decrypt(encryptedToken, 'clothingPartner').toString(CryptoJS.enc.Utf8);

          if (decryptedUser && decryptedToken ) {
            commit('setUser', JSON.parse(decryptedUser));
            commit('setToken', decryptedToken);
          }
        }
      },
      
    },
  getters : {
    
  },
  });


  function clearCookies() {
    Cookies.remove('encryptedUser');
    Cookies.remove('encryptedToken');
  }


  async function login(username, password, state) {
    try {
      const loginUrl = `${state.apiBaseUrl}Auth/user-login?username=${encodeURIComponent(username)}&password=${encodeURIComponent(password)}`;

      const response = await axios.post(loginUrl);
      console.log('Response en funcion login store: ',response);

      if (response.status === 200) {
        return { loginResponse: response, success: true };
      } else { 
        console.error('Error on API request');
        return { success: false };
      }
    } catch (error) {
      console.error('Error trying to login: ', error);
      return { success: false, error };
    }
  }



export default store;
