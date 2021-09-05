<template>
  <div class="bg-dark">
    <div class="MISADialog p-32">
      <div class="content d-flex align-items-c">
        <div>
          <div class="warning-icon" :class="type"></div>
        </div>
        <div class="d-block">{{ content }}</div>
      </div>
      <span class="divide"></span>
      <!-- thông tin -->
      <div class="d-flex align-items-c mt-20" v-if="type == 'info'">
        <button class="MISABtn MISADefault" @click="closeDialog()">Hủy</button>
        <div class="ml-auto"></div>
        <button class="MISABtn MISADefault mr-8" @click="exitForm()">
          Không
        </button>
        <button class="MISABtn MISASucess" @click="doForm()">Có</button>
      </div>
      <!-- lỗi -->
      <div
        class="d-flex align-items-c mt-20"
        style="justify-content: center"
        v-else-if="type == 'error'"
      >
        <button class="MISABtn MISASucess" @click="closeDialog()">Đóng</button>
      </div>

      <!-- xác nhận -->
      <div class="d-flex align-items-c mt-20" v-else-if="type == 'confirm'">
        <button class="MISABtn MISADefault mr-8" @click="closeDialog()">
          Không
        </button>
        <div class="ml-auto"></div>
        <button class="MISABtn MISASucess" @click="doForm()">Có</button>
      </div>

      <!-- cảnh báo -->
      <div class="d-flex align-items-c mt-20" v-else>
        <button class="MISABtn MISASucess ml-auto" @click="closeDialog">
          Đồng ý
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["type", "content"],
  data() {
    return {};
  },
  methods: {
    /**
     * do form
     * Author : LP(29/8)
     */
    doForm() {
      this.$emit("do");
    },

    /**
     * exit form
     * Author : LP(29/8)
     */
    exitForm() {
      this.$emit("exit");
    },

    /**
     * Close dialog
     * Author : LP(29/8)
     */
    closeDialog() {
      this.$emit("close");
    },
  },
};
</script>

<style scoped>
.divide {
  border-top: 1px solid #ccc;
  margin-top: 20px;
  display: block;
  padding: 0;
}

.warning-icon {
  display: block;
  /* border: 1px solid #000; */
  height: 48px;
  width: 56px;
}

.warning-icon.warning,
.warning-icon.confirm {
  /* width: 100%; */
  
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -598px -458px;
}

.warning-icon.error {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -752px -456px;
}
.warning-icon.info {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -832px -456px;
}
.bg-dark {
  position: fixed;
  z-index: 4;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  overflow: auto;
  /* background-color: rgb(0, 0, 0); */
  background-color: rgba(0, 0, 0, 0.4);
}

.MISADialog {
  width: 400px;
  background: #fff;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  border-radius: 5px;
  /* box-sizing: border-box; */
  /* z-index: 3; */
}

.MISADialog .content {
  width: 100%;
}
</style>