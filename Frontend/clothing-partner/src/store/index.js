// store/index.js
import Vue from 'vue';
import Vuex from 'vuex';
import userData from '../assets/data/users.json';
import Cookies from 'js-cookie';
import CryptoJS from 'crypto-js';
import axios from 'axios';


Vue.use(Vuex);

const apiUrl = "http://4.157.254.214:8080/api/Auth/user-login";
//const apiUrl = "https://localhost:8080/api/Auth/user-login";

const store = new Vuex.Store({
  state: {
    user: null, 
    token: null, 
  },
  mutations: {
    setUser(state, user) {
      state.user = user;
    },
    setPwd(state, pwd){
      state.pwd = pwd;
    },
    setToken(state, token) {
      state.token = token;
    },
  },
  actions: {
    async loginUser({ commit }, { username, password }) {
        try {
          //const response = await fakeLogin(username, password);
          const response = await login(username, password);
          console.log('Response loginUser en Store: ',response)
          
          if (response && response.success) {
            const { token } = response.loginResponse.data;

            const encryptedUser = CryptoJS.AES.encrypt(JSON.stringify(username), 'clothingPartner').toString();
            const encryptedPwd = CryptoJS.AES.encrypt(JSON.stringify(password), 'clothingPartner').toString();
            const encryptedToken = CryptoJS.AES.encrypt(token, 'clothingPartner').toString(); 

            Cookies.set('encryptedUser', encryptedUser, { secure: true });
            Cookies.set('encryptedPwd', encryptedPwd, { secure: true });
            Cookies.set('encryptedToken', encryptedToken, { secure: true });

            commit('setUser', username);
            commit('setPwd', encryptedPwd)
            commit('setToken', token);
            // sessionStorage.setItem('user', user.username);
            // // sessionStorage.setItem('user', JSON.stringify(user));
            // sessionStorage.setItem('token', token);
            return {success  : true};
          } else {
            console.error('Wrong Credentials'); 
            // sessionStorage.removeItem('user');
            // sessionStorage.removeItem('token');
            clearCookies()
            return {success  : false};
          }
        } catch (error) {
          console.error('Error al iniciar sesión', error);
          // sessionStorage.removeItem('user');
          // sessionStorage.removeItem('token');
          clearCookies();
          return {success  : false, error};
        }
      },
      logout({ commit }){
        // sessionStorage.removeItem('user');
        // sessionStorage.removeItem('token');
        clearCookies();
        commit('setUser', null);
        commit('setPwd', null);
        commit('setToken', null);

      },
      checkStoredUser({ commit }) {

        const encryptedUser = Cookies.get('encryptedUser');
        const encryptedPwd = Cookies.get('encryptedPwd');
        const encryptedToken = Cookies.get('encryptedToken');
  
        if (encryptedUser && encryptedToken && encryptedPwd) {
          // Descifrar datos y actualizar el estado
          const decryptedUser = CryptoJS.AES.decrypt(encryptedUser, 'clothingPartner').toString(CryptoJS.enc.Utf8);
          const decryptedPwd = CryptoJS.AES.decrypt(encryptedPwd, 'clothingPartner').toString(CryptoJS.enc.Utf8);
          const decryptedToken = CryptoJS.AES.decrypt(encryptedToken, 'clothingPartner').toString(CryptoJS.enc.Utf8);
  
          if (decryptedUser && decryptedToken && decryptedPwd) {
            commit('setUser', JSON.parse(decryptedUser));
            commit('setPwd', decryptedPwd);
            commit('setToken', decryptedToken);
          }
        }
      },
    },
  });


  function clearCookies() {
    Cookies.remove('encryptedUser');
    Cookies.remove('encryptedPwd');
    Cookies.remove('encryptedToken');
  }


  async function login(username, password) {
    try {
      const loginUrl = `${apiUrl}?username=${encodeURIComponent(username)}&password=${encodeURIComponent(password)}`;

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

  function fakeLogin(username, password) {
    const { v4: uuid4 } = require('uuid');

    const users = userData.users;
  
    const user = users.find((u) => u.username === username && u.password === password);
  
    if (user) {
      return Promise.resolve({
        success: true,
        data: {
          user: user,
          token: uuid4(),
        },
      });
    } else {

      return Promise.resolve({
        success: false,
      });
    }
  }

export default store;
