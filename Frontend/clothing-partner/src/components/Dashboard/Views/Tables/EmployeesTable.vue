<!-- Basada en PaginatedTables -->
<template>
  <div class="row">
    <div class="col-md-12">
      <h4 class="title">Employees</h4>
      
    </div>
    <div class="col-md-12 card">
      <div class="card-header">
        <div class="category">Extended tables</div>
      </div>
      <div class="card-body row">
        <div class="col-sm-6">
          <el-select
            class="select-default"
            v-model="pagination.perPage"
            placeholder="Per page">
            <el-option
              class="select-default"
              v-for="item in pagination.perPageOptions"
              :key="item"
              :label="item"
              :value="item">
            </el-option>
          </el-select>
          
        </div>
        <div class="col-sm-6">
          <div class="pull-right">
            <p-button type="success" size="sm" ><i class="fa fa-add"></i>Add Employee</p-button>
          </div>
          
          <div class="pull-right">
            <fg-input class="input-sm"
                      placeholder="Search"
                      v-model="searchQuery"
                      addon-right-icon="nc-icon nc-zoom-split">
            </fg-input>
            
          </div>
          
        </div>
        
        <div class="col-sm-12 mt-2">
          <el-table class="table-striped"
                    :data="queriedData"
                    border
                    style="width: 100%">
            <el-table-column v-for="column in tableColumns"
                             :key="column.label"
                             :min-width="column.minWidth"
                             :prop="column.prop"
                             :label="column.label">
            </el-table-column>
            <el-table-column
              :min-width="90"
              fixed="right"
              class-name="td-actions"
              label="Actions">
              <template slot-scope="props">
                <!-- <p-button type="info" size="sm" icon @click="handleLike(props.$index, props.row)" title="Edit">
                  <i class="fa fa-user"></i>
                </p-button> -->
                <p-button type="success" size="sm" icon @click="handleEdit(props.$index, props.row)" title="Edit">
                  <i class="fa fa-edit"></i>
                </p-button>
                <p-button type="danger" size="sm" icon @click="handleDelete(props.$index, props.row)" title="Delete">
                  <i class="fa fa-times"></i>
                </p-button>
              </template>
            </el-table-column>
          </el-table>
        </div>
        <div class="col-sm-6 pagination-info">
          <p class="category">Showing {{from + 1}} to {{to}} of {{total}} entries</p>
        </div>
        <div class="col-sm-6">
          <p-pagination class="pull-right"
                        v-model="pagination.currentPage"
                        :per-page="pagination.perPage"
                        :total="pagination.total">
          </p-pagination>
        </div>
      </div>
    </div>
    <!-- <modal :show.sync="modals.classic" width="80%" headerClasses="justify-content-center">
      <validationObserver v-slot="{ handleSubmit }">
        <form @submit.prevent="handleSubmit(submit)">
          <div class="card" width="80%">
            <div class="card-header">
              <h4 class="card-title" v-if="selectedEmployee">
                {{ selectedEmployee.fullName }}
              </h4>
            </div>
            <div class="card-body">
              <div class="col-md-8">
                <div class="form-group">
                  <label >Full Name</label>
                  <ValidationProvider 
                    name="fullName"
                    rules="required" 
                    v-slot="{ passed, failed}">
                    <fg-input type="text" 
                      :error="failed ? 'The name field is required': null"
                      :hasSuccess="passed"
                      :name="fullName" v-model="selectedEmployee.fullName">

                    </fg-input>
                  </ValidationProvider>
                  <label >Personal Email</label>
                  <ValidationProvider 
                    name="fullName"
                    rules="required" 
                    v-slot="{ passed, failed}">
                    <fg-input type="text" 
                      :error="failed|email ? 'The name field is required': null"
                      :hasSuccess="passed"
                      :name="fullName" v-model="selectedEmployee.personalEmail">

                    </fg-input>
                  </ValidationProvider>
                </div>
              </div>
              
            </div>
          </div>
        </form>
      </validationObserver>
      <template slot="footer">
        <div class="left-side">
          <p-button type="success" link @click="modals.classic = false">Accept</p-button>
        </div>
        <div class="divider"></div>
        <div class="right-side">
          <p-button type="danger" link @click="modals.classic = false">Cancel</p-button>
        </div>
      </template>
    </modal> -->
    <modal :show.sync="modals.mini"
          class="modal-primary"
          :show-close="false"
          headerClasses="justify-content-center"
          type="mini">
      <div slot="header" class="modal-profile ml-auto mr-auto">
        <i class="nc-icon nc-simple-remove"></i>
      </div>
      <p v-if="selectedEmployee">Confirm Deletion {{ selectedEmployee.fullName }} ?</p>
      <template slot="footer">
        <div class="left-side">
          <p-button type="danger" link @click="handleCommitDelete()">Delete</p-button>
        </div>
        <div class="divider"></div>
        <div class="right-side">
          <p-button type="default" link @click="modals.mini = false">Close</p-button>
        </div>
      </template>
    </modal>
  </div>
</template>

<script>
  import Vue from 'vue'
  import {Table, TableColumn, Select, Option} from 'element-ui'
  import { Card, Button, Modal } from 'src/components/UIComponents'
  import PPagination from 'src/components/UIComponents/Pagination.vue'
  //import users from './users'
  import { mapActions, mapGetters } from 'vuex';
  import { extend } from "vee-validate";
  import { required, email } from "vee-validate/dist/rules";

  Vue.use(Table)
  Vue.use(TableColumn)
  Vue.use(Select)
  Vue.use(Option)

  extend("email",email);
  extend("required", required);


  export default{
    components: {
      PPagination,
      Modal,
    },
    computed: {
      ...mapGetters('employees', ['getAllEmployees']),
      pagedData () {
        //return this.tableData.slice(this.from, this.to);
        return this.getAllEmployees.slice(this.from, this.to)
      },
      /***
       * Searches through table data and returns a paginated array.
       * Note that this should not be used for table with a lot of data as it might be slow!
       * Do the search and the pagination on the server and display the data retrieved from server instead.
       * @returns {computed.pagedData}
       */
      queriedData () {
        if (!this.searchQuery) {
          this.pagination.total = this.getAllEmployees.length
          return this.pagedData
        }
        let result = this.getAllEmployees
          .filter((row) => {
            let isIncluded = false
            console.log('Row en queriedData: ',row)
            for (let key of this.propsToSearch) {
              let rowValue = row[key].toString()
              if (rowValue.includes && rowValue.includes(this.searchQuery)) {
                isIncluded = true
              }
            }
            return isIncluded
          })
        this.pagination.total = result.length
        return result.slice(this.from, this.to)
      },
      to () {
        let highBound = this.from + this.pagination.perPage
        if (this.total < highBound) {
          highBound = this.total
        }
        return highBound
      },
      from () {
        return this.pagination.perPage * (this.pagination.currentPage - 1)
      },
      total () {
        this.pagination.total = this.getAllEmployees.length
        return this.getAllEmployees.length
      }
    },
    data () {
      return {
        pagination: {
          perPage: 5,
          currentPage: 1,
          perPageOptions: [5, 10, 25, 50],
          total: 0
        },
        searchQuery: '',
        propsToSearch: ['fullName', 'personalEmail', 'phone', 'employeeId'],
        tableColumns: [
          {
            prop: 'employeeId',
            label: 'Employee Id',
            minWidth: 200
          },
          {
            prop: 'fullName',
            label: 'Full Name',
            minWidth: 250
          },
          {
            prop: 'personalEmail',
            label: 'Personal Email',
            minWidth: 100
          },
          {
            prop: 'phone',
            label: 'Phone',
            minWidth: 120
          }
        ],
        // tableData: this.getAllEmployees,
        modals: {
          classic: false,
          notice: false,
          mini: false
        },
        selectedEmployee: {},
      }
    },
    methods: {
      ...mapActions('employees',['fetchEmployees','deleteEmployee']),
      //...mapActions('employees',['deleteEmployee']),
      handleEdit (index, row) {
        console.log('Row a Editar: ', row);
        // alert(`Your want to edit ${row.fullName}`);
        this.selectedEmployee = row;
        this.modals.classic = true;
      },
      handleDelete (index, row) {
        console.log('Row en Delete: ', row)
        this.selectedEmployee = row;
        this.modals.mini = true;
      },
      handleCommitDelete(){
        console.log("Borrar empleado en componente: ", this.selectedEmployee );
        const result = this.deleteEmployee(this.selectedEmployee);
        if(result) {
          let indexToDelete = this.getAllEmployees.findIndex((tableRow) => tableRow.employeeId === this.selectedEmployee.employeeId)
          if (indexToDelete >= 0) {
            this.getAllEmployees.splice(indexToDelete, 1)
          }
        }
        
        this.modals.mini = false;
      },
      async fetchEmployeeData() {
        await this.fetchEmployees();
      },
    },
    async created() {
      await this.fetchEmployeeData();
      // this.$nextTick(()=>{
        console.log('Empleados en el componente: ',this.getAllEmployees);
      // })
      
    }
  }
</script>
<style lang="scss">
  .el-table .td-actions{
  button.btn {
    margin-right: 5px;
  }
  }
</style>
