<template>
  <div class="card mb-4">
    <img
      src="@/assets/product-vr.png"
      class="card-img-top product-image"
      :alt="product.name"
    />
    <div class="card-body">
      <h5 class="card-title">{{ product.name }}</h5>
      <router-link
        :to="{ name: 'product-details', params: { id: product.id } }"
        class="card-link"
      >
        Aanpassen
      </router-link>
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
    moment(date) {
      moment.locale("nl");
      return moment(date);
    },
  },
};
</script>
