import Client from "./clients/AxiosHttpClient";
const RESOURCE = "product";

export default {
  async fetchAll() {
    return Client.get(RESOURCE);
  },
  async fetch(id) {
    return Client.get(`${RESOURCE}/${id}`);
  },
  async search(query) {
    return Client.get(`${RESOURCE}/search?query=${query}`);
  },
  async create(payload) {
    return Client.post(RESOURCE, payload);
  },
  async update(id, payload) {
    return Client.put(`${RESOURCE}/${id}`, payload);
  },
  async delete(id) {
    return Client.delete(`${RESOURCE}/${id}`);
  },
};
