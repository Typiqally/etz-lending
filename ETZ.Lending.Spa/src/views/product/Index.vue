<template>
  <div id="product">
    <div class="row align-items-center justify-content-between pt-5 pb-4">
      <div class="col-auto">
        <h1 class="display-4">Producten</h1>
      </div>
      <div class="col-auto">
        <div class="row">
          <div class="col-auto">
            <label>
              <input
                type="text"
                class="form-control"
                v-model="query"
                placeholder="Zoeken"
              />
            </label>
          </div>
        </div>
      </div>
    </div>

    <div v-if="loading || searching" class="text-center">
      <i class="fas fa-circle-notch fa-spin fa-3x fa-fw mb-3"></i>
      <h3 class="mb-3">Producten laden</h3>
      <p>Producten worden geladen, even geduld</p>
    </div>
    <div v-else-if="computed.length" class="row">
      <div
        class="col-12 col-sm-6 col-lg-4"
        v-for="product in computed"
        :key="product.id"
      >
        <product :product="product" />
      </div>
    </div>
    <div v-else class="text-center">
      <div v-if="query.length === 0">
        <i class="fas fa-cube fa-4x mb-3"></i>
        <h3 class="mb-3">Geen producten</h3>
        <p>Nieuwe producten worden hier weergegeven</p>
      </div>
      <div v-else>
        <i class="far fa-frown fa-4x mb-3"></i>
        <h3 class="mb-3">Geen resultaten</h3>
        <p>De producten waar je naar zocht konden niet gevonden worden</p>
      </div>
    </div>
  </div>
</template>

<script>
import RepositoryContainer from "@/repositories/RepositoryContainer";
const ProductRepository = RepositoryContainer.get("products");

import Product from "@/components/ProductComponent.vue";

export default {
  name: "ProductIndex",
  components: {
    Product,
  },
  data() {
    return {
      products: [],
      filtered: [],
      loading: false,
      query: "",
      searching: false,
    };
  },
  async mounted() {
    this.loading = true;

    await ProductRepository.fetchAll()
      .then((response) => {
        this.products = response.data;
        this.loading = false;
      })
      .catch((error) => {
        console.error(error);
      });
  },
  watch: {
    query: async function () {
      if (!this.searching) {
        await setTimeout(async () => {
          await ProductRepository.search(this.query)
            .then((response) => {
              this.filtered = response.data;
              this.searching = false;
            })
            .catch((error) => {
              console.error(error);
            });
        }, 500);
      }

      this.searching = true;
    },
  },
  computed: {
    computed() {
      if (this.query) {
        return this.filtered;
      } else {
        return this.products;
      }
    },
  },
};
</script>
