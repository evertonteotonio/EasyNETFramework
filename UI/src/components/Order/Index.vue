<template>
  <div>
    <h3>Orders</h3>
    <table class="q-table">
      <thead>
        <tr>
          <th></th>
          <th class="text-left">Order Title</th>
          <th class="text-right">Amount</th>
          <th class="text-right">Customer Name</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in orders">
          <td><input type="checkbox" value="" @click="changeState(item)"/></td>
          <td class="text-left">{{item.orderTitle}}</td>
          <td class="text-right">${{item.amount}}</td>
          <td class="text-right">{{item.customerName}}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
export default {
  data () {
    return {
      orders: null
    }
  },
  methods: {
    // API Calls
    getItems: function () {
      this.$http.get('Order/FindAll').then(response => {
        // get body data
        this.orders = response.body
        console.log(response.body)
      }, response => {
        // error callback
      })
    },
    changeState: function (item) {
      console.log(item)
      item.isSelected = !item.isSelected
    }
  },
  created () {
    this.getItems()
  }
}
</script>
<style lang="styl">

</style>
