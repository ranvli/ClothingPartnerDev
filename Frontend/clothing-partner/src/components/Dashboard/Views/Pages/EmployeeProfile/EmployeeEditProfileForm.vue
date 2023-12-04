<template>
  <div class="card">
    <div class="card-header">
      <h4 class="title">Employee Profile</h4>
    </div>
    <div class="card-body">
      <form>
        <div class="row">
          <div class="col-md-2">
            <fg-input type="number"
                      label="Id"
                      :disabled="true"
                      placeholder="Employee Id"
                      v-model="employeeToEdit.employeeId">
            </fg-input>
          </div>
          <div class="col-md-3">
            <fg-input type="text"
                      label="Username"
                      placeholder="Username"
                      v-model="employeeToEdit.username">
            </fg-input>
          </div>
          <div class="col-md-7">
            <fg-input type="text"
                      label="Employee Full Name"
                      placeholder="Full Name"
                      v-model="employeeToEdit.fullName">
            </fg-input>
          </div>
        </div>
        <div class="row">
          
          <div class="col-md-6">
            <fg-input type="email"
                      label="Company Email"
                      placeholder="Email"
                      v-model="employeeToEdit.companyEmail">
            </fg-input>
          </div>
          <div class="col-md-6">
            <fg-input type="text"
                      label="Personal Email"
                      placeholder="Personal Email"
                      v-model="employeeToEdit.personalEmail">
            </fg-input>
          </div>
        </div>

        <div class="row">
          <div class="col-md-6">
            <fg-input type="text"
                      label="Recent Address"
                      placeholder="Recent Address"
                      v-model="employeeToEdit.recentAddress">
            </fg-input>
          </div>
          <div class="col-md-6">
            <fg-input type="text"
                      label="Permanent Address"
                      placeholder="Permanent Address"
                      v-model="employeeToEdit.permanentAddress">
            </fg-input>
          </div>
        </div>

        <div class="row">
          
          <div class="col-md-6">
            <fg-input type="text"
                      label="Department"
                      placeholder="Department"
                      v-model="employeeToEdit.department">
            </fg-input>
          </div>
          <div class="col-md-6">
            <fg-input type="text"
                      label="Designation"
                      placeholder="Designation"
                      v-model="employeeToEdit.designation">
            </fg-input>
          </div>
        </div>

        <div class="row">
          <div class="col-md-4">
            <fg-input type="text"
                      label="Team"
                      placeholder="Team"
                      v-model="employeeToEdit.team">
            </fg-input>
          </div>
          <div class="col-md-4">
            <fg-input type="text"
                      label="Phone"
                      placeholder="Phone"
                      v-model="employeeToEdit.phone">
            </fg-input>
          </div>
          <div class="col-md-4">
            <fg-input type="text"
                      label="Emergency Phone"
                      placeholder="Emergency Phone"
                      v-model="employeeToEdit.emergencyPhone">
            </fg-input>
          </div>
        </div>

        <div class="row">
          <div class="col-md-6">
            <fg-input type="text"
                    label="Date of Birth"
                    placeholder="Date of Birth"
                    v-model="employeeToEdit.dateOfBirth">
          </fg-input>
          </div>
          <div class="col-md-6">
            <fg-input type="text"
                      label="Date of Joining"
                      placeholder="Date of Joining"
                      v-model="employeeToEdit.dateOfJoining">
            </fg-input>
          </div>
        </div>

       <div class="row">
          <div class="col-md-4">
            <fg-input type="text"
                    label="Voter Id"
                    placeholder="Voter Id"
                    v-model="employeeToEdit.voterId">
          </fg-input>
          </div>
          <div class="col-md-4">
            <fg-input type="text"
                      label="Driving License"
                      placeholder="Driving License"
                      v-model="employeeToEdit.drivingLicense">
            </fg-input>
          </div>
          <div class="col-md-4">
            <fg-input type="text"
                      label="Passport Number"
                      placeholder="Passsport Number"
                      v-model="employeeToEdit.passportNumber">
            </fg-input>
          </div>
        </div>

        <div class="row">
          <div class="col-md-6">
            <div class="form-group">
              <fg-input type="text"
                  label="Salary Bank Account"
                  placeholder="Salary Bank Account"
                  v-model="employeeToEdit.salaryBankAccount">
            </fg-input>
            </div>
          </div>
          <div class="col-md-6">
            <fg-input type="text"
                label="Last Company NOC"
                placeholder="Last Company NOC"
                v-model="employeeToEdit.lastCompanyNOC">
            </fg-input>
          </div>
        </div>
        <div class="row">
          <div class="col-md-6">
            <div class="form-group">
              <fg-input type="text"
                  label="WeChat Id"
                  placeholder="WeChat Id"
                  v-model="employeeToEdit.weChatId">
            </fg-input>
            </div>
          </div>
          <div class="col-md-6">
            <fg-input type="text"
                label="Whatsapp Number"
                placeholder="Whatsapp Number"
                v-model="employeeToEdit.whatsappNumber">
            </fg-input>
          </div>
        </div>
        <div class="row">
          <div class="col-md-6">
            <div class="form-group">
              <fg-input type="text"
                  label="Blood Group"
                  placeholder="Blood Group"
                  v-model="employeeToEdit.bloodGroup">
            </fg-input>
            </div>
          </div>
          
        </div>

        <div class="text-center">
         <!--  <button type="submit" class="btn btn-info btn-fill btn-wd" @click.prevent="updateProfile">
            Update Profile
          </button> -->
          <p-button v-if="employeeToEdit && Object.keys(employeeToEdit).length > 0" type="success" link @click="handleSubmit">Update Profile</p-button>
          <p-button v-else type="success" link @click="handleAdd">Add Employee</p-button>
          <p-button type="danger" link @click="handleCancel">Cancel Changes</p-button>
        </div>
        <div class="clearfix"></div>
      </form>
    </div>
  </div>
</template>
<script>
import { mapState, mapActions } from 'vuex'

  export default {
    data () {
      return {
        // user: {
        //   company: 'Paper Dashboard',
        //   username: 'michael23',
        //   email: '',
        //   lastName: 'Faker',
        //   address: 'Melbourne, Australia',
        //   city: 'melbourne',
        //   postalCode: '',
        //   aboutMe: `Oh so, your weak rhyme. You doubt I'll bother, reading into it.I'll probably won't, left to my own devicesBut that's the difference in our opinions.`
        // }
        employeeToEdit : {},
      }
    },
    computed : {
      ...mapState('employees',['selectedEmployee']),
    },
    watch: {
      selectedEmployee: {
        handler(newVal) {
          this.employeeToEdit = { ...newVal };
        },
        immediate: true, // to be executed imediatly on component creation
      },
    },
    methods: {
      ...mapActions('employees',['updateEmployee']),
      updateProfile () {
        alert('Your data: ' + JSON.stringify(this.selectedEmployee));
      },
      handleCancel () {
        this.$router.push({name: 'Paginated Tables'})
      },
      handleSubmit() {
        const result = this.updateEmployee(this.employeeToEdit);
        console.log('Resultado del update: ',result);
        if(result===true){
          this.$router.push({name: 'Paginated Tables'})
        }
        // alert('Your data: ' + JSON.stringify(this.employeeToEdit))
      },
      handleAdd() {
        console.log('Agregar');
      },
    }
  }

</script>
<style>

</style>
