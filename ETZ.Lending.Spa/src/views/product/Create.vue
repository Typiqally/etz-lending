<template>
  <div>
    <div class="pt-5 pb-4">
      <h1 class="display-4">Product aanmaken</h1>
      <router-link :to="{ name: 'product-index' }">
        Terug naar producten
      </router-link>
    </div>

    <Form
      class="row g-0 justify-content-md-center"
      @submit="onSubmit"
      :validation-schema="schema"
    >
      <div class="col-12 p-0">
        <bootstrap-input name="name" label="Naam" class="mb-3" v-model="name" />

        <div class="row flex-row-reverse mb-3">
          <div class="col-auto">
            <button class="btn btn-primary" type="submit">Aanmaken</button>
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

const ProductRepository = RepositoryContainer.get("products");

export default {
  name: "ProductCreate",
  components: {
    Form,
    BootstrapInput,
  },
  data() {
    return {
      name: "",
      skuCode: "",
      eanCode: 0,
      price: 0,
      image: null,
    };
  },
  methods: {
    async onSubmit() {
      await ProductRepository.create({
        name: this.name,
      })
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
