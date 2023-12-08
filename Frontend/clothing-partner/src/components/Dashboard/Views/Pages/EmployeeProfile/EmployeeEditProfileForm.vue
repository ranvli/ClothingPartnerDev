<template>
  <ValidationObserver v-slot="{ handleSubmit }">
    <div class="card">
      <div class="card-header">
        <h4 class="title">Employee Profile</h4>
      </div>
      <div class="card-body" v-if="employeeToEdit">
        <form @submit.prevent="handleSubmit(submit)">
          <div class="row">
            <div class="col-md-2">
              <fg-input
                type="number"
                label="Id"
                :disabled="true"
                placeholder="Employee1 Id"
                v-model="employeeToEdit.employeeId"
              >
              </fg-input>
            </div>
            <div class="col-md-3">
              <fg-input
                type="text"
                label="Username"
                placeholder="Username"
                v-model="employeeToEdit.username"
              >
              </fg-input>
            </div>
            <div class="col-md-7">
              <ValidationProvider
                name="fullName"
                rules="required"
                v-slot="{ passed, failed }"
              >
                <fg-input
                  type="text"
                  :error="failed ? 'Full Name is required' : null"
                  :hasSuccess="passed"
                  label="Employee Full Name"
                  placeholder="Full Name"
                  name="fullName"
                  v-model="employeeToEdit.fullName"
                >
                </fg-input>
              </ValidationProvider>
            </div>
          </div>
          <div class="row">
            <div class="col-md-6">
              <ValidationProvider
                name="email"
                rules="required|email"
                v-slot="{ passed, failed }"
              >
                <fg-input
                  type="email"
                  :error="failed ? 'Email is required' : null"
                  :hasSuccess="passed"
                  name="email"
                  label="Company Email"
                  placeholder="Email"
                  v-model="employeeToEdit.companyEmail"
                >
                </fg-input>
              </ValidationProvider>
            </div>
            <div class="col-md-6">
              <ValidationProvider
                name="personalEmail"
                rules="required|email"
                v-slot="{ passed, failed }"
              >
                <fg-input
                  type="email"
                  name="personalEmail"
                  :error="failed ? 'Personal Email is required' : null"
                  :hasSuccess="passed"
                  label="Personal Email"
                  placeholder="Personal Email"
                  v-model="employeeToEdit.personalEmail"
                >
                </fg-input>
              </ValidationProvider>
            </div>
          </div>

          <div class="row">
            <div class="col-md-6">
              <ValidationProvider
                name="recentAddress"
                rules="required"
                v-slot="{ passed, failed }"
              >
                <fg-input
                  type="text"
                  name="reecentAddress"
                  :error="failed ? 'Recent Address is required' : null"
                  :hasSuccess="passed"
                  label="Recent Address"
                  placeholder="Recent Address"
                  v-model="employeeToEdit.recentAddress"
                >
                </fg-input>
              </ValidationProvider>
            </div>
            <div class="col-md-6">
              <ValidationProvider
                name="permanentAddress"
                rules="required"
                v-slot="{ passed, failed }"
              >
                <fg-input
                  name="permanentAddress"
                  :error="failed ? 'Permanent Address Required' : null"
                  :hasSuccess="passed"
                  type="text"
                  label="Permanent Address"
                  placeholder="Permanent Address"
                  v-model="employeeToEdit.permanentAddress"
                >
                </fg-input>
              </ValidationProvider>
            </div>
          </div>

          <div class="row">
            <div class="col-md-6">
              <fg-input
                type="text"
                label="Department"
                placeholder="Department"
                v-model="employeeToEdit.department.departmentId"
              >
              </fg-input>
            </div>
            <div class="col-md-6">
              <fg-input
                type="text"
                label="Designation"
                placeholder="Designation"
                v-model="employeeToEdit.designation.designationId"
              >
              </fg-input>
            </div>
          </div>

          <div class="row">
            <div class="col-md-4">
              <fg-input
                type="text"
                label="Team"
                placeholder="Team"
                v-model="employeeToEdit.team.teamId"
              >
              </fg-input>
            </div>
            <div class="col-md-4">
              <ValidationProvider
                name="phone"
                rules="required|numeric"
                v-slot="{ passed, failed }"
              >
                <fg-input
                  name="phone"
                  :error="failed ? 'Phone Number is required' : null"
                  :hasSuccess="passed"
                  type="text"
                  label="Phone"
                  placeholder="Phone"
                  v-model="employeeToEdit.phone"
                >
                </fg-input>
              </ValidationProvider>
            </div>
            <div class="col-md-4">
              <ValidationProvider
                name="emergencyPhone"
                rules="required|numeric"
                v-slot="{ passed, failed }"
              >
                <fg-input
                  type="text"
                  :error="failed ? 'Emergency Phone Number is required' : null"
                  :hasSuccess="passed"
                  label="Emergency Phone"
                  placeholder="Emergency Phone"
                  v-model="employeeToEdit.emergencyPhone"
                >
                </fg-input>
              </ValidationProvider>
            </div>
          </div>

          <div class="row">
            <div class="col-md-6">
              <fg-input
                type="text"
                label="Date of Birth"
                placeholder="Date of Birth"
                v-model="employeeToEdit.dateOfBirth"
              >
              </fg-input>
            </div>
            <div class="col-md-6">
              <fg-input
                type="text"
                label="Date of Joining"
                placeholder="Date of Joining"
                v-model="employeeToEdit.dateOfJoining"
              >
              </fg-input>
            </div>
          </div>

          <div class="row">
            <div class="col-md-4">
              <ValidationProvider
                name="voterId"
                rules="required"
                v-slot="{ passed, failed }"
              >
                <fg-input
                  type="text"
                  name="voterId"
                  :error="failed ? 'Voter Id is required' : null"
                  :hasSuccess="passed"
                  label="Voter Id"
                  placeholder="Voter Id"
                  v-model="employeeToEdit.voterId"
                >
                </fg-input>
              </ValidationProvider>
            </div>
            <div class="col-md-4">
              <ValidationProvider
                name="drivingLicense"
                rules="required"
                v-slot="{ passed, failed }"
              >
                <fg-input
                  type="text"
                  :error="failed ? 'Driving License is Required' : null"
                  :hasSuccess="passed"
                  name="drivingLicense"
                  label="Driving License"
                  placeholder="Driving License"
                  v-model="employeeToEdit.drivingLicense"
                >
                </fg-input>
              </ValidationProvider>
            </div>
            <div class="col-md-4">
              <ValidationProvider
                name="passportNumber"
                rules="required"
                v-slot="{ passed, failed }"
              >
                <fg-input
                  type="text"
                  name="passportNumber"
                  :error="failed ? 'Passport is Required' : null"
                  :hasSuccess="passed"
                  label="Passport Number"
                  placeholder="Passsport Number"
                  v-model="employeeToEdit.passportNumber"
                >
                </fg-input>
              </ValidationProvider>
            </div>
          </div>

          <div class="row">
            <div class="col-md-6">
              <div class="form-group">
                <ValidationProvider
                  name="salaryBankAccount"
                  rules="required"
                  v-slot="{ passed, failed }"
                >
                  <fg-input
                    type="text"
                    name="salaryBankAccount"
                    :error="failed ? 'Salary Bank Account is required' : null"
                    :hasSuccess="passed"
                    label="Salary Bank Account"
                    placeholder="Salary Bank Account"
                    v-model="employeeToEdit.salaryBankAccount"
                  >
                  </fg-input>
                </ValidationProvider>
              </div>
            </div>
            <div class="col-md-6">
              <ValidationProvider
                name="lastCompanyNOC"
                rules="required"
                v-slot="{ passed, failed }"
              >
                <fg-input
                  type="text"
                  name="lastCompanyNOC"
                  :error="failed ? 'Last Company NOC is required' : null"
                  :hasSuccess="passed"
                  label="Last Company NOC"
                  placeholder="Last Company NOC"
                  v-model="employeeToEdit.lastCompanyNOC"
                >
                </fg-input>
              </ValidationProvider>
            </div>
          </div>
          <div class="row">
            <div class="col-md-6">
              <div class="form-group">
                <ValidationProvider
                  name="weChatId"
                  rules="required"
                  v-slot="{ passed, failed }"
                  ><fg-input
                    type="text"
                    name="weChatId"
                    :error="failed ? 'WeChat Id is required' : null"
                    :hasSuccess="passed"
                    label="WeChat Id"
                    placeholder="WeChat Id"
                    v-model="employeeToEdit.weChatId"
                  >
                  </fg-input
                ></ValidationProvider>
              </div>
            </div>
            <div class="col-md-6">
              <ValidationProvider
                name="whatsAppNumber"
                rules="required"
                v-slot="{ passed, failed }"
                ><fg-input
                  type="text"
                  name="whatsAppNumber"
                  :error="failed ? 'Whatsapp Number is required' : null"
                  :hasSuccess="passed"
                  label="Whatsapp Number"
                  placeholder="Whatsapp Number"
                  v-model="employeeToEdit.whatsappNumber"
                >
                </fg-input>
              </ValidationProvider>
            </div>
          </div>
          <div class="row">
            <div class="col-md-6">
              <div class="form-group">
                <fg-input
                  type="text"
                  label="Blood Group"
                  placeholder="Blood Group"
                  v-model="employeeToEdit.bloodGroup"
                >
                </fg-input>
              </div>
            </div>
            <div class="col-md-6">
              <div class="form-group">
                <fg-input
                  type="text"
                  label="Employee Picture"
                  placeholder="Employee Picture"
                  v-model="employeeToEdit.recentPicture"
                >
                </fg-input>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-6">
              <ValidationProvider
                vid="confirmation"
                name="password"
                rules="required"
                v-slot="{ passed, failed }"
              >
                <fg-input
                  type="password"
                  name="password"
                  :error="failed ? 'The Password field is required' : null"
                  :hasSuccess="passed"
                  label="Password"
                  placeholder="Password"
                  v-model="employeeToEdit.password"
                >
                </fg-input>
              </ValidationProvider>
            </div>
            <div class="col-md-6">
              <ValidationProvider
                name="passwordConfirm"
                rules="required|confirmed:confirmation"
                v-slot="{ passed, failed }"
              >
                <fg-input
                  type="password"
                  name="passwordConfirm"
                  :error="failed ? 'Password does not match' : null"
                  :hasSuccess="passed"
                  label="Confirm Password"
                  placeholder="Confirm Password"
                  v-model="employeeToEdit.passwordCondirm"
                >
                </fg-input>
              </ValidationProvider>
            </div>
          </div>
          <div class="text-center">
            <!--  <button type="submit" class="btn btn-info btn-fill btn-wd" @click.prevent="updateProfile">
            Update Profile
          </button> -->
            <p-button
              v-if="employeeToEdit.employeeId !== 0"
              type="success"
              link
              native-type="submit"
              >Update Profile</p-button
            >
            <p-button v-else type="success" link native-type="submit"
              >Add Employee</p-button
            >
            <p-button type="danger" link @click="handleCancel"
              >Cancel Changes</p-button
            >
          </div>
          <div class="clearfix"></div>
        </form>
      </div>
    </div>
  </ValidationObserver>
</template>
<script>
import { mapState, mapActions } from "vuex";
import { required, email, numeric, confirmed } from "vee-validate/dist/rules";
import { extend } from "vee-validate";

extend("email", email);
extend("required", required);
extend("numeric", numeric);
extend("confirmed", confirmed);

export default {
  data() {
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
      employeeToEdit: {},
    };
  },
  computed: {
    ...mapState("employees", ["selectedEmployee"]),
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
    ...mapActions("employees", ["updateEmployee", "addEmployee"]),
    updateProfile() {
      alert("Your data: " + JSON.stringify(this.selectedEmployee));
    },
    handleCancel() {
      this.$router.push({ name: "Paginated Tables" });
    },
    submit() {
      // alert('Your data: ' + JSON.stringify(this.employeeToEdit))

      console.log("Employee en handleSubmit", this.employeeToEdit);
      if (this.employeeToEdit.employeeId !== 0) {
        this.updateEmployee(this.employeeToEdit).then((response) => {
          if (response) {
            this.$router.push({ name: "Paginated Tables" });
          } else {
            console.log("Update Failed", response);
          }
        });
      } else {
        this.addEmployee(this.employeeToEdit).then((response) => {
          if (response) {
            this.$router.push({ name: "Paginated Tables" });
          } else {
            console.log("Add Failed", response);
          }
        });
      }
    },
  },
};
</script>
<style></style>
