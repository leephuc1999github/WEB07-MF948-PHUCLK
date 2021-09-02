<template>
  <div class="modal">
    <div class="MISAEmployeeDetail">
      <div class="box">
        <div class="header d-flex">
          <div class="d-flex" style="padding: 20px 32px">
            <span style="font-size: 24px; font-family: Semibold"
              >Thông tin nhân viên</span
            >
            <div class="d-flex align-items-c px-20">
              <input type="checkbox" name="object" id="" class="mr-10" />
              Là khách hàng
            </div>
            <div class="d-flex align-items-c">
              <input type="checkbox" name="object" id="" class="mr-10" />
              Là nhà cung cấp
            </div>
          </div>
          <div class="d-flex ml-auto p-12">
            <div class="question-icon" title="Giúp"></div>
            <div
              class="close-icon"
              title="Đóng"
              @click="closeForm('close')"
            ></div>
          </div>
        </div>
        <div class="content px-32">
          <div class="d-flex">
            <div class="w-50 pr-12">
              <div class="MISARow mb-12">
                <div class="MISAInput w-40 mr-6">
                  <label for="">Mã <span class="b-red">*</span></label>
                  <div class="mt-4 MISAInputBox">
                    <input
                      type="text"
                      v-model="employee.employeeCode"
                      @blur="checkEmpty('employeeCode')"
                      @focus="removeBorder('employeeCode')"
                      ref="employeeCode"
                      tabindex="1"
                      :title="
                        flagCode ? 'Mã nhân viên không được để trống' : ''
                      "
                    />
                  </div>
                </div>
                <div class="MISAInput w-60">
                  <label for="">Tên <span class="b-red">*</span></label>
                  <div class="mt-4 MISAInputBox">
                    <input
                      type="text"
                      class="w-100"
                      ref="fullName"
                      @blur="checkEmpty('fullName')"
                      v-model="employee.fullName"
                      @focus="removeBorder('fullName')"
                      tabindex="2"
                      :title="flagName ? 'Tên không được để trống' : ''"
                    />
                  </div>
                </div>
              </div>
              <div class="MISARow mb-12">
                <div class="MISAInput w-100">
                  <label for="">Đơn vị <span class="b-red">*</span></label>
                  <div class="mt-4 MISAInputBox">
                    <Combobox
                      v-if="true"
                      :required="true"
                      :value="employee.departmentId"
                      @selected="getDepartment"
                      :tabindex="'5'"
                      :refName="'departmentId'"
                      :class="
                        flagDepartment
                          ? 'MISACombobox border-red'
                          : 'MISACombobox'
                      "
                    ></Combobox>
                  </div>
                </div>
              </div>
              <div class="MISARow">
                <div class="MISAInput w-100">
                  <label for="">Chức danh</label>
                  <div class="mt-4">
                    <input
                      type="text"
                      class="w-100"
                      tabindex="8"
                      v-model="employee.positionName"
                    />
                  </div>
                </div>
              </div>
            </div>
            <div class="w-50 pl-12">
              <div class="MISARow mb-12">
                <div class="MISADate">
                  <label for="">Ngày sinh</label>
                  <div class="mt-4">
                    <date-picker
                      ref="dateOfBirth"
                      :disabled-date="(date) => date >= new Date()"
                      v-model="employee.dateOfBirth"
                      type="date"
                      placeholder="DD/MM/YYYY"
                      :format="'DD/MM/YYYY'"
                      :value-type="'YYYY-MM-DD'"
                    ></date-picker>
                  </div>
                </div>

                <div class="MISARadio ml-10">
                  <label for="">Giới tính</label>
                  <div class="mt-4 h-32 d-flex">
                    <span class="mr-10 d-flex align-items-c"
                      ><input
                        type="radio"
                        name="Gender"
                        class="align-self-c mr-8"
                        tabindex="4"
                        v-model="employee.gender"
                        value="0"
                      />
                      Nam
                    </span>
                    <span class="mr-10 d-flex align-items-c"
                      ><input
                        type="radio"
                        name="Gender"
                        class="align-self-c mr-8"
                        value="1"
                        v-model="employee.gender"
                      />
                      Nữ</span
                    >
                    <span class="d-flex align-items-c"
                      ><input
                        type="radio"
                        name="Gender"
                        class="align-self-c mr-8"
                        value="2"
                        v-model="employee.gender"
                      />
                      Khác</span
                    >
                  </div>
                </div>
              </div>

              <div class="MISARow mb-12">
                <div class="MISAInput mr-6 w-100">
                  <label for="">Số CMND</label>
                  <div class="mt-4 MISAInputBox">
                    <input
                      type="text"
                      tabindex="6"
                      v-model="employee.identityNumber"
                    />
                  </div>
                </div>
                <div class="MISADate">
                  <label for="">Ngày cấp</label>
                  <div class="mt-4">
                    <date-picker
                      ref="identityDate"
                      :disabled-date="(date) => date >= new Date()"
                      v-model="employee.identityDate"
                      type="date"
                      placeholder="DD/MM/YYYY"
                      :format="'DD/MM/YYYY'"
                      :value-type="'YYYY-MM-DD'"
                    ></date-picker>
                  </div>
                </div>
              </div>

              <div class="MISARow">
                <div class="MISAInput w-100">
                  <label for="">Nơi cấp</label>
                  <div class="mt-4">
                    <input
                      type="text"
                      tabindex="9"
                      v-model="employee.identityPlace"
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="MISARow mb-12" style="margin-top: 34px">
            <div class="MISAInput w-100">
              <label for="">Địa chỉ</label>
              <div class="mt-4">
                <input type="text" tabindex="10" v-model="employee.address" />
              </div>
            </div>
          </div>

          <div class="MISARow mb-12">
            <div class="MISAInput mr-6">
              <label for="">ĐT di động</label>
              <div class="mt-4">
                <input
                  type="text"
                  tabindex="11"
                  v-model="employee.phoneNumber"
                />
              </div>
            </div>
            <div class="MISAInput mr-6">
              <label for="">ĐT cố định</label>
              <div class="mt-4">
                <input type="text" tabindex="12" v-model="employee.hotline" />
              </div>
            </div>
            <div class="MISAInput">
              <label for="">Email</label>
              <div class="mt-4 MISAInputBox">
                <input
                  type="text"
                  tabindex="13"
                  v-model="employee.email"
                  @blur="checkFormat('email')"
                  ref="email"
                  @focus="removeBorder('email')"
                  :title="flagEmail ? 'Email không đúng định dạng' : ''"
                />
              </div>
            </div>
          </div>
          <div class="MISARow mb-12">
            <div class="MISAInput mr-6">
              <label for="">Tài khoản ngân hàng</label>
              <div class="mt-4">
                <input
                  type="text"
                  tabindex="14"
                  v-model="employee.accountNumber"
                />
              </div>
            </div>
            <div class="MISAInput mr-6">
              <label for="">Tên ngân hàng</label>
              <div class="mt-4">
                <input type="text" tabindex="15" v-model="employee.bankName" />
              </div>
            </div>
            <div class="MISAInput">
              <label for="">Chi nhánh</label>
              <div class="mt-4">
                <input
                  type="text"
                  tabindex="16"
                  v-model="employee.branchName"
                />
              </div>
            </div>
          </div>
        </div>
        <div class="footer">
          <button
            class="MISABtn MISADefault"
            tabindex="17"
            @click="closeForm('cancle')"
          >
            Hủy
          </button>
          <div class="ml-auto"></div>
          <button
            class="MISABtn MISADefault mr-10"
            title="Cất"
            tabindex="18"
            @click="saveForm(1)"
          >
            Cất
          </button>
          <button
            class="MISABtn MISASucess"
            @click="saveForm(2)"
            title="Cất và thêm"
            tabindex="19"
          >
            Cất và Thêm
          </button>
        </div>
      </div>
    </div>
    <Dialog
      v-if="showDialog"
      :type="typeDialog"
      :content="contentDialog"
      @close="showDialog = false"
      @do="saveForm(1)"
      @exit="closeForm('cancle')"
    ></Dialog>
  </div>
</template>

<script>
import Dialog from "../components/base/BaseDialog.vue";
import DatePicker from "vue2-datepicker";
import Combobox from "./items/DepartmentCombobox.vue";
import { mapGetters } from "vuex";
import { mapMutations } from "vuex";
import { mapActions } from "vuex";
import axios from "axios";
import Common from "../Common";
export default {
  components: {
    DatePicker,
    Combobox,
    Dialog,
  },
  created() {},
  computed: {
    ...mapGetters([
      "getEmployee",
      "getMode",
      "getNewCode",
      "getAPI",
      "getEmployeeId",
    ]),
  },
  mounted() {
    this.employee = this.getEmployee;
    // mở form
    // gán dữ liệu cho employee(tất cả dữ liệu từ đối tượng nhân bản trừ mã nhân viên)
    // gán mã mới cho employee
    if (this.getMode == 2) {
      this.employee["employeeCode"] = this.getNewCode;
    }
    // giá trị so sánh
    this.clone = JSON.stringify(this.employee);
    // focus employee code
    this.focusEmployeeCode();
    // set tabindex datepicker
    this.setTabindexDatePicker("dateOfBirth", 3);
    this.setTabindexDatePicker("identityDate", 7);
  },
  data() {
    return {
      employee: {},
      showDialog: false,
      contentDialog: "",
      typeDialog: "",
      type: 1,
      clone: null,

      flagCode: false,
      flagName: false,
      flagEmail: false,
      flagDepartment: false,
    };
  },
  watch: {
    getEmployee() {
      // nếu chọn cất và thêm 
      // reset form
      if (this.type == 2) {
        this.employee = this.getEmployee;
      }
    },
  },
  methods: {
    ...mapMutations(["SET_IS_OPEN", "SET_MODE", "SET_NULL_EMPLOYEE"]),
    ...mapActions(["setNewCode"]),

    /**
     * Remove border red
     * Author : LP(29/8)
     */
    removeBorder(refName) {
      this.$refs[refName].classList = "";
      switch (refName) {
        case "fullName":
          this.flagName = false;
          break;
        case "employeeCode":
          this.flagCode = false;
          break;
      }
    },

    /**
     * Check empty input required
     * Author : LP(29/8)
     */
    checkEmpty(refName) {
      if (Common.isNullOrUndifined(this.employee[refName])) {
        this.$refs[refName].classList = "border-red";
        switch (refName) {
          case "fullName":
            this.flagName = true;
            this.contentDialog = "Tên nhân viên không được để trống";
            break;
          case "employeeCode":
            this.flagCode = true;
            this.contentDialog = "Mã nhân viên không được để trống";
            break;
        }
        return false;
      }
      return true;
    },

    /**
     * Check format input
     * Author : LP(29/8)
     */
    checkFormat(refName) {
      if (!Common.isNullOrUndifined(this.employee[refName])) {
        if (!Common.validateEmail(this.employee[refName])) {
          this.$refs[refName].classList = "border-red";
          this.contentDialog = "Email không đúng định dạng";
          this.flagEmail = true;
          return false;
        }
      }
      this.flagEmail = false;
      return true;
    },
    /**
     * save form
     * Author : LP(29/8)
     */
    saveForm(type) {
      this.type = type;

      let isValidEmail = this.checkFormat("email");
      let isValidDepartment = Common.isNullOrUndifined(
        this.employee.departmentId
      )
        ? false
        : true;
      if (!isValidDepartment) {
        this.contentDialog = "Phòng ban không được để trống";
        this.flagDepartment = true;
      }
      let isValidName = this.checkEmpty("fullName");
      let isValidCode = this.checkEmpty("employeeCode");

      if (isValidCode && isValidName && isValidEmail && isValidDepartment) {
        // kích hoạt check trùng mã nhân viên
        try {
          let self = this;
          axios
            .get(
              this.getAPI +
                `Employees/Duplicate?name=employeeCode&value=${
                  this.employee.employeeCode
                }&id=${
                  this.employee.employeeId == null
                    ? ""
                    : this.employee.employeeId
                }`
            )
            .then(function (response) {
              // nếu trả về trùng == true
              // input cảnh báo đỏ + hover thông báo
              // mở dialog cảnh báo

              // nếu trả về == false
              // thực hiện lưu dữ liệu
              if (response.data.data) {
                self.$refs.employeeCode.classList = "border-red";
                // self.$refs["employeeCode-error"].classList = "error show";
                // nội dung thông báo
                self.contentDialog =
                  `Mã nhân viên <${self.employee.employeeCode}> ` +
                  "đã tồn tại trong hệ thống, vui lòng kiểm tra lại";
                // hiển thị thông báo
                self.showDialog = true;
                self.typeDialog = "warning";
                self.flagCode = true;
              } else {
                self.flagCode = false;
                if (self.getMode == 0 || self.getMode == 2) {
                  self.addEmployee();
                } else {
                  self.editEmployee();
                }
              }
            })
            .catch((error) => {
              console.log("get duplicate " + error);
            });
        } catch (error) {
          console.log("get duplicate " + error);
        }
      } else {
        this.typeDialog = "error";
        this.showDialog = true;
      }
    },

    /**
     * Add employee
     * Author : LP(29/8)
     */
    addEmployee() {
      try {
        let self = this;
        axios
          .post(this.getAPI + `Employees`, this.employee)
          .then(async function (response) {
            // tái tạo lại employee
            self.SET_NULL_EMPLOYEE();
            if (self.type == 2) {
              // nếu chọn cất và thêm
              // thực hiện lấy mã mới cho nhân viên
              // gửi mode = 0 => thêm mới nhân viên
              await self.$store.dispatch("setNewCode");
              self.SET_MODE(0);
            }
            // gửi phản hồi sau khi thêm mới nhân viên
            await self.$emit("resultForm", response, self.type);
          });
      } catch (error) {
        console.log("add employee error " + error);
      }
    },

    /**
     * Edit employee
     * Author : LP(29/8)
     */
    editEmployee() {
      try {
        let self = this;
        axios
          .put(this.getAPI + `Employees/${this.getEmployeeId}`, this.employee)
          .then(async function (response) {
            // tái tạo lại dữ liệu cho employee
            self.SET_NULL_EMPLOYEE();
            if (self.type == 2) {
              // chọn cất và thêm
              // => sinh mã mới
              // => gán mode = 0 => thêm mới nhân viên
              await self.$store.dispatch("setNewCode");
              self.SET_MODE(0);
            }
            // gửi dữ liệu sau khi cập nhật cho bảng hiển thị
            self.$emit("resultForm", response, self.type);
          });
      } catch (error) {
        console.log("edit employee error " + error);
      }
    },

    /**
     * Get department id on combobox
     * Author : LP(28/8)
     */
    getDepartment(value) {
      this.employee.departmentId = value;
    },

    /**
     * Focus employee code
     * LP(28/8)
     */
    focusEmployeeCode() {
      this.$refs.employeeCode.focus();
    },

    /**
     * Focus date picker
     * Author : LP(1/9)
     */
    setTabindexDatePicker(refName, tab) {
      this.$refs[refName].$refs.input.setAttribute("tabindex", tab);
    },

    /**
     * Close form
     * Author : LP(28/8)
     */
    closeForm(type) {
      switch (type) {
        case "cancle":
          this.SET_IS_OPEN(false);
          this.employee = {};
          this.SET_NULL_EMPLOYEE();
          break;
        case "close":
          if (this.clone != JSON.stringify(this.employee)) {
            this.showDialog = true;
            this.contentDialog =
              "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?";
            this.typeDialog = "info";
          } else {
            this.SET_IS_OPEN(false);
            this.employee = {};
            this.SET_NULL_EMPLOYEE();
          }
          break;
      }
    },
  },
};
</script>

<style scoped>
.MISAInputBox {
  /* position: relative; */
}

.MISAInputBox .error {
  position: absolute;
  top: 24px;
  left: 24px;
  background: #2c2b30;
  color: #fff;
  height: 24px;
  line-height: 24px;
  padding: 0 12px;
  display: none;
  box-shadow: 1px 1px #000;
}
.MISAInputBox:hover .error.show {
  display: block;
}
.modal {
  position: fixed;
  z-index: 5;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  overflow: auto;
  /* background-color: rgb(0, 0, 0); */
  background-color: rgba(0, 0, 0, 0.4);
}

.MISAEmployeeDetail {
  width: 900px;
  height: 600px;
  background: #fff;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  border-radius: 5px;
  /* z-index: 3; */
}

.box {
  display: flex;
  flex-direction: column;
}

.question-icon {
  background: url("../assets/img/Sprites.64af8f61.svg") no-repeat -731px -30px;
  width: 24px;
  height: 24px;
  margin-right: 6px;
  cursor: pointer;
}
.close-icon {
  /* border: 1px solid #000; */
  height: 24px;
  width: 24px;
  background: url("../assets/img/Sprites.64af8f61.svg") no-repeat -144px -144px;
  /* width: 18px;
	height: 18px; */
  cursor: pointer;
}

.MISARow {
  display: flex;
  width: 100%;
}

.MISAInput label,
.MISADate label,
.MISARadio label {
  font-family: Bold;
}

.MISAInput input {
  box-sizing: border-box;
  height: 32px;
  border: 1px solid #babec5;
  outline: none;
  padding: 0 10px;
  width: 100%;
  color: #555;
}

.MISAInput input.border-red {
  border: 1px solid red;
}

.MISAInput input:focus {
  border: 1px solid #25a415;
}
.content {
  height: calc(600px - 72.8px - 76px);
}
.footer {
  height: 76px;
  /* background: red; */
  margin-top: auto;
  border-bottom-left-radius: 5px;
  border-bottom-right-radius: 5px;
  border-top: 1px solid #dfdfdf;
  margin: auto 32px;
  display: flex;
  align-items: center;
}
</style>