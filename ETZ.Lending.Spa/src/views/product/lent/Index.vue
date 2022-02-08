<template>
  <div id="product">
    <div class="row align-items-center justify-content-between pt-5 pb-4">
      <div class="col-auto">
        <h1 class="display-4">Uitgeleend</h1>
      </div>
    </div>

    <div v-if="loading" class="text-center">
      <i class="fas fa-circle-notch fa-spin fa-3x fa-fw mb-3"></i>
      <h3 class="mb-3">Uitgeleende producten laden</h3>
      <p>Producten worden geladen, even geduld</p>
    </div>
    <div v-else-if="lentProducts.length" class="row">
      <div
        class="col-12 col-sm-6 col-lg-4"
        v-for="lentProduct in lentProducts"
        :key="lentProduct.id"
      >
        <lent-product :lentProduct="lentProduct" />
      </div>
    </div>
    <div v-else class="text-center">
      <i class="far fa-frown fa-4x mb-3"></i>
      <h3 class="mb-3">Geen resultaten</h3>
      <p>De uitgeleende producten waar je naar zocht konden niet gevonden worden</p>
    </div>
  </div>
</template>

<script>
import RepositoryContainer from "@/repositories/RepositoryContainer";
const LentProductRepository = RepositoryContainer.get("lentProducts");

import LentProduct from "@/components/LentProductComponent";

export default {
  name: "LentProductIndex",
  components: {
    LentProduct,
  },
  data() {
    return {
      lentProducts: [],
      loading: false,
    };
  },
  async mounted() {
    this.loading = true;

    await LentProductRepository.fetchAll()
      .then((response) => {
        this.lentProducts = response.data;
        this.loading = false;
      })
      .catch((error) => {
        console.error(error);
      });
  },
};
</script>
