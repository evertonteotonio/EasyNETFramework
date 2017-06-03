<template>
  <div>
    <h3 v-on:click="$refs.newUserModal.open()">Users</h3>
    <button v-on:click="getData()">test</button>
    <q-data-table :data="table"
                  :config="config"
                  :columns="columns"
                  @refresh="refresh">
      <template slot="col-source" scope="cell">
        <span class="label text-white bg-negative">{{cell.data}}</span>
      </template>

      <template slot="selection" scope="props">
        <button class="primary clear" @click="changeMessage(props)">
          <i>edit</i>
        </button>
        <button class="primary clear" @click="deleteRow(props)">
          <i>delete</i>
        </button>
      </template>
    </q-data-table>
    <q-modal ref="newUserModal" :content-css="{minWidth: '40vw', minHeight: '60vh'}">
      <q-layout>
        <div slot="header" class="toolbar">
          <button @click="$refs.newUserModal.close()">
            <i>keyboard_arrow_left</i>
          </button>
          <q-toolbar-title :padding="1">
            Add new user
          </q-toolbar-title>
        </div>
        <div class="layout-view">
          <div class="layout-padding">
            <div class="row">
              <div><input v-model="UserName" placeholder="Username" v-bind:class="{'has-error':!Validation.UserNameValid}"></div>
            </div>
            <div class="row">
              <div><input type="password" v-model="Password" placeholder="Password" v-bind:class="{'has-error':!Validation.PasswordValid}"></div>
            </div>
            <button class="primary" @click="addNewUser()">Save</button>
            <button class="secondary" @click="$refs.newUserModal.close()">Close</button>
          </div>
        </div>
      </q-layout>
    </q-modal>
    <q-modal ref="newProfileModal" :content-css="{minWidth: '40vw', minHeight: '60vh'}">
      <q-layout>
        <div slot="header" class="toolbar">
          <button @click="$refs.newProfileModal.close()">
            <i>keyboard_arrow_left</i>
          </button>
          <q-toolbar-title :padding="1">
            Add new user
          </q-toolbar-title>
        </div>
        <div class="layout-view">
          <div class="layout-padding">
            <div class="row">
              <div><input v-model="FullName" placeholder="Full Name" v-bind:class="{'has-error':!Validation.FullNameValid}"></div>
            </div>
            <div class="row">
              <div><input v-model="Email" placeholder="Email" v-bind:class="{'has-error':!Validation.EmailValid}"></div>
            </div>
            <div class="row">
              <div><input v-model="Phone" placeholder="Phone" v-bind:class="{'has-error':!Validation.PhoneValid}"></div>
            </div>
            <div class="row">
              <div><input v-model="Mobile" placeholder="Mobile" v-bind:class="{'has-error':!Validation.MobileValid}"></div>
            </div>
            <button class="primary" @click="addNewUser()">Save</button>
            <button class="secondary" @click="$refs.newProfileModal.close()">Close</button>
          </div>
        </div>
      </q-layout>
    </q-modal>
  </div>
</template>
<script>
import { Dialog, Platform, Toast } from 'quasar'
export default {
  data () {
    return {
      UserName: '',
      Password: '',
      FullName: '',
      Email: '',
      Phone: '',
      Mobile: '',
      Validation: {
        UserNameValid: true,
        PasswordValid: true,
        FullNameValid: true,
        EmailValid: true,
        PhoneValid: true,
        MobileValid: true
      },
      msg: 'test',
      dataTable: '',
      table: '',
      config: {
        title: 'Data Table',
        refresh: true,
        columnPicker: true,
        leftStickyColumns: 1,
        rightStickyColumns: 2,
        bodyStyle: {
          maxHeight: Platform.is.mobile ? '50vh' : '500px'
        },
        rowHeight: '50px',
        responsive: true,
        pagination: {
          rowsPerPage: 15,
          options: [5, 10, 15, 30, 50, 500]
        },
        selection: 'multiple',
        messages: {
          noData: '<i>warning</i> No data available to show.',
          noDataAfterFiltering: '<i>warning</i> No results. Please refine your search terms.'
        }
      },
      columns: [
        {
          label: 'Id',
          field: 'id',
          sort: true,
          filter: true,
          format (value) {
            if (value === 'Informational') {
              return '<i class="text-positive">info</i>'
            }
            return value
          },
          width: '80px'
        },
        {
          label: 'Full Name',
          field: 'fullName',
          sort: true,
          filter: true,
          width: '500px'
        },
        {
          label: 'Email',
          field: 'email',
          sort: true,
          filter: true,
          width: '120px'
        }
      ],
      pagination: true,
      rowHeight: 50,
      bodyHeightProp: 'maxHeight',
      bodyHeight: 500
    }
  },
  computed: {

  },
  methods: {
    changeMessage (props) {
      props.rows.forEach(row => {
        row.data.fullName = 'Quasar Framework rocks!'
      })
      Toast.create('Changed "Message" field for selected row(s).')
    },
    deleteRow (props) {
      props.rows.forEach(row => {
        this.table.splice(row.index, 1)
      })
    },
    refresh (done) {
      this.getData()
      done()
      /* this.timeout = setTimeout(() => {
        done()
      }, 5000) */
    },
    getData: function () {
      this.$http.get('Profile/FindAll?page=1&limit=10').then(response => {
        // get body data
        this.table = response.body.data
      }, response => {
        // error callback
      })
    },
    validateNewUser: function () {
      let valid = true
      this.Validation.UserNameValid = true
      this.Validation.PasswordValid = true
      if (this.UserName.trim() === '') {
        this.Validation.UserNameValid = false
        valid = false
      }
      if (this.Password.trim() === '') {
        this.Validation.PasswordValid = false
        valid = false
      }
      return valid
    },
    addNewUser: function () {
      if (this.validateNewUser()) {
        this.$http.post('User', { UserName: this.UserName, Password: this.Password }).then(responseUser => {
          this.$refs.newUserModal.close()
          Dialog.create({
            title: 'Success',
            message: 'User created successfully',
            buttons: [
              {
                label: 'Ok'
              }
            ]
          })
        })
      }
    },
    editUseProfile: function (item) {
      this.$http.post('Profile', { FullName: this.FullName, Email: this.Email, Phone: this.Phone, Mobile: this.Mobile }).then(response => {

      })
    }
  },
  created () {
    this.getData()
  }
}
</script>
<style lang="styl">

</style>
