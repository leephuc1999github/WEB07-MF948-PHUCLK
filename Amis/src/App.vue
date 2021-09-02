<template>
  <div id="app">
    <Navbar></Navbar>
    <Content></Content>
  </div>
</template>

<script>
import "vue2-datepicker/index.css";
import Content from "./components/layout/TheContent.vue";
import Navbar from "./components/layout/TheNavbar.vue";
export default {
  name: "App",
  components: {
    Navbar,
    Content,
  },
};
</script>

<style scoped>
@import "./styles/main.css";
@import "./styles/date-picker.css";
@import "./styles/paging.css";
#app {
  display: flex;
  height: 100vh;
  width: 100%;
}
</style>
