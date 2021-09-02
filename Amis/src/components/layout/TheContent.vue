<template>
  <div class="MISAContent"  :class="getExpand == false ? 'toggle' : ''">
    <div class="MISATop d-flex px-10">
      <div class="d-flex align-items-c">
        <div class="toggle-icon" @click="SET_TOGGLE_MENU"></div>
        <div class="MISADropdown">
          <span class="dropdown-text"
            >công ty TNHH sản xuất - thương mại - dịch vụ qui phúc</span
          >
          <span class="rotate-icon"></span>
        </div>
      </div>
      <div class="ml-auto d-flex align-items-c">
        <div class="notification"></div>
        <div class="d-flex align-items-c" style="cursor: pointer;">
          <div class="user-icon"></div>
          <span style="font-weight: 600; font-size: 14px">Lê Khắc Phúc</span>
          <span class="rotate-icon-sm"></span>
        </div>
      </div>
    </div>
    <div class="MISABody">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import { mapMutations } from 'vuex'
// import EmployeeList from "../../views/EmployeeList.vue";
export default {
  components: {
    // EmployeeList,
  },
  computed : {
    ...mapGetters(["getExpand"])
  },
  methods: {
    ...mapMutations(["SET_TOGGLE_MENU"])
  },
};
</script>

<style scoped>
.MISAContent {
  width: calc(100% - 178px);
  height: 100vh;
  background: #f4f5f7;
}
.MISAContent.toggle{
  width: calc(100% - 52px);
}
.MISATop {
  height: 48px;
  background: #fff;
}
.MISAContent.toggle .toggle-icon{
  display: none;
}
.toggle-icon {
  height: 24px;
  width: 24px;
  cursor: pointer;
  /* border: 1px solid #000; */
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -847px -30px;
}

.MISADropdown {
  font-weight: 600;
  padding-left: 10px;
  font-size: 14px;
  text-transform: uppercase;
  display: flex;
}

.MISADropdown .rotate-icon {
  transform: rotate(90deg);
  /* border: 1px solid #000; */
  height: 14px;
  width: 14px;
  display: block;
  align-self: center;
  margin: 0 10px;
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -80px -361px;
}

.notification {
  cursor: pointer;
  height: 24px;
  width: 24px;
  /* border: 1px solid #000; */
  margin-right: 16px;
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -787px -30px;
}

/* .notification:hover, .notification:focus{
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat;
} */

.user-icon {
  height: 32px;
  width: 32px;
  /* border: 1px solid #000; */
  margin-right: 6px;
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -352px -894px;
  border-radius: 50%;
}

.rotate-icon-sm {
  /* border: 1px solid #000; */
  height: 14px;
  width: 14px;
  display: block;
  align-self: center;
  margin: 0 10px;
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -1076px -32px;
}
.MISABody{
    padding: 0 20px;
    height: calc(100% - 48px - 12px);
}

</style>