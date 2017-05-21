import Vue from 'vue'
import VueRouter from 'vue-router'
import Store from './store'
import Vuex from 'vuex'
Vue.use(VueRouter)
Vue.use(Vuex)

function load (component) {
  return () => System.import(`components/${component}.vue`)
}

const router = new VueRouter({
  /*
   * NOTE! VueRouter "history" mode DOESN'T works for Cordova builds,
   * it is only to be used only for websites.
   *
   * If you decide to go with "history" mode, please also open /config/index.js
   * and set "build.publicPath" to something other than an empty string.
   * Example: '/' instead of current ''
   *
   * If switching back to default "hash" mode, don't forget to set the
   * build publicPath back to '' so Cordova builds work again.
   */
  routes: [
    { path: '/', component: load('login') }, // Default
    {
      path: '/Admin',
      component: load('Index'),
      children: [
        {
          path: 'Dashboard',
          component: load('Dashboard/Index')
        },
        {
          path: 'Users',
          component: load('Users/Index')
        },
        {
          path: 'Stock',
          component: load('Stock/Index')
        }
      ]
    }, // Default
    { path: '*', component: load('Error404') } // Not found
  ]
})
router.beforeEach((to, from, next) => {
  if (Store.getters.getAccessToken === '' && to.path !== '/') {
    router.push({ path: '/' })
  }
  else {
    next()
  }
})
export default router
