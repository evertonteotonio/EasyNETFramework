<template>
  <q-layout>
      <!-- Header -->
      <div slot="header" class="toolbar">
        <!-- opens left-side drawer using its ref -->
        <button class="hide-on-drawer-visible" @click="$refs.leftDrawer.open()">
          <i>menu</i>
        </button>
        <q-toolbar-title :padding="1">
          EasyNETFramework v0.1
        </q-toolbar-title>
        <!-- opens right-side drawer using its ref -->
        <!--<button class="hide-on-drawer-visible" @click="$refs.rightDrawer.open()">
          <i>menu</i>
        </button>-->
      </div>
      <!-- Navigation Tabs -->
      <!--<q-tabs slot="navigation">
        <q-tab icon="home" route="/Admin/Dashboard" exact replace>Home</q-tab>
        <q-tab icon="alarm" route="/layout/alarm" exact replace>Alarms</q-tab>
        <q-tab icon="help" route="/layout/help" exact replace>Help</q-tab>
      </q-tabs>-->
      <!-- Left-side Drawer -->
      <q-drawer ref="leftDrawer">
        <div class="toolbar">
          <q-toolbar-title>
            Main Menu
          </q-toolbar-title>
        </div>
        <div class="list no-border platform-delimiter">
          <q-drawer-link icon="home" :to="{path: '/Admin/Dashboard', exact: true}">
            Dashboard
          </q-drawer-link>
        </div>
        <div class="list no-border platform-delimiter">
          <q-drawer-link icon="account_box" :to="{path: '/Admin/Users', exact: true}">
            Users
          </q-drawer-link>
        </div>
      </q-drawer>
      <!-- IF USING subRoutes only: -->
      <router-view class="layout-view"></router-view>
      <!-- OR ELSE, IF NOT USING subRoutes: -->
    <!--<div class="layout-view">
      {{msg}}
      <br />
      <h3>Some data value</h3>

      <button @click="getData()">Click for API</button><br />
      <span v-for="item in someData">
        <span>Person name: {{item.fullName}}</span><br />
      </span>
    </div>-->
      <!-- Right-side Drawer -->
      <!--<q-drawer ref="rightDrawer" right-side>
      </q-drawer>-->
      <!-- Footer -->
      <div slot="footer" class="toolbar">
      </div>
    <!--
      Replace following "div" with
      "<router-view class="layout-view">" component
      if using subRoutes
    -->
    
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

  }
}
</script>
<style lang="styl">

</style>
