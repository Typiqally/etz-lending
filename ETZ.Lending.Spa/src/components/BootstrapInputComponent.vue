<template>
  <div>
    <label :for="id">{{ label }}</label>
    <input
      ref="input"
      class="form-control"
      :id="id"
      :name="name"
      :type="type"
      :placeholder="placeholder"
      :accept="accept"
      :class="{ 'is-invalid': !!errorMessage }"
      @input="handleInput"
      @blur="handleBlur"
    />

    <div class="invalid-feedback" v-show="errorMessage">
      {{ errorMessage }}
    </div>
  </div>
</template>

<script>
import { useField } from "vee-validate";

export default {
  name: "BootstrapInput",
  props: {
    modelValue: {},
    id: {
      type: String,
      default: null,
    },
    name: {
      type: String,
      required: true,
    },
    type: {
      type: String,
      default: "text",
    },
    label: {
      type: String,
      required: true,
    },
    placeholder: {
      type: String,
      default: "",
    },
    accept: {
      type: String,
      default: "",
    },
  },
  setup(props) {
    const { value, errorMessage, handleBlur, handleChange, meta } = useField(
      props.name,
      undefined,
      {
        initialValue: props.modelValue,
      }
    );

    return {
      handleChange,
      handleBlur,
      errorMessage,
      value,
      meta,
    };
  },
  mounted() {
    this.updateValue();
  },
  methods: {
    handleInput(e) {
      this.handleChange(e);
      this.updateValue();
      this.$emit("update:modelValue", e.target.value);
    },
    updateValue() {
      if (this.type !== "file") {
        this.$refs.input.value = this.value;
      }
    },
  },
};
</script>
