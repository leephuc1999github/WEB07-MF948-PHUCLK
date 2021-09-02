<template>
  <div class="MISADropdown" @click="show = !show">
    <div class="d-flex">
      <input
        type="text"
        :value="getCurrentPaging.Text"
        readonly
        class="h-100 align-self-c"
      />
      <div class="rotate-site" :class="show == true ? 'deg-180' : ''">
        <div class="rotate"></div>
      </div>
    </div>
    <div class="data" v-show="show">
      <span
        v-for="(option, index) in getOptionsPaging"
        :key="index"
        @click="changePaging(index)"
        :class="getCurrentPaging.PageSize == option.PageSize ? 'active' : ''"
      >
        {{ option.Text }}</span
      >
    </div>
  </div>
</template>

<script>
import { mapGetters, mapMutations } from "vuex"
import EventBus from '../../EventBus'
export default {
  data() {
    return {
      show: false
    };
  },
  created() {
      
  },
  watch : {
    
  },
  mounted() {
    document.addEventListener("click", this.closeData);
  },
  computed: {
    ...mapGetters(["getCurrentPaging", "getOptionsPaging"]),
  },
  methods: {
    ...mapMutations(["SET_PAGING_OPTION"]),
    /**
     * Change option paging
     * Author : LP(28/8)
     */
    changePaging(index){
      this.SET_PAGING_OPTION(index);
      EventBus.$emit("chooseOptionPaging");
    },

    /**-------------------------------------------------------
     * Close data
     * Author : LP(28/8)
     */
    closeData(event) {
      if (!this.$el.contains(event.target)) {
        this.show = false;
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
  height: 30px;
  width: 30px;
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -360px -351px;
}
.MISADropdown {
  height: 32px;
  box-sizing: border-box;
  border: 1px solid #babec5;
  position: relative;
}

.MISADropdown:hover {
  border: 1px solid #2ca01c;
}
.MISADropdown:focus-within{
    border: 1px solid #2ca01c;
}
.MISADropdown .data {
  position: absolute;
  bottom: 34px;
  height: 10;
  background: #fff;
  width: 100%;
  padding: 4px 0;
  border: 1px solid #babec5;
  /* box-shadow: 1px 1px #000; */
  border-radius: 4px;
}
.MISADropdown .data span {
  display: block;
  height: 32px;
  padding: 0 10px;
  line-height: 32px;
  cursor: pointer;
}

.MISADropdown .data span:hover {
  color: #2ca01c;
}

.MISADropdown .data span.active {
  background: #2ca01c;
  color: #fff;
}

.MISADropdown input {
  border: none;
  outline: none;
  padding: 0 10px;
  cursor: pointer;
}
</style>