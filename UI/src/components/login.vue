<template>
  <q-layout>
    <div slot="header" class="toolbar">
      <q-toolbar-title :padding="0">
        EasyNETFramework - v0.1
      </q-toolbar-title>
    </div>
    <!--
      Replace following "div" with
      "<router-view class="layout-view">" component
      if using subRoutes
    -->
    <div class="layout-view">
      <br />
      <h3>Login please to continue</h3>
      <input type="text" name="userName" v-model="UserName" />
      <input type="text" name="password" v-model="Password" />
      <button class="button button-default" @click="login()">Click for API</button><br />
    </div>
  </q-layout>
</template>
<script>
export default {
  data () {
    return {
      UserName: '',
      Password: ''
    }
  },
  methods: {
    login: function () {
      this.$http.post('jwt', {UserName: this.UserName, Password: this.Password}).then(response => {
        // get body data
        if (response.body.access_token == null && response.body === 'Invalid credentials') {
          alert('no login can be made!')
        }
        console.log(response.body)
      }, response => {
        // error callback
        alert('no login can be made!')
      })
    }
  }
}
</script>
<style lang="styl">

</style>
