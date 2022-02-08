import { createRouter, createWebHistory } from "vue-router";

const routes = [
  {
    path: "/product",
    name: "product-index",
    component: () => import("./views/product/Index"),
  },
  {
    path: "/product/:id",
    name: "product-details",
    component: () => import("./views/product/Details"),
  },
  {
    path: "/product/create",
    name: "product-create",
    component: () => import("./views/product/Create"),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
  linkActiveClass: "active",
  linkExactActiveClass: "exact-active",
});

export default router;
