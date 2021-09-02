import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

//Views
import Employee from "../views/EmployeeList.vue";
import Cash from '../views/Cash.vue'

const routes = [
  { path: "/", component: Employee },
  { path : "/cash", component : Cash}
];

const router = new VueRouter({
  mode: 'history',
  routes,
  linkActiveClass: "active", // active class for non-exact links.
  linkExactActiveClass: "active" // active class for *exact* links.
});

export default router;