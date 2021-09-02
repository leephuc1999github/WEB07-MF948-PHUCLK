<template>
  <div class="MISAContext">
    <a href="javascript:void(0)" class="edit" @click="openFormToEdit">Sửa</a>
    <div
      class="more-site ml-10"
      :class="show == true ? 'active' : ''"
      @click="show = !show"
    >
      <span class="more-icon"> </span>
      <div class="more-options" v-if="show" :class="nth">
        <a href="javascript:void(0)" @click="openFormToClone">Nhân bản</a>
        <a href="javascript:void(0)" @click="toDelete">Xóa</a>
        <a href="javascript:void(0)" class="disabled">Ngừng sử dụng</a>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["id", "nth"],
  data() {
    return {
      show: false,
    };
  },
  created() {
    // alert(this.id);
  },
  mounted() {
    document.addEventListener("click", this.close);
  },
  methods: {

    /**
     * Click to delete
     * Author : LP(28/8)
     */
    toDelete(){
      this.$emit("delete", this.id);
    },


    /**
     * Open form to clone
     * Author : LP(28/8)
     */
    openFormToClone(){
      this.$emit("clone", this.id);
    },


    /**
     * Open form edit
     * Author : LP(29/8) 
     */
    openFormToEdit(){
      this.$emit("edit", this.id);
    },


    /**
     * Close more options
     * Author : LP(28/8)
     */
    close(event) {
      if (!this.$el.contains(event.target)) {
        this.show = false;
      }
    },
  },
};
</script>

<style scoped>
.MISAContext a {
  text-decoration: none;
}
.more-site {
  width: 28px;
  height: 18px;
  border: 1px solid transparent;
  position: relative;

}
.more-site.active {
  border: 1px solid #058bd4;
}
.MISAContext {
  height: 100%;
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;

}
.edit {
  color: #058bd4;
  font-family: Semibold;
}
.more-icon {
  height: 16px;
  width: 16px;
  cursor: pointer;
  /* border: 1px solid #000000; */
  display: block;
  margin: 0 auto;
  background: url("../../assets/img/Sprites.4dee508c.svg") no-repeat -896px -359px;
}
.more-options {
  background: #fff;
  width: 100%;
  position: absolute;
  top: 24px;
  left: -80px;
  z-index: 1;
  width: 130px;
  border: 1px solid #c8c8c8;
}


.more-options a {
  display: block;
  height: 32px;
  line-height: 32px;
  color: #000;
  padding: 0 12px;
  text-align: left;
}
.last {
  background: #fff;
  position: absolute;
  top: auto;
  bottom: 24px;
  left: -80px;
  z-index: 999;
  width: 130px;
  border: 1px solid #c8c8c8;
}
/* .more-options:nth-last-child(1) {
  background: red;
  position: absolute;
  bottom: 0px;
  left: -80px;
  z-index: 999;
  width: 130px;
  border: 1px solid #c8c8c8;
}

.more-options:nth-last-child(2) {
  background: red;
  position: absolute;
  bottom: 0px;
  left: -80px;
  z-index: 999;
  width: 130px;
  border: 1px solid #c8c8c8;
} */
.more-options a:hover:not(.disabled) {
  background: #e8e9ed;
  color: #48b660;
}

.more-options a.disabled{
  /* background: #c8c8c8; */
  color: #ccc;
}
</style>