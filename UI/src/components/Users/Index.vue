<template>
  <div>
    <h3>Users</h3>
    <h5>Data from API</h5>
    <button class="default" v-on:click="getData()">get data</button>
    <v-server-table url="Profile/FindAll" :columns="columns" ></v-server-table>
    <ul>
      <li v-for="item in someData">
        {{item.fullName}}
      </li>
    </ul>
  </div>
</template>
<script>
export default {
  data () {
    return {
      columns: ['id', 'fullName'],
      msg: 'test',
      someData: ''
    }
  },
  computed: {

  },
  methods: {
    getData: function () {
      this.$http.get('Profile/FindAll').then(response => {
        // get body data
        this.someData = response.body
      }, response => {
        // error callback
      })
    }
  }
}
</script>
<style lang="styl">

</style>
