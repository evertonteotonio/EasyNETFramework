<template>
  <div>
    <h3 v-on:click="$refs.newUserModal.open()">Users</h3>
    <button v-on:click=""></button>
    <q-data-table :data="dataTable"
                  :config="config"
                  :columns="columns">
      <!-- Custom renderer for "message" column -->
      <template slot="col-message" scope="cell">
        <span class="light-paragraph">{{cell.data}}</span>
      </template>
      <!-- Custom renderer for "source" column -->
      <template slot="col-source" scope="cell">
        <span class="label text-white bg-negative">{{cell.data}}</span>
      </template>
      <!-- Custom renderer when user selected one or more rows -->
      <template slot="selection" scope="selection">
        <button class="primary clear" @click="changeMessage(selection)">
          <i>edit</i>
        </button>
        <button class="primary clear" @click="deleteRow(selection)">
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
import { Dialog } from 'quasar'
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
      columns: [
        {
          // [REQUIRED] Column name
          label: 'Full Name',
          // [REQUIRED] Row property name
          field: 'fullName',
          // [REQUIRED] Column width
          width: '50px',
          // (optional) Column CSS style
          // "style" can be a function too if you want to apply
          // certain CSS style based on cell value:
          // style (cell_value) { return .... }
          // (optional) Column CSS classes
          classes: 'bg-primary',
          // "classes" can be a function too if you want to apply
          // certain CSS class based on cell value:
          // classes (cell_value) { return .... }
          // (optional) Can filter/search be applied to this column?
          filter: true
        }
      ],
      config: {
        // [REQUIRED] Set the row height
        rowHeight: '50px',
        // (optional) Title to display
        title: 'Users',
        // (optional) No columns header
        noHeader: true,
        // (optional) Display refresh button
        refresh: true,
        // (optional)
        // User will be able to choose which columns
        // should be displayed
        columnPicker: true,
        // (optional) How many columns from the right are sticky
        // (optional)
        // Styling the body of the data table;
        // "minHeight", "maxHeight" or "height" are important
        bodyStyle: {
          maxHeight: '500px'
        },
        // (optional) By default, Data Table is responsive,
        // but you can disable this by setting the property to "false"
        responsive: true,
        // (optional) Use pagination. Set how many rows per page
        // and also specify an additional optional parameter ("options")
        // which forces user to make a selection of how many rows per
        // page he wants from a specific list
        pagination: {
          rowsPerPage: 10,
          options: [5, 10, 15, 30, 50, 500]
        },
        // (optional) User can make selections. When "single" is specified
        // then user will be able to select only one row at a time.
        // Otherwise use "multiple".
        selection: 'multiple',
        // (optional) Override default messages when no data is available
        // or the user filtering returned no results.
        messages: {
          noData: '<i>warning</i> No data available to show.',
          noDataAfterFiltering: '<i>warning</i> No results. Please refine your search terms.'
        },
        // (optional) Override default labels. Useful for I18n.
        labels: {
          columns: 'columns',
          allCols: 'Every Cols',
          rows: 'Rows',
          selected: {
            singular: 'item selected.',
            plural: 'items selected.'
          },
          clear: 'clear',
          search: 'Search',
          all: 'All'
        }
      }
    }
  },
  computed: {

  },
  methods: {
    getData: function () {
      this.$http.get('Profile/FindAll?page=1&limit=10').then(response => {
        // get body data
        this.dataTable = response.body.data
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
