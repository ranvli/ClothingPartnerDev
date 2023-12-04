<template>
  <div class="row">
    <div class="col-lg-4 col-md-5">
      <employee-card>

      </employee-card>
      <employee-members-card>

      </employee-members-card>
    </div>
    <div class="col-lg-8 col-md-7">
      <employee-edit-profile-form>

      </employee-edit-profile-form>
    </div>
  </div>
</template>
<script>
  import EmployeeEditProfileForm from './EmployeeProfile/EmployeeEditProfileForm.vue' 
  import EmployeeCard from './EmployeeProfile/EmployeeCard.vue'
  import EmployeeMembersCard from './EmployeeProfile/EmployeeMembersCard.vue'

  export default {
    components: {
      EmployeeEditProfileForm,
      EmployeeCard,
      EmployeeMembersCard
    }
  }

</script>
<style>

</style>
