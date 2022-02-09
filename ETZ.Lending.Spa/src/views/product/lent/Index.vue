<template>
  <div id="product">
    <modal title="Product verlengen" id="modal" v-model:is-open="isOpen">
      <div class="modal-body">
        <bootstrap-input
          name="expirationDate"
          label="Nieuwe vervaldatum"
          class="mb-3"
          type="date"
          v-model="expiredAt"
        />
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
          Annuleren
        </button>
        <button
          type="button"
          class="btn btn-primary"
          data-bs-dismiss="modal"
          @click="extendLentProduct"
        >
          Verlengen
        </button>
      </div>
    </modal>

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
        <lent-product
          :lentProduct="lentProduct"
          v-on:extend="selectLentProduct"
        />
      </div>
    </div>
    <div v-else class="text-center">
      <i class="fas fa-cube fa-4x mb-3"></i>
      <h3 class="mb-3">Geen uitgeleende producten</h3>
      <p>Uitgeleende producten worden hier weergegeven</p>
    </div>
  </div>
</template>

<script>
import RepositoryContainer from "@/repositories/RepositoryContainer";

const LentProductRepository = RepositoryContainer.get("lentProducts");

import LentProduct from "@/components/LentProductComponent";
import BootstrapInput from "@/components/BootstrapInputComponent";
import Modal from "@/components/ModalComponent";

export default {
  name: "LentProductIndex",
  components: {
    Modal,
    BootstrapInput,
    LentProduct,
  },
  data() {
    return {
      lentProducts: [],
      loading: false,
      isOpen: false,
      selectedLentProduct: {},
      expiredAt: "",
    };
  },
  methods: {
    selectLentProduct(lentProduct) {
      console.log(lentProduct);
      this.selectedLentProduct = lentProduct;
      this.isOpen = true;
    },
    async extendLentProduct() {
      await LentProductRepository.extend(
        this.selectedLentProduct.id,
        this.expiredAt
      )
        .then(async () => {
          await this.fetchLentProducts();
          this.isOpen = false;
        })
        .catch((error) => {
          console.error(error);
        });
    },
    async fetchLentProducts() {
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
  },
  async mounted() {
    await this.fetchLentProducts();
  },
};
</script>
