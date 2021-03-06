import { createRouter, createWebHistory } from "vue-router";

const routes = [
  {
    path: "/product",
    name: "product-index",
    component: () => import("./views/product/Index"),
  },
  {
    path: "/product/lent",
    name: "lent-product-index",
    component: () => import("./views/product/lent/Index"),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
  linkActiveClass: "active",
  linkExactActiveClass: "exact-active",
});

export default router;
