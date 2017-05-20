<template>
  <div>
    <h3>Dashboard</h3>
    <div class="row">
      <div>
        <div class="card">
          <div v-on:click="ShowToast()" class="card-title bg-primary text-white">
            Total Users
          </div>
          <div class="card-content card-force-top-padding">
            {{userCount}}
          </div>
        </div>
      </div>
      <div>
        <div class="card">
          <div v-on:click="ShowToast()" class="card-title bg-primary text-white">
            Total Invoices
          </div>
          <div class="card-content card-force-top-padding">
            {{userCount}}
          </div>
        </div>
      </div>
      <div>
        <div class="card">
          <div v-on:click="ShowToast()" class="card-title bg-primary text-white">
            Total Payments
          </div>
          <div class="card-content card-force-top-padding">
            {{userCount}}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { Toast } from 'quasar'
export default {
  data () {
    return {
      userCount: 0
    }
  },
  computed: {

  },
  methods: {
    userCountAPI: function () {
      this.$http.get('User/Count').then(response => {
        // get body data
        this.userCount = response.body
      }, response => {
        // error callback
      })
    },
    ShowToast: function () {
      Toast.create('Total no. of users in the system')
    }
  },
  created () {
    this.userCountAPI()
  }
}
</script>
<style lang="styl">

</style>
