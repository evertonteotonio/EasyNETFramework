<template>
  <h3>Dashboard main</h3>
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
      this.$http.get('Profile/FindAll', {
        headers: {
          Authorization: 'Bearer ' + localStorage.getItem('token')
        }
      }).then(response => {
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
