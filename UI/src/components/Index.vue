<template>
  <q-layout>
    <div slot="header" class="toolbar">
      <q-toolbar-title :padding="0">
        Quasar Framework v{{$q.version}}
      </q-toolbar-title>
    </div>
    <!--
      Replace following "div" with
      "<router-view class="layout-view">" component
      if using subRoutes
    -->
    <div class="layout-view">
      {{msg}}
      <br />
      <h3>Some data value</h3>
      
      <button @click="getData()">Click for API</button><br />
      <span v-for="item in someData">
        <span>Person name: {{item.fullName}}</span><br/>
      </span>
    </div>
  </q-layout>
</template>
<script>
export default {
  data () {
    return {
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
