import Vue from 'vue'
import App from './App.vue'
import { store } from './store/index'
import Toast from 'vue-toastification'
import "vue-toastification/dist/index.css";
import Paginate from 'vuejs-paginate'
import router from "./router";
Vue.component('paginate', Paginate)
Vue.config.productionTip = false

const options = {
  // You can set your default options here
  position: 'bottom-right'
}
Vue.use(Toast, options)
new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app')
