<template>
  <div>
    <h3>Users</h3>
    <h5>Data from API</h5>
    <button class="default" v-on:click="getData()">get data</button>
    <q-data-table :data="dataTable"
                  :config="config"
                  :columns="columns">
      <!-- Custom renderer for "message" column -->
      <template slot="col-message" scope="cell">
        <span class="light-paragraph">{{cell.data}}</span>
      </template>
      <!-- Custom renderer for "source" column -->
      <template slot="col-source" scope="cell">
        <span v-if="cell.data === 'Audit'" class="label text-white bg-primary">
          Audit
          <q-tooltip>Some data</q-tooltip>
        </span>
        <span v-else class="label text-white bg-negative">{{cell.data}}</span>
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
  </div>
</template>
<script>
export default {
  data () {
    return {
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
          rows: 'Rooows',
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
    }
  }
}
</script>
<style lang="styl">

</style>
