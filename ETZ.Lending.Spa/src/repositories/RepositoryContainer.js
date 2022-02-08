import ProductRepository from "./ProductRepository";

const repositories = {
  products: ProductRepository,
};
export default {
  get: (name) => repositories[name],
};
