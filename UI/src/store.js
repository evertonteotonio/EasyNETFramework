import Vue from 'vue'
import Vuex from 'vuex'
Vue.use(Vuex)
/* eslint-disable no-new */
const store = new Vuex.Store({
  state: {
    token: ''
  },
  mutations: {
    increment (state, value) {
      state.token = value
    }
  },
  getters: {
    getAccessToken: state => {
      return state.token
    }
  }
})
export default store
