<template>
  <div class="MISAEmployee">
    <div class="d-flex py-24">
      <span class="title">{{ title }}</span>
      <button class="MISABtn MISASucess ml-auto" @click="addEmployee">
        Thêm mới nhân viên
      </button>
    </div>
    <div class="content bg-white">
      <div class="top-control d-flex ml-auto align-items-c p-16">
        <div class="MISAInputSearch d-flex">
          <input
            type="text"
            v-model="keysearch"
            v-on:keyup.enter="
              isSearching = true;
              loadEmployees();
            "
            placeholder="Tìm theo mã, tên nhân viên"
          />
          <div class="search-icon"></div>
        </div>
        <button
          class="reload-icon"
          title="Lấy lại dự liệu"
          @click="
            isReload = true;
            loadEmployees();
          "
        ></button>
        <button
          class="export-icon"
          title="Xuất ra excel"
          @click="exportData"
        ></button>
      </div>
      <div
        class="MISATable"
        style="overflow-x: scroll; display: block; margin: 0 16px"
      >
        <table>
          <thead>
            <tr>
              <th style="position: sticky; left: 0">
                <input type="checkbox" />
              </th>
              <th style="position: sticky; left: 46px">Mã nhân viên</th>
              <th style="position: sticky; left: 196px; white-space: nowrap">
                Tên nhân viên
              </th>
              <th>Giới tính</th>
              <th class="t-c">Ngày sinh</th>
              <th>Số CMND</th>
              <th>Chức danh</th>
              <th>Tên đơn vị</th>
              <th>Số tài khoản</th>
              <th>Tên ngân hàng</th>
              <th>Chi nhánh TK ngân hàng</th>
              <th class="t-c">Chức năng</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="(employee, index) in getEmployees"
              :key="employee.EmployeeId"
            >
              <td style="position: sticky; left: 0">
                <input type="checkbox" name="" id="" />
              </td>
              <td style="position: sticky; left: 46px">
                {{ employee.employeeCode }}
              </td>
              <td style="position: sticky; left: 196px; white-space: nowrap">
                {{ employee.fullName }}
              </td>
              <td>{{ employee.genderName }}</td>
              <td class="t-c">{{ employee.dateOfBirth | formatDOB }}</td>
              <td>{{ employee.identityNumber }}</td>
              <td class="hide-txt" :title="employee.positionName">
                {{ employee.positionName }}
              </td>
              <td>{{ employee.departmentName }}</td>
              <td>{{ employee.accountNumber }}</td>
              <td>{{ employee.bankName }}</td>
              <td>{{ employee.branchName }}</td>
              <td class="t-c">
                <Context
                  @edit="editEmployee"
                  @clone="cloneEmployee"
                  @delete="
                    openConfirmDialog(
                      employee.employeeCode,
                      employee.employeeId
                    )
                  "
                  :id="employee.employeeId"
                  :nth="
                    (index == getEmployees.length - 1 &&
                      getEmployees.length >= 10) ||
                    (index == getEmployees.length - 2 &&
                      getEmployees.length >= 10)
                      ? 'last'
                      : ''
                  "
                ></Context>
              </td>
            </tr>
            <tr v-if="getEmployees.length > 0 ? false : true" class="t-c">
              <td colspan="12">Hiện tại không có dữ liệu nào</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="footer bg-white" style="padding: 0 16px">
      <div class="MISAPagination d-flex align-items-c">
        <span class="total-record"
          >Tổng số: <span class="bold">{{ getTotalRecord }}</span> bản ghi</span
        >
        <div class="ml-auto"></div>
        <Dropdown></Dropdown>
        <paginate
          v-if="getPageNumber > 0"
          :page-count="getPageNumber"
          :page-range="3"
          :margin-pages="1"
          :prev-text="'Trước'"
          :next-text="'Sau'"
          :container-class="'pagination'"
          :page-class="'page-item'"
          :prev-link-class="'btn-pre'"
          :next-link-class="'btn-next'"
          :click-handler="choose"
          ref="paging"
        >
        </paginate>
      </div>
    </div>
    <!-- getIsOpen -->
    <EmployeeDetail
      v-if="getIsOpen"
      @resultForm="hanldeResultForm"
    ></EmployeeDetail>
    <!-- getIsLoading -->
    <Loading v-if="getIsLoading"></Loading>
    <Dialog
      :type="'confirm'"
      v-if="showDialog"
      :content="contentDialog"
      @close="showDialog = false"
      @do="deleteEmployee(id)"
    ></Dialog>
  </div>
</template>

<script>
import Dialog from "../components/base/BaseDialog.vue";
import Context from "../components/base/BaseContext.vue";
import Dropdown from "../components/base/BaseOptionsPaging.vue";
import EmployeeDetail from "./EmployeeDetail.vue";
import { mapGetters, mapActions, mapMutations } from "vuex";
import Common from "../Common";
import Loading from "../components/base/BaseLoading.vue";
import axios from "axios";
import EventBus from "../EventBus";
export default {
  components: {
    EmployeeDetail,
    Dropdown,
    Loading,
    Context,
    Dialog,
  },
  data() {
    return {
      title: "Nhân viên",
      show: false,
      keysearch: "",
      showDialog: false,
      contentDialog: "",
      id: "",
      isReload: false,
      isSearching: false,
    };
  },
  async created() {
    // load dữ liệu ban đầu pageIndex = 1, pageSize = 10
    this.loadEmployees();
    this.$store.dispatch("setDepartments");

    // bắt sự kiện chọn pageSize
    EventBus.$on("chooseOptionPaging", () => {
      // active trang 1
      this.$refs.paging.selectFirstPage();
      // load lại dữ liệu
      this.$store.dispatch("setEmployees");
    });
  },
  computed: {
    ...mapGetters([
      "getEmployees",
      "getKeysearch",
      "getIsLoading",
      "getIsOpen",
      "getEmployee",
      "getAPI",
      "getPageNumber",
      "getTotalRecord",
    ]),
  },
  filters: {
    /**
     * Format date of birth
     * Author : LP(28/8)
     */
    formatDOB: function (value) {
      return Common.formatDateDDMMYYYY(value);
    },
  },
  watch: {
    keysearch() {
      // set keysearch
      this.SET_KEYSEARCH(this.keysearch);
    },
  },
  methods: {
    ...mapActions([
      "setEmployees",
      "setEmployee",
      "setDepartments",
      "setNewCode",
    ]),
    ...mapMutations([
      "SET_KEYSEARCH",
      "SET_EMPLOYEE_ID",
      "SET_MODE",
      "SET_IS_OPEN",
      "SET_PAGE_INDEX",
    ]),

    /**
     * Choose paging
     * Author : LP(1/9)
     */
    choose(index) {
      // chọn thứ tự trang
      this.SET_PAGE_INDEX(index);
      this.$store.dispatch("setEmployees");
    },

    /**
     * Open confirm dialog
     * Author : LP(1/9)
     */
    openConfirmDialog(code, id) {
      this.id = id;
      this.showDialog = true;
      this.contentDialog = `Bạn có thực sự muốn xóa Nhân viên có mã nhân viên là <${code}> không?`;
    },

    /**
     * Handle result form
     * Author : LP(29/8)
     */
    hanldeResultForm(value, type) {
      // loging trạng thái từ api hiển thị người dùng
      switch (value.data.resultCode) {
        case 200:
          this.$toast.success(value.data.userMessage);
          break;
        case 400:
          this.$toast.info(value.data.userMessage);
          break;
        case 500:
          this.$toast.error(value.data.userMessage);
          break;
        case 401:
          this.$toast.info(value.data.userMessage);
          break;
        case 201:
          this.$toast.succes(value.data.userMessage);
          break;
        case 204:
          this.$toast.info(value.data.userMessage);
      }
      // nếu type = 2 => người dùng bấm cất và thêm
      if (type == 1) {
        this.SET_IS_OPEN(false);
      }
      // quay lại trang 1
      this.$refs.paging.selectFirstPage();
      // tải lại dữ liệu
      this.$store.dispatch("setEmployees");
    },

    /**
     * To add data
     */
    addEmployee() {
      // set id empty when click btn add
      // this.setEmployeeId("");
      // set mode = 0 to add
      this.SET_MODE(0);
      // get new code => lấy mã + gán employee + mở form
      this.$store.dispatch("setNewCode");
    },

    /**
     * To delete
     * Author : LP(28/8)
     */
    deleteEmployee(id) {
      // đóng dialog nếu confirm = true
      this.showDialog = false;
      // xóa
      try {
        let self = this;
        axios.delete(this.getAPI + `Employees/${id}`).then(function (response) {
          self.hanldeResultForm(response);
        });
      } catch (error) {
        console.log("delete employee error " + error);
      }
    },

    /**
     * Open form to clone data
     * Auhthor : LP(28/8)
     */
    cloneEmployee(id) {
      this.SET_EMPLOYEE_ID(id);
      this.SET_MODE(2);
      // get new code => lấy mã + gán nhân viên + mở form
      this.$store.dispatch("setNewCode");
    },

    /**
     * Open form to edit
     * Author : LP(29/8)
     */
    editEmployee(id) {
      this.SET_EMPLOYEE_ID(id);
      this.SET_MODE(1);
      this.$store.dispatch("setEmployee");
    },

    /**
     * Load employees
     * Author : LP(28/8)
     */
    loadEmployees() {
      // nếu bấm nút reload => trở về trang đầu
      if (this.isReload || this.isSearching) {
        this.$refs.paging.selectFirstPage();
      }
      this.$store.dispatch("setEmployees");
    },

    /**
     * Export data
     * Author : LP(31.8)
     */
    exportData() {
      try {
        let self = this;
        axios.get(this.getAPI + `Employees/Export`).then(function (response) {
          self.hanldeResultForm(response);
          window.location = response.data.data;
        });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Close form
     * Author : LP(28/8)
     */
    closeForm() {
      this.show = false;
    },
  },
};
</script>

<style scoped>
.pagination {
  display: flex;
}

.pagination .activeclass {
  border: 1px solid #e0e0e0;

  font-weight: 700;
}

.pagination .disabled {
  color: #a6a6a6;
}

.pagination li {
  list-style-type: none;
}

.page-item {
  padding-left: 8px;
  padding-right: 8px;
}

.btn-pre {
  padding-right: 7px;
}

.btn-next {
  padding-left: 7px;
}

.options select {
  height: 32px;
  border: 1px solid #babec5;
  outline: none;
  padding-left: 12px;
  border-radius: 2px;
  padding-right: 12px;
}
.MISAPagination {
  height: 46px;
}
table {
  border-collapse: separate;
  width: 100%;
  /* height: 492px; */
  height: calc(100vh - 72px - 46px - 48px - 66px - 34px);
  display: block;
  overflow-y: scroll;
  cursor: pointer;
  border-spacing: 0;
}

/* @media only screen and (max-width: 1024px) {
    table {
      height: 510px;
    }
  } */

tbody {
  /* height: 400px; */
  /* display: block;
      overflow: auto; */
}
thead {
  /* border-bottom: 1px solid #c8c8c8; */
  position: sticky;
  top: 0;
  z-index: 3;
  height: 34px;
  background: #edeef2;
  box-sizing: border-box;
}

th {
  /* position: sticky; */
  /* border-right: 1px dotted  #c8c8c8; */
  box-sizing: border-box;
  border-bottom: 1px solid #c8c8c8;
  background: #edeef2;
}
th:not(:first-child) {
  white-space: nowrap;
  min-width: 150px;
  text-align: left;
  text-transform: uppercase;
}
th:last-child {
  border-left: 1px solid #c8c8c8;
}
td:last-child {
  border-left: 1px dotted #c8c8c8;
}
th:not(:last-child) {
  border-right: 1px solid #c8c8c8;
  padding: 0 10px;
}
tr:not(thead tr) {
  height: 46px;
}
tr td:not(:last-child) {
  border-right: 1px dotted #c8c8c8;
  padding: 0 10px;
}
td {
  background: #fff;
  box-sizing: border-box;
  border-bottom: 1px solid #c8c8c8;
}

.title {
  font-size: 24px;
  font-family: Bold;
}
.MISAEmployee {
  display: flex;
  flex-direction: column;
  height: 100%;
}
.content {
  height: calc(100% - 72px - 46px - 12px);
  display: flex;
  flex-direction: column;
}

.footer {
  height: 46px;
  margin-top: auto;
}

.search-icon {
  /* border: 1px solid #000; */
  height: 22px;
  width: 22px;
  background: url("../assets/img/Sprites.64af8f61.svg") no-repeat -992px -357px;
  margin-left: 12px;
  margin-right: 4px;
}
.MISAInputSearch {
  box-sizing: border-box;
  border: 1px solid #babec5;
  display: flex;
  align-items: center;
  border-radius: 2px;
  min-width: 200px;
  margin-right: 10px;
}
.MISAInputSearch:focus-within {
  border: 1px solid #25a415;
}

.MISAInputSearch input {
  outline: none;
  border: none;
  height: 32px;
  padding: 0;
  padding-left: 12px;
  font-family: Regular;
}

.MISAInputSearch input::placeholder {
  font-family: Italic;
}
.reload-icon {
  width: 24px;
  height: 24px;
  border: none;
  /* border: 1px solid #000; */
  outline: none;
  background: #fff;
  margin: 0 6px;
  cursor: pointer;
  background: url("../assets/img/Sprites.64af8f61.svg") no-repeat -424px -201px;
}
.reload-icon:hover {
  background: url("../assets/img/Sprites.64af8f61.svg") no-repeat -1097px -89px;
}

.export-icon {
  width: 24px;
  height: 24px;
  /* border: 1px solid #000; */
  border: none;
  background: url("../assets/img/Sprites.64af8f61.svg") no-repeat -705px -200px;
  outline: none;
  /* background: #fff; */
  margin: 0 6px;
  cursor: pointer;
}

.export-icon:hover {
  background: url("../assets/img/Sprites.64af8f61.svg") no-repeat -705px -256px;
}
</style>