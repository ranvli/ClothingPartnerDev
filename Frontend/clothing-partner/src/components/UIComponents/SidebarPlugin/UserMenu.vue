<template>
  <div class="user">
    <div class="photo">
      <img src="static/img/faces/face-2.jpg" alt="user avatar"/>
    </div>
    <div class="info">
      <a data-toggle="collapse" :aria-expanded="!isClosed" @click="toggleMenu" href="#">
         <span>
            {{user }} 
           <b class="caret"></b>
        </span>
        <p>/src/components/UIComponents/SidebarPlugin/UserMenu.vue</p>
      </a>
      <div class="clearfix"></div>
      <div>
        <collapse-transition>
          <ul class="nav nav-menu" v-show="!isClosed">
            <li> 
              <router-link to="/pages/user">
                <!-- <a href="#"> -->
                  <span class="sidebar-mini-icon">Mp</span>
                  <span class="sidebar-normal">My Profile</span>
                <!-- </a> -->
              </router-link>
              
            </li>
            <li>
              <a href="#"> 
                <span class="sidebar-mini-icon">Ep</span>
                <span class="sidebar-normal">Edit Profile</span>
              </a>
            </li>
            <li>
              <a href="#">
                <span class="sidebar-mini-icon">S</span>
                <span class="sidebar-normal">Settings</span>
              </a>
            </li>
          </ul>
        </collapse-transition>
      </div>
    </div>
  </div>
</template>
<script>
  import { CollapseTransition } from 'vue2-transitions'
  import { mapState } from 'vuex';
  

  export default {
    components: {
      CollapseTransition
    },
    data() {
      return {
        isClosed: true,
        //user: this.$store.state.user.username
      }
    },
    methods: {
      toggleMenu() {
        this.isClosed = !this.isClosed
      }
    },
    computed: {
      ...mapState(['user']),
    }
  }
</script>
<style scoped>
  .nav.nav-menu {
    margin-top: 0;
  }
</style>
