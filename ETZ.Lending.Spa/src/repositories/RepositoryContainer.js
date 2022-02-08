import ProductRepository from "./ProductRepository";
import LentProductRepository from "@/repositories/LentProductRepository";

const repositories = {
  products: ProductRepository,
  lentProducts: LentProductRepository,
};
export default {
  get: (name) => repositories[name],
};
