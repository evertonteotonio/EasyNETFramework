// === DEFAULT / CUSTOM STYLE ===
// WARNING! always comment out ONE of the two require() calls below.
// 1. use next line to activate CUSTOM STYLE (./src/themes)
// require(`./themes/app.${__THEME}.styl`)
// 2. or, use next line to activate DEFAULT QUASAR STYLE
require(`quasar/dist/quasar.${__THEME}.css`)
// ==============================
/* eslint-disable no-new */
import Vue from 'vue'
import Quasar from 'quasar'
import Router from './router'
import VueResource from 'vue-resource'

/* eslint-disable no-new */
Vue.use(Quasar) // Install Quasar Framework
Vue.use(VueResource)
Vue.http.options.root = 'http://localhost:53161/api'
Quasar.start(() => {
  new Vue({
    el: '#q-app',
    router: Router,
    render: h => h(require('./App'))
  })
})
