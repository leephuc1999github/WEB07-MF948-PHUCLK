<template>
  <div class="MISALoading">
    <img src="../../assets/loading.svg" alt="" />
  </div>
</template>

<script>
export default {};
</script>

<style scoped>
.MISALoading {
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: rgba(#ffffff, 0.2);
  z-index: 1111;
}

.MISALoading img {
  animation: clock 1s infinite;
}
@keyframes clock {
  from {
    transform: rotate(0);
  }
  to {
    transform: rotate(360deg);
  }
}
</style>