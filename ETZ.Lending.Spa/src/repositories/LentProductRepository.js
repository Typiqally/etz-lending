import Client from "./clients/AxiosHttpClient";
const RESOURCE = "lentProduct";

export default {
  async fetchAll() {
    return Client.get(RESOURCE);
  },
  async fetch(id) {
    return Client.get(`${RESOURCE}/${id}`);
  },
  async lend(productId, expiredAt) {
    return Client.post(
      `${RESOURCE}?productId=${productId}&expireDate=${expiredAt}`
    );
  },
  async extend(lentProductId, expiredAt) {
    return Client.put(
      `${RESOURCE}/${lentProductId}?newExpireDate=${expiredAt}`
    );
  },
  async delete(id) {
    return Client.delete(`${RESOURCE}/${id}`);
  },
};
