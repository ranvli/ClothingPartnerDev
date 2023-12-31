/*!

  =========================================================
  * Vue Paper Dashboard 2 PRO - V2.4.0
  =========================================================

  * Product Page: https://www.creative-tim.com/product/vue-paper-dashboard-2-pro
  * Available with purchase of license from https://www.creative-tim.com/product/vue-paper-dashboard-2-pro
  * Copyright 2019 Creative Tim (https://www.creative-tim.com)
  * License Creative Tim (https://www.creative-tim.com/license)

  =========================================================

*/
import Vue from 'vue'
import './pollyfills'
import VueRouter from 'vue-router'
import VueNotify from 'vue-notifyjs'
import lang from 'element-ui/lib/locale/lang/en'
import locale from 'element-ui/lib/locale'
import App from './App.vue'
import store from './store'

// Plugins
import GlobalComponents from './globalComponents' 
import GlobalDirectives from './globalDirectives'
import SideBar from './components/UIComponents/SidebarPlugin'
import initProgress from './progressbar';

// router setup
import routes from './routes/routes'

// library imports

import './assets/sass/paper-dashboard.scss'
import './assets/sass/demo.scss'

import sidebarLinks from './sidebarLinks'
import './registerServiceWorker'

import PerfectScrollbar from 'perfect-scrollbar'
import 'perfect-scrollbar/css/perfect-scrollbar.css';

Vue.prototype.$perfectScrollbar = PerfectScrollbar;

// plugin setup
Vue.use(VueRouter)
Vue.use(GlobalDirectives)
Vue.use(GlobalComponents)
Vue.use(VueNotify)
Vue.use(SideBar, {sidebarLinks: sidebarLinks})
locale.use(lang)

//Quita el mensaje del console cuando esta en modo dev
Vue.config.productionTip = false;

// configure router
const router = new VueRouter({
  routes, // short for routes: routes
  linkActiveClass: 'active',
  scrollBehavior: (to) => {
    if (to.hash) {
      return {selector: to.hash}
    } else {
      return { x: 0, y: 0 }
    }
  }
})

router.beforeEach((to, from, next) => {
  console.log('Pase por el beforeach del main: ', to.fullPath);

  store.dispatch('checkStoredUser');

  // const userLoged = sessionStorage.getItem('user');
  const userLoged = store.state.user;

  console.log('userloged:', userLoged);

  let isAuthenticated = false;
  if (userLoged) {
    isAuthenticated = true;
  }
  

  if (to.matched.some((record) => record.meta.requiresAuth) && !isAuthenticated) {
    // Redirige al usuario a la página de inicio de sesión si no está autenticado
    next('/login');
  } else {
    // Permite el acceso a la ruta
    next();
  }
});


initProgress(router);

/* eslint-disable no-new */
new Vue({
  el: '#app',
  render: h => h(App),
  router,
  store
})
