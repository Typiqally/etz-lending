<template>
  <div class="card mb-4">
    <img
      :src="product.image.absoluteUrl"
      class="card-img-top product-image"
      :alt="product.name"
    />
    <div class="card-body">
      <h5 class="card-title">{{ product.name }}</h5>
      <a class="card-link" @click="onLend">Lenen</a>
      <p class="card-text">
        <small class="text-muted">
          Laatst gewijzigd {{ moment(product.updatedAt).calendar() }}
        </small>
      </p>
    </div>
  </div>
</template>

<script>
import moment from "moment";

export default {
  name: "Product",
  props: {
    product: null,
  },
  methods: {
    onLend() {
      this.$emit("lend", this.product);
    },
    moment(date) {
      moment.locale("nl");
      return moment(date);
    },
  },
};
</script>
