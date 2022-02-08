<template>
  <div class="card mb-4">
    <img
      src="@/assets/product-vr.png"
      class="card-img-top product-image"
      :alt="lentProduct.product.name"
    />
    <div class="card-body">
      <h5 class="card-title">{{ lentProduct.product.name }}</h5>
      <p class="card-text">
        <small class="text-muted">
          Geleend {{ moment(lentProduct.createdAt).calendar() }}
        </small>
        <br />
        <small class="text-muted">
          Vervalt
          {{ moment(lentProduct.expiredAt).fromNow() }}
        </small>
      </p>
    </div>
  </div>
</template>

<script>
import moment from "moment";

export default {
  name: "LentProduct",
  props: {
    lentProduct: null,
  },
  methods: {
    moment(date) {
      moment.locale("nl");
      return moment(date);
    },
  },
};
</script>
