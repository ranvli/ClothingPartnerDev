// store/index.js
import Vue from 'vue';
import Vuex from 'vuex';
import userData from '../assets/data/users.json';


Vue.use(Vuex);

const store = new Vuex.Store({
  state: {
    user: null, 
    token: null, 
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
    async loginUser({ commit }, { username, password }) {
        try {
          const response = await fakeLogin(username, password);
          console.log('Response: ',response)
          
          if (response && response.success) {
            const { user, token } = response.data;
            commit('setUser', user);
            commit('setToken', token);
            sessionStorage.setItem('user', user.username);
            // sessionStorage.setItem('user', JSON.stringify(user));
            sessionStorage.setItem('token', token);
            return {success  : true};
          } else {
            console.error('Wrong Credentials'); 
            sessionStorage.removeItem('user');
            sessionStorage.removeItem('token');
            return {success  : false};
          }
        } catch (error) {
          console.error('Error al iniciar sesión', error);
          sessionStorage.removeItem('user');
          sessionStorage.removeItem('token');
          return {success  : false, error};
        }
      },
      logout({ commit }){
        sessionStorage.removeItem('user');
        sessionStorage.removeItem('token');
        commit('setUser', null);
        commit('setToken', null);

      }
    },
  });

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
