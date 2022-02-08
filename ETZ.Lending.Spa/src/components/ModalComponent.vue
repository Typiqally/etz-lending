<template>
  <div
    class="modal fade"
    :id="id"
    tabindex="-1"
    :aria-labelledby="`${id}Label`"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" :id="`${id}Label`">{{ title }}</h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <slot />
      </div>
    </div>
  </div>
</template>

<script>
import Modal from "bootstrap/js/dist/modal";

export default {
  name: "Modal",
  props: {
    id: String,
    title: String,
    isOpen: Boolean,
    options: {
      keyboard: false,
    },
  },
  data() {
    return {
      modal: null,
    };
  },
  mounted() {
    const element = document.getElementById(this.id);

    this.modal = new Modal(element, this.options);
    element.addEventListener("hidden.bs.modal", () =>
      this.$emit("update:is-open", false)
    );

    if (this.open) {
      this.modal.show();
    }
  },
  unmounted() {
    if (this.isOpen) {
      this.modal.hide();
    }
  },
  watch: {
    isOpen: function (value) {
      if (value) {
        this.modal.show();
      } else {
        this.modal.hide();
      }
    },
  },
};
</script>
