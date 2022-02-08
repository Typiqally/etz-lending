<template>
  <component :is="layout">
    <slot />
  </component>
</template>

<script>
import LayoutDefault from "./LayoutDefault";
import { shallowRef, watch } from "vue";
import { useRoute } from "vue-router";

export default {
  name: "AppLayout",
  setup() {
    const layout = shallowRef(LayoutDefault);
    const route = useRoute();
    watch(
      () => route.meta,
      async (meta) => {
        try {
          const component = await require(`@/layouts/${meta.layout}.vue`);
          layout.value = component?.default || LayoutDefault;
        } catch (e) {
          layout.value = LayoutDefault;
        }
      }
    );
    return { layout };
  },
};
</script>
