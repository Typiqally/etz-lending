import axios from "axios";

const baseURL = process.env["VUE_APP_BACKEND_URL"];

// Configure
export default axios.create({
  baseURL,
  headers: {
    // "Authorization": "Bearer xxxxx"
  },
});
