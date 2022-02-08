import Client from "./clients/AxiosHttpClient";
const RESOURCE = "lentProduct";

export default {
  async fetchAll() {
    return Client.get(RESOURCE);
  },
  async fetch(id) {
    return Client.get(`${RESOURCE}/${id}`);
  },
  async lend(payload) {
    return Client.post(RESOURCE, payload);
  },
  async delete(id) {
    return Client.delete(`${RESOURCE}/${id}`);
  },
};
