<template>
  <div v-if="product">
    <modal title="Product verwijderen" id="modal" v-model:is-open="isOpen">
      <div class="modal-body">
        Weet je zeker dat je de {{ product.name }} wil verwijderen?
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
          Annuleren
        </button>
        <button
          type="button"
          class="btn btn-danger"
          data-bs-dismiss="modal"
          @click="deleteProduct"
        >
          Verwijderen
        </button>
      </div>
    </modal>

    <div class="pt-5 pb-4">
      <h1 class="display-4">{{ product.name }} aanpassen</h1>
      <router-link :to="{ name: 'product-index' }">
        Terug naar producten
      </router-link>
    </div>

    <Form class="row g-0 justify-content-md-center" @submit="onSubmit">
      <div class="col-12 p-0">
        <bootstrap-input
          name="name"
          label="Naam"
          class="mb-3"
          v-model="product.name"
        />

        <div class="row flex-row-reverse mb-3">
          <div class="col-auto">
            <button class="btn btn-primary" type="submit">Opslaan</button>
          </div>
        </div>

        <hr />

        <div class="row flex-row-reverse mb-3">
          <div class="col-auto">
            <button class="btn btn-danger" type="button" @click="isOpen = true">
              Verwijderen
            </button>
          </div>
        </div>
      </div>
    </Form>
  </div>
</template>

<script>
import { Form } from "vee-validate";
import RepositoryContainer from "@/repositories/RepositoryContainer";
import BootstrapInput from "@/components/BootstrapInputComponent";
import Modal from "@/components/ModalComponent";

const ProductRepository = RepositoryContainer.get("products");

export default {
  name: "ProductDetails",
  components: {
    Form,
    BootstrapInput,
    Modal,
  },
  data() {
    return {
      product: null,
      image: null,
      isOpen: false,
    };
  },
  async mounted() {
    await ProductRepository.fetch(this.$route.params.id)
      .then((response) => {
        this.product = response.data;
      })
      .catch((error) => {
        console.error(error);
      });
  },
  methods: {
    async onSubmit() {
      await ProductRepository.update(this.product.id, this.product)
        .then(() => {
          this.$router.push({ name: "product-index" });
        })
        .catch((error) => {
          console.error(error);
        });
    },
    async deleteProduct() {
      await ProductRepository.delete(this.product.id)
        .then(() => {
          this.$router.push({ name: "product-index" });
        })
        .catch((error) => {
          console.error(error);
        });
    },
  },
};
</script>
