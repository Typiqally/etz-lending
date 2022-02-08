import { createApp } from "vue";
import axios from "axios";
import VueAxios from "vue-axios";

import { nl } from "yup-locales";
import { setLocale } from "yup";

import App from "./App.vue";
import Layout from "./layouts/Layout.vue";
import router from "./router";
import store from "./store";

import "./assets/styles/main.scss";

setLocale(nl);

createApp(App)
  .use(store)
  .use(router)
  .use(VueAxios, axios)
  .component("Layout", Layout)
  .mount("#app");
