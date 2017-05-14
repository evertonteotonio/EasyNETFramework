<template>
  <!-- Don't drop "q-app" class -->
  <div id="q-app">
    <router-view></router-view>
  </div>
</template>

<script>
/*
 * Root component
 */
import { Loading, Events } from 'quasar'
Events.$on('app:loading', state => {
  Loading.show()
})
export default {}
</script>

<style></style>
