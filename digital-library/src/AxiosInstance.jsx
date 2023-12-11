import axios from "axios";

export const AxiosInstance = axios.create({
    baseURL: process.env.REACT_APP_BASE_URL,
    timeout: 1000,
    headers: {
      common: { 
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    }
  });