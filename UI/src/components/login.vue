<template>
  <q-layout>
    <!--
      Replace following "div" with
      "<router-view class="layout-view">" component
      if using subRoutes
    -->
    <div class="layout-view">
      <br/>
      <div class="row">
        <div class="width-1of3"></div>
        <div class="width-1of3" style="text-align:center;">
          <h5>Login</h5>
          <input type="text" style="text-align:center;" name="userName" v-model="UserName" placeholder="Username" v-bind:class="{'has-error':!Validation.UserNameValid}" />
          <br />
          <input type="password" style="text-align:center;" name="password" v-model="Password" placeholder="Password" v-bind:class="{'has-error':!Validation.PasswordValid}" />
          <br /><br />
          <button class="primary big" @click="login()">Login</button>
        </div>
        <div class="width-1of3"></div>
      </div>
      <br />
    </div>
  </q-layout>
</template>
<script>
import { Toast, SessionStorage } from 'quasar'
export default {
  data () {
    return {
      UserName: '',
      Password: '',
      Validation: {
        UserNameValid: true,
        PasswordValid: true
      }
    }
  },
  methods: {
    login: function () {
      if (this.validateLogin()) {
        this.$http.post('jwt', { UserName: this.UserName, Password: this.Password }).then(response => {
          // get body data
          console.log(response.body)
          SessionStorage.set('token', response.body.access_token)
          this.$router.push({ path: 'Admin/Dashboard' })
        }, response => {
          // error callback
          Toast.create.negative({html: 'Sorry user cannot login', timeout: 2500})
        })
      }
    },
    validateLogin: function () {
      let valid = true
      this.Validation.UserNameValid = true
      this.Validation.PasswordValid = true
      if (this.UserName === '') {
        this.Validation.UserNameValid = false
        valid = false
      }
      if (this.Password === '') {
        this.Validation.PasswordValid = false
        valid = false
      }
      return valid
    }
  }
}
</script>
<style lang="styl">

</style>
