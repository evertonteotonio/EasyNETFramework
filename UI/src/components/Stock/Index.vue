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
            {{itemCount}}
          </div>
        </div>
      </div>
      <div class="width-2of3">
        <div class="card">
          <div v-on:click="InvoiceToast()" class="card-title bg-primary text-white">
            Total Invoices
          </div>
          <div class="card-content card-force-top-padding">
            {{itemAvailable}}
          </div>
        </div>
      </div>
      <div class="width-2of3">
        <div class="card">
          <div v-on:click="PaymentToast()" class="card-title bg-primary text-white">
            Total Payments
          </div>
          <div class="card-content card-force-top-padding">
            {{itemOut}}
          </div>
        </div>
      </div>
    </div>
    <table class="q-table">
      <thead>
        <tr>
          <th></th>
          <th class="text-left">Item Name</th>
          <th class="text-right">Price</th>
          <th class="text-right">Available</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in stockItem">
          <td><input type="checkbox" value="" @click="changeState(item)"/></td>
          <td class="text-left">{{item.itemName}}</td>
          <td class="text-right">${{item.price}}</td>
          <td class="text-right">{{item.itemStock.available}}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
import { Toast } from 'quasar'
export default {
  data () {
    return {
      itemCount: 0,
      itemAvailable: 0,
      itemOut: 0,
      stockItem: null
    }
  },
  computed: {

  },
  methods: {
    // API Calls
    userCountAPI: function () {
      this.$http.get('Item/Count').then(response => {
        // get body data
        this.itemCount = response.body
      }, response => {
        // error callback
      })
    },
    AvailableCountAPI: function () {
      this.$http.get('Item/Count').then(response => {
        // get body data
        this.itemAvailable = response.body
      }, response => {
        // error callback
      })
    },
    GetStock: function () {
      this.$http.get('Item/ItemWithStock?page=1&limit=1000').then(response => {
        // get body data
        this.stockItem = response.body.data
      }, response => {
        // error callback
      })
    },
    // API Calls
    UserToast: function () {
      Toast.create('Total no. of items')
      this.userCountAPI()
    },
    InvoiceToast: function () {
      Toast.create('Total no. of Invoices in the system')
    },
    PaymentToast: function () {
      Toast.create('Total no. of Payments in the system')
    },
    changeState: function (item) {
      console.log(item)
      item.isSelected = !item.isSelected
    }
  },
  created () {
    this.userCountAPI()
    this.GetStock()
  }
}
</script>
<style lang="styl">

</style>
