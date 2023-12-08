import axios from 'axios';

const employeesModule = {
  namespaced: true,
    state: {
      employees: [],
      selectedEmployee : {},
      // Otros estados relacionados con empleados
    },
    mutations: {
      SET_EMPLOYEES(state, employees) {
        console.log('En el mutation/set_employees: ',employees);
        state.employees = employees;
      },
      SET_SELECTED_EMPLOYEE(state, employee) {
        state.selectedEmployee = employee;
      },
      DELETE_EMPLOYEE(state, employeeId) {
        state.employees = state.employees.filter(employee => employee.employeeId !== employeeId);
      },
      UPDATE_EMPLOYEE(state, updatedEmployee) {
        const index = state.employees.findIndex(employee => employee.employeeId === updatedEmployee.employeeId);
        if (index !== -1) {
          // Update the existing employee with the new data
          state.employees.splice(index, 1, updatedEmployee);
        }
      },
      // Otras mutaciones relacionadas con empleados
    },
    actions: {
      async fetchEmployees( { rootState, commit } ) {
        //se accede al urlBae por medio del state del store raíz
        console.log('Entre a fetchEmployees en el store/employees')
        const apiUrl = rootState.apiBaseUrl;
        try {
          const headers = {Authorization:  `Bearer ${rootState.token}` };
          const employeeUrl = `${apiUrl}Employee/employee-get-all`;
          const response = await axios.get(employeeUrl, {headers});
          
          if(response.status === 200){
            const data = response.data.data;
            commit('SET_EMPLOYEES', data);
            return {success: true, data};
          }
        } catch (error) {
          console.error('Error al obtener datos desde el API:', error);
          return { success: false, error};
        }
      },
      async deleteEmployee({ rootState, commit }, employee){
        console.log('Borrar en store: ', employee)
        const apiUrl = rootState.apiBaseUrl;
        try {
          //const loginUrl = `${state.apiBaseUrl}Auth/user-login?username=${encodeURIComponent(username)}&password=${encodeURIComponent(password)}`;
          const headers = {Authorization:  `Bearer ${rootState.token}` };
          const employeeUrl = `${apiUrl}Employee/employee-delete?employeeId=${employee.employeeId}`;
          const response = await axios.delete(employeeUrl, {headers});
          if(response.status === 200){
            commit('DELETE_EMPLOYEE', employee.employeeId);
            return true;
          }
          else {
            return false;
          }
        } catch (error) {
          console.error('Error deleting', error)
        }
      },
      async getEmployeeById({ rootState, commit }, employee){
        console.log('Editar en store: ', employee)
        const apiUrl = rootState.apiBaseUrl;
        try {
          if(employee.employeeId === 0){
            console.log('viene el objeto vacío');
            commit('SET_SELECTED_EMPLOYEE', employee);
            return true;
          }
          //const loginUrl = `${state.apiBaseUrl}Auth/user-login?username=${encodeURIComponent(username)}&password=${encodeURIComponent(password)}`;
          const headers = {Authorization:  `Bearer ${rootState.token}` };
          const employeeUrl = `${apiUrl}Employee/employee-get-by-id?Id=${employee.employeeId}`;
          const response = await axios.get(employeeUrl, {headers});
          console.log('Data en Response de getEmployeeById: ', response)
          if(response.status === 200){
            console.log('modifica el empleado en el store', response.data.data);
            commit('SET_SELECTED_EMPLOYEE', response.data.data);
            return true;
          }
          else {
            return false;
          }
        } catch (error) {
          console.error('Error, employee not found !!', error)
        }
      },
      async updateEmployee({ rootState, commit }, employee){
        const apiUrl = rootState.apiBaseUrl;
        console.log('Employee to be updated: ', employee);
        try {
          const employeeId = employee.employeeId;
          console.log('Id del empleado a modificar',employeeId);
          employee = {
            ...employee,
            recentPicture : "prueba.png",
            password: "string",
            departmentId : employee.department.departmentId,
            designationId : employee.designation.designationId,
            teamId : employee.team.teamId
          };
          console.log('Employee a modificar: ',employee); 
          const headers = {Authorization: `Bearer ${rootState.token}` };
          const employeeUrl = `${apiUrl}Employee/employee-update?employeeId=${employeeId}`;
          const response = await axios.put(employeeUrl, employee, {headers});
          if(response.status === 200){
            commit('UPDATE_EMPLOYEE', employee.employeeId);
            return true
          } else { 
            return false
          }
        } catch (error) {
          console.error('Error Updating !!!', error);
          return false
        }
      },
      async addEmployee({ rootState, commit }, employee){
        const apiUrl = rootState.apiBaseUrl;
        console.log('Employee to be added: ', employee);
        try {
          employee.departmentId = 1;
          employee.designationId = 1;
          employee.teamId = 1;
          employee.password = "1740523Ampa$";
          const headers = {Authorization: `Bearer ${rootState.token}` };
          const employeeUrl = `${apiUrl}Employee/employee-add`;
          const response = await axios.post(employeeUrl, employee, {headers});
          if(response.status === 200){
            //commit('UPDATE_EMPLOYEE', employee.employeeId);
            return true
          } else { 
            return false
          }
        } catch (error) {
          console.error('Error Adding !!!', error);
          return false
        }
      }
      // Otras acciones relacionadas con empleados
    },
    getters: {
      getAllEmployees: (state) => state.employees,
      getSelectedEmployee: (state) => state.selectedEmployee,
      // Otros getters relacionados con empleados
    },
  };
  
  export default employeesModule;