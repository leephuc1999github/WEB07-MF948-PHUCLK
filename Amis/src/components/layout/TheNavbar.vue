<template>
  <div class="MISANavbar" :class="getExpand == false ? 'toggle' : ''">
    <div class="d-flex pl-24 top" style="height: 48px">
      <div class="icon align-self-c" @click="SET_TOGGLE_MENU"></div>
      <div class="logo align-self-c"></div>
    </div>
    <div class="menu">
      <router-link to="/dashboard" class="menu-item" exact-active-class title="Tổng quan">
        <div class="menu-icon" id="tongquan"></div>
        <span>Tổng quan</span>
      </router-link>
      <router-link to="/cash" class="menu-item" exact-active-class title="Tiền mặt">
        <div class="menu-icon" id="tienmat"></div>
        <span>Tiền mặt</span>
      </router-link>
      <router-link to="/bank-deposits" class="menu-item" exact-active-class title="Tiền gửi">
        <div class="menu-icon" id="tiengui"></div>
        <span>Tiền gửi</span>
      </router-link>
      <router-link to="/purchure" class="menu-item" exact-active-class title="Mua hàng">
        <div class="menu-icon" id="muahang"></div>
        <span>Mua hàng</span>
      </router-link>
      <router-link to="/sale" class="menu-item" exact-active-class title="Bán hàng">
        <div class="menu-icon" id="banhang"></div>
        <span>Bán hàng</span>
      </router-link>
      <router-link to="/bill" class="menu-item" exact-active-class="" title="Hóa đơn">
        <div class="menu-icon" id="hoadon"></div>
        <span>Hóa đơn</span>
      </router-link>
      <router-link to="/store" class="menu-item" exact-active-class title="Kho">
        <div class="menu-icon" id="kho"></div>
        <span>Kho</span>
      </router-link>
      <router-link to="/tool" class="menu-item" exact-active-class title="Danh sách dụng cụ">
        <div class="menu-icon" id="dungcu"></div>
        <span>Công cụ dụng cụ</span>
      </router-link>
      <router-link to="/money" class="menu-item" exact-active-class title="Tài sản">
        <div class="menu-icon" id="taisan"></div>
        <span>Tài sản</span>
      </router-link>
      <router-link to="/" class="menu-item" active-class title="Nhân viên">
        <div class="menu-icon" id="nhanvien"></div>
        <span>Nhân viên</span>
      </router-link>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import { mapMutations } from 'vuex'
export default {
  computed:{
    ...mapGetters(["getExpand"])
  },
  methods: {
    ...mapMutations(["SET_TOGGLE_MENU"])
  },
};
</script>

<style scoped>
.MISANavbar {
  width: 178px;
  background: #393a3e;
}

.MISANavbar.toggle{
  width: 52px;
}
.MISANavbar.toggle .top{
  margin: 0 auto;
  padding: 0;
}

.MISANavbar.toggle .menu span{
  display: none;
}
.MISANavbar.toggle .logo{
  display: none;
}

.MISANavbar.toggle .icon{
  margin: 0 auto;
  height: 24px;
  width: 24px;
  cursor: pointer;
  /* border: 1px solid #000; */
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -312px -32px;
}
.icon {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat;
  height: 24px;
  width: 24px;
  margin-right: 10px;
  background-position: -424px -88px;
  cursor: pointer;
}
.logo {
  cursor: pointer;
  height: 30px;
  width: 100px;
  background: url("../../assets/img/Logo_Module_TiengViet_White.66947422.svg")
    no-repeat;
  background-size: contain;
}

.menu-item{
    height: 42px;
    display: block;
    text-decoration: none;
    color: #fff;
    display: flex;
    align-items: center;
    border-left: 6px solid transparent;
}

.menu-item:hover{
    background: #5d5d61;
}

.menu-icon{
    height: 24px;
    width: 24px;
    border: 1px solid #fff;
    margin-right: 8px;
    margin-left: 10px;
}

.menu-item.active{
    border-left: 6px solid #2ca01c;
    background: #5d5d61;
}
</style>