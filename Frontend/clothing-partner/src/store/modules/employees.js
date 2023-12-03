import axios from 'axios';

const employeesModule = {
  namespaced: true,
    state: {
      employees: [],
      // Otros estados relacionados con empleados
    },
    mutations: {
      SET_EMPLOYEES(state, employees) {
        console.log('En el mutation/set_employees: ',employees);
        state.employees = employees;
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
            return true;
          }
          else {
            return false;
          }
        } catch (error) {
          console.error('Error deleting', error)
        }
      }
      // Otras acciones relacionadas con empleados
    },
    getters: {
      getAllEmployees: (state) => state.employees,
      // Otros getters relacionados con empleados
    },
  };
  
  export default employeesModule;