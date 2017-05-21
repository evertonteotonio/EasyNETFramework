<template>
  <div>
    <h3>Stock</h3>
    <div class="row text-center">
      <div class="width-2of3">
        <div class="card">
          <div v-on:click="UserToast()" class="card-title bg-primary text-white">
            Total Items
          </div>
          <div class="card-content card-force-top-padding">
            {{userCount}}
          </div>
        </div>
      </div>
      <div class="width-2of3">
        <div class="card">
          <div v-on:click="InvoiceToast()" class="card-title bg-primary text-white">
            Total Invoices
          </div>
          <div class="card-content card-force-top-padding">
            {{userCount}}
          </div>
        </div>
      </div>
      <div class="width-2of3">
        <div class="card">
          <div v-on:click="PaymentToast()" class="card-title bg-primary text-white">
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
      this.$http.get('Item/Count').then(response => {
        // get body data
        this.userCount = response.body
      }, response => {
        // error callback
      })
    },
    UserToast: function () {
      Toast.create('Total no. of items')
    },
    InvoiceToast: function () {
      Toast.create('Total no. of Invoices in the system')
    },
    PaymentToast: function () {
      Toast.create('Total no. of Payments in the system')
    }
  },
  created () {
    this.userCountAPI()
  }
}
</script>
<style lang="styl">

</style>
