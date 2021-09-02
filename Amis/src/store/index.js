import Vue from 'vue'
import Vuex from 'vuex'
import * as data from './data'
import axios from 'axios'
Vue.use(Vuex)

export const store = new Vuex.Store({
  state: {
    URL_API: "https://localhost:44334/api/v1/",
    // mode = 0 => add new 
    // mode = 1 => edit 
    MODE: 0,
    // employee list
    employees: [],
    // employee data
    employee: {
      employeeCode: null,
      fullName: null,
      dateOfBirth: null,
      gender: 0,
      departmentId: null,
      identityNumber: null,
      identityDate: null,
      identityPlace: null,
      positionName: null,
      address: null,
      hotline: null,
      phoneNumber: null,
      email: null,
      bankName: null,
      accountNumber: null,
      branchName: null,
    },
    // employee id
    employeeId: "",
    // status open form  
    isOpen: false,
    // new employee code 
    newCode: "",

    // page index
    pageIndex: 1,
    // page numbers
    pageNumber: 0,
    // key search
    keysearch: "",
    // options for page size
    optionsPaging: data.pagingOptions,
    // option for page size = 10
    currentPaging: data.pagingOptions[0],
    // expand menu
    expand: false,

    // department list
    departments: [],
    // status reload table 
    isLoading: false,

    totalRecord : 0,

  },

  getters: {
    getTotalRecord: state => state.totalRecord,
    // get paage number
    getPageNumber: state => state.pageNumber,
    // get clone
    getClone: state => state.clone,
    // get api
    getAPI: state => state.URL_API,
    // get newCOde
    getNewCode: state => state.newCode,
    // get status open form 
    getIsOpen: state => state.isOpen,
    // get employee
    getEmployee: state => state.employee,
    // get mode 
    getMode: state => state.MODE,
    // get current option paging
    getCurrentPaging: state => state.currentPaging,
    // list options paging
    getOptionsPaging: state => state.optionsPaging,
    // get status expand menu
    getExpand: state => state.expand,
    // get departments
    getDepartments: state => state.departments,
    // get employees list
    getEmployees: state => state.employees,
    // get keysearch
    getKeysearch: state => state.keysearch,
    // get status loading
    getIsLoading: state => state.isLoading,
    // get employee id
    getEmployeeId: state => state.employeeId,
  },

  mutations: {
    SET_TOTAL_RECORD(state, payload){
      state.totalRecord = payload;
    } , 
    /**
     * set page number
     * Author : LP(1/9) 
     */
    SET_PAGE_NUMBER(state, payload) {
      state.pageNumber = payload;
    },

    /**
     * set page index
     * Author : LP(1/9) 
     */
    SET_PAGE_INDEX(state, payload) {
      state.pageIndex = payload;
    },

    /**
     * Reset employee after add or edit 
     * AUthor : LP(30/8)
     */
    SET_NULL_EMPLOYEE(state) {
      state.employee = {
        employeeCode: null,
        fullName: null,
        dateOfBirth: null,
        gender: 0,
        departmentId: null,
        identityNumber: null,
        identityDate: null,
        identityPlace: null,
        positionName: null,
        address: null,
        hotline: null,
        phoneNumber: null,
        email: null,
        bankName: null,
        accountNumber: null,
        branchName: null,
      };
    },

    /**
     * set departments
     * Author : LP(29/8)
     */
    SET_DEPARTMENTS(state, payload) {
      state.departments = payload;
    },

    /**
     * set newCode
     * Author : LP(28/8)
     */
    SET_NEW_CODE(state, payload) {
      state.newCode = payload;
    },

    /**
     * set isOpen
     * Author : LP(29/8)
     */
    SET_IS_OPEN(state, payload) {
      state.isOpen = payload;
    },

    /**
     * Set mode
     * Author : LP(29/8)
     */
    SET_MODE(state, payload) {
      state.MODE = payload;
    },

    /**
     * Set employee
     * Author : LP(29/8) 
     */
    SET_EMPLOYEE(state, payload) {
      state.employee = payload;
      delete state.employee.createdBy;
      delete state.employee.createdDate;
      delete state.employee.modifiedBy;
      delete state.employee.modifiedDate;
      delete state.employee.genderName;
      delete state.employee.departmentName;
    },

    /**
     * set id employee
     * Author : LP(29/8)
     */
    SET_EMPLOYEE_ID(state, payload) {
      state.employeeId = payload;
    },

    /**
     * Choose option paginations
     * Author : LP(28/8)
     */
    SET_PAGING_OPTION(state, index) {
      state.currentPaging = state.optionsPaging[index];
      // this.setEmployees();
    },
    /**
     * Set toggle menu
     * Author : LP(28/8)
     */
    SET_TOGGLE_MENU(state) {
      state.expand = !state.expand;
    },

    /**
     * Set employee 
     * Author : LP(28/8)
     */
    SET_EMPLOYEES(state, payload) {
      state.employees = payload;
    },

    /**
     * Set keysearch
     * Author : LP(28/8)
     */
    SET_KEYSEARCH(state, payload) {
      state.keysearch = payload;
    },


  },

  actions: {
    /**
     * Get employees list by paging
     * @param {*} context 
     * Author : LP(28/8)
     */
    setEmployees: context => {
      const pageIndex = context.state.pageIndex;
      const pageSize = context.state.currentPaging.PageSize;
      const keysearch = context.state.keysearch;
      try {
        context.state.isLoading = true;
        axios.get(context.state.URL_API + `Employees/Paging?keyword=${keysearch}&pageIndex=${pageIndex}&pageSize=${pageSize}`).then(async function (response) {
          let result = response.data;
          let employees = [];
          // console.log(result.data.data);
          if (result.data != null) {
            employees = result.data.data;
            context.commit("SET_TOTAL_RECORD", result.data.totalRecord);
            context.commit("SET_PAGE_NUMBER", result.data.totalPage);
            context.commit("SET_EMPLOYEES", employees);
            
          }else{
            context.commit("SET_EMPLOYEES", []);
          }
          context.state.isLoading = false;

        }).catch(error => {
          context.state.isLoading = true;
          console.log("get employee error " + error);
        })
      } catch (error) {
        context.state.isLoading = true;
        console.log("get employee error " + error);
      }

    },

    /**
     * Get employee by id
     * @param {*} context
     * Author : LP(29/8) 
     */
    setEmployee({ dispatch, commit, state }) {
      try {
        const id = state.employeeId;
        axios.get(state.URL_API + `Employees/${id}`).then(async function (response) {
          let result = response.data;
          if (result.data != null) {
            dispatch("setDepartments");
            commit("SET_EMPLOYEE", result.data); // gán dữ liệu cho employee
            commit("SET_IS_OPEN", true); // mở form
          }
        }).catch(error => {
          console.log("get employee by id error " + error);
        })
      } catch (error) {
        console.log("get employee by id error " + error);
      }
    },

    /**
     * Get new employee code
     * Author : LP(28/8)
     */
    async setNewCode({ dispatch, commit, state }) {
      try {
        axios.get(state.URL_API + `Employees/NewEmployeeCode`).then(async function (response) {
          let result = response.data;
          if (result.data != null) {
            // gán mã cho nhân viên
            state.employee.employeeCode = result.data;
            // gán mã 
            await commit("SET_NEW_CODE", result.data);
            // get list departments
            await dispatch("setDepartments");
            // reset form
            await commit("SET_EMPLOYEE", state.employee);
            if (state.MODE == 2) {// nếu mode = 2 => nhân bản dữ liệu
              await dispatch("setEmployee"); // lấy dữ liệu theo id => gán vào employee
            } else { // mode != 2 (thêm hoặc sửa nhân viên) => mở form 
              await commit("SET_IS_OPEN", true);
            }

          }
        }).catch(error => {
          console.log("get new employee code error " + error);
        })
      } catch (error) {
        console.log("get new employee code error " + error);
      }
    },

    /**
     * set departments
     * Author : LP(29/8)
     */
    setDepartments: context => {
      try {
        axios.get(context.state.URL_API + `Departments`).then(async function (response) {
          let result = response.data;
          if (result.data != null) {
            context.commit("SET_DEPARTMENTS", result.data);
          }
        }).catch(error => {
          console.log("get departments error " + error);
        })
      } catch (error) {
        console.log("get departments error " + error);
      }
    },
  }
})
