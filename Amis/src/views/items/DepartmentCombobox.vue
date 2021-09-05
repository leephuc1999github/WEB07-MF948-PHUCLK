<template>
  <div class="MISACombobox" :ref="refName">
    <div class="d-flex">
      <input
        type="text"
        v-model="keysearch"
        class="h-100 align-self-c"
        :tabindex="tabindex"
        @blur="checkEmpty"
        @focus="removeBorder;"
        :title="msgValidate"
        ref="department"
      />

      <div
        class="rotate-site"
        @click="showAll"
        :class="show == true ? 'deg-180' : ''"
      >
        <div class="rotate"></div>
      </div>
    </div>
    <div class="data" v-if="show">
      <table>
        <thead>
          <tr>
            <th class="t-l">Mã đơn vị</th>
            <th class="t-l">Tên đơn vị</th>
          </tr>
        </thead>
        <tbody>
          <tr
            :class="current == department.departmentId ? 'active' : ''"
            @click="choose(index)"
            :tabindex="tabindex"
            v-shortkey="['enter']" @shortkey="choose(index)"
            v-for="(department, index) in options"
            :key="index"
          >
            <td>{{ department.departmentCode }}</td>
            <td>{{ department.departmentName }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import Common from "../../Common";
export default {
  props: ["value", "tabindex", "required", "title", "refName"],
  data() {
    return {
      show: false,
      keysearch: null,
      current: null,
      currentName: null,
      options: [],
      msgValidate: "",
    };
  },
  created() {},
  computed: {
    ...mapGetters(["getDepartments"]),
  },
  mounted() {
    document.addEventListener("click", this.closeData);
    this.options = this.getDepartments;
  },
  watch: {
    // giá trị departmentId thay đổi
    // gán current = value
    value() {
      this.active(this.value);
    },

    keysearch() {
      if (this.keysearch != this.currentName) {
        this.show = true;
      } else {
        this.show = false;
      }

      if (this.keysearch == "" || this.keysearch == null) {
        // xóa hết ô input => bỏ chọn giá trị hiện tại
        // giá trị so sánh currentName gán = null
        // hiển thị tất cả danh sách
        this.current = null;
        this.currentName = null;
        this.options = this.getDepartments;
      } else {
        // giá trị trong ô input còn
        // tìm kiếm theo tên và mã
        this.options = [];
        this.getDepartments.forEach((element) => {
          if (
            element.departmentName
              .toLowerCase()
              .includes(this.keysearch.toLowerCase()) ||
            element.departmentCode
              .toLowerCase()
              .includes(this.keysearch.toLowerCase())
          ) {
            this.options.push(element);
          }
        });
      }
    },
  },
  methods: {


    /**
     * Remove border red
     * Author : LP(2/9)
     */
    removeBorder() {
      this.$refs.departmentId.classList = "MISACombobox";
      this.msgValidate = "";
    },

    /**
     * Check empty
     * Author : LP(31/8)
     */
    checkEmpty() {
      if (!this.isValid(this.current)) {
        this.$refs.departmentId.classList = "MISACombobox border-red";
        this.msgValidate = "Phòng ban không được để trống";
      }
    },

    /**
     * Show all options
     * Author : LP(31/8)
     */
    showAll() {
      // lấy lại danh sách các lựa chọn
      this.options = this.getDepartments;
      // đóng mở dữ liệu
      this.show = !this.show;
    },

    /**
     * Choose option
     * Author : LP(31/7)
     */
    choose(index) {
      // gán mã đơn vị
      this.active(this.options[index].departmentId);
      // active text lên input
      this.keysearch = this.options[index].departmentName;
      // gửi dữ liệu
      this.$emit("selected", this.current);
      // xóa border-red nếu có
      this.$refs.departmentId.classList = "MISACombobox";
      this.msgValidate = "";

      this.$refs.department.focus();
    },

    /**
     * Active by value
     * Author : LP(31/8)
     */
    active(id) {
      this.current = this.isValid(id)
        ? this.getDepartments[this.index].departmentId
        : null;
      if (!Common.isNullOrUndifined(this.current)) {
        this.currentName = this.getDepartments[this.index].departmentName;
        this.keysearch = this.getDepartments[this.index].departmentName;
      } else {
        this.currentName = null;
        this.keysearch = null;
      }
    },

    /**
     * Check find id
     * Author : LP(31/8)
     */
    isValid(id) {
      for (let i = 0; i < this.getDepartments.length; i++) {
        if (this.getDepartments[i].departmentId === id) {
          this.index = i;
          return true;
        }
      }
      return false;
    },

    /**-------------------------------------------------------
     * Close data
     * Author : LP(28/8)
     */
    closeData(event) {
      if (!this.$el.contains(event.target)) {
        // đóng data
        this.show = false;
        // nếu current tồn tại
        // gán keysearch = tên đơn vị
        // nếu current không tồn tại
        // gán keysearch = null
        if (this.isValid(this.current)) {
          this.keysearch = this.getDepartments[this.index].departmentName;
        } else {
          this.keysearch = null;
          this.current = null;
        }
      }
    },
  },
};
</script>

<style scoped>
.rotate-site:hover {
  background: #dfdfdf;
}
.rotate-site.deg-180 .rotate {
  transform: rotate(180deg);
}
.rotate-site .rotate {
  cursor: pointer;
  /* border: 1px solid #000; */
  height: 32px;
  width: 32px;
  box-sizing: border-box;
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -360px -351px;
}
.MISACombobox {
  height: 32px;
  border: 1px solid #babec5;
  position: relative;
}

/* .MISACombobox:hover {
  border: 1px solid #babec5;
} */
.MISACombobox:focus-within {
  border: 1px solid #2ca01c;
}
.MISACombobox .data {
  position: absolute;
  top: 34px;
  height: 10;
  background: #fff;
  width: 100%;
  padding: 4px 0;
  border: 1px solid #babec5;
  /* box-shadow: 1px 1px #000; */
  border-radius: 4px;
}
.MISACombobox .data table {
  width: 100%;
  border-collapse: collapse;
}
.MISACombobox .data thead {
  background: #f4f5f8;
}

.MISACombobox .data tr {
  height: 32px;
}

td,
th {
  padding: 0 10px;
}
td {
  text-transform: uppercase;
}
tr.active {
  background: #2ca01c;
  color: #fff;
}
tbody tr {
  cursor: pointer;
  outline: none;
}
tbody tr:hover:not(.active) {
  color: #2ca01c;
}

tbody tr:focus:not(.active) {
  color: #2ca01c;
}

.MISACombobox input {
  border: none;
  outline: none;
  padding: 0 10px;
  width: 100%;
  border-radius: 2px;
  background: #fff;
}

.border-red {
  border: 1px solid red;
}
</style>