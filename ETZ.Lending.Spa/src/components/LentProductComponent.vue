<template>
  <div class="card mb-4">
    <img
      :src="lentProduct.product.image.absoluteUrl"
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
        <small
          :class="[
            moment(lentProduct.expiredAt).isBefore()
              ? 'text-danger'
              : 'text-success',
          ]"
        >
          Vervallen
          {{ moment(lentProduct.expiredAt).fromNow() }}
        </small>
      </p>
      <a class="card-link" @click="onExtend">Verlengen</a>
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
    onExtend() {
      this.$emit("extend", this.lentProduct);
    },
    moment(date) {
      moment.locale("nl");
      return moment(date);
    },
  },
};
</script>
