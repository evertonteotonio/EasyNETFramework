import Vue from 'vue'
import Vuex from 'vuex'
Vue.use(Vuex)
/* eslint-disable no-new */
const store = new Vuex.Store({
  state: {
    token: '',
    user: {}
  },
  mutations: {
    increment (state, value) {
      state.token = value
    },
    userData (state, value) {
      state.user = value
    }
  },
  getters: {
    getAccessToken: state => {
      return state.token
    },
    getUserInfo: state => {
      return state.user
    }
  }
})
export default store
