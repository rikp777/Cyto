import applyCaseMiddleware from "axios-case-converter";
import axios from "axios";

const errorMessage = true;
const showRequest = true;

const axiosInstance = axios.create({
  baseURL: "http://localhost",
  withCredentials: false,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json"
  },
  timeout: 3000
});

const apiClient = applyCaseMiddleware(axiosInstance);

export const apiService = {
  query(resource, params) {
    const request = apiClient.get(`${resource}`, params).catch(error => {
      if (errorMessage)
        throw `[Cyto] ApiService ${resource} \n ${error.response.data.message}`;
    });
    if (showRequest) console.log(request);
    return request;
  },
  getByParam(resource, param = "") {
    const request = apiClient.get(`${resource}${param}`).catch(error => {
      if (errorMessage)
        throw `[Cyto] ApiService ${resource} \n ${error.response.data.message}`;
    });
    if (showRequest) console.log(request);
    return request;
  },
  get(resource, slug = "") {
    const request = apiClient.get(`${resource}/${slug}`).catch(error => {
      if (errorMessage)
        throw `[Cyto] ApiService ${resource} \n ${error.response.data.message}`;
    });
    if (showRequest) console.log(request);
    return request;
  },
  post(resource, params) {
    const request = apiClient.post(`${resource}`, params).catch(error => {
      if (errorMessage)
        throw `[Cyto] ApiService ${resource} \n ${error.response.data.message}`;
    });
    if (showRequest) console.log(request);
    return request;
  },

  update(resource, slug, params) {
    const request = apiClient
      .put(`${resource}/${slug}`, params)
      .catch(error => {
        if (errorMessage)
          throw `[Cyto] ApiService ${resource}/${slug} \n ${error.response.data.message}`;
      });
    if (showRequest) console.log(request);
    return request;
  },

  put(resource, params) {
    const request = apiClient.put(`${resource}`, params).catch(error => {
      if (errorMessage)
        throw `[Cyto] ApiService ${resource} \n ${error.response.data.message}`;
    });
    if (showRequest) console.log(request);
    return request;
  },

  delete(resource, slug) {
    const request = apiClient.delete(`${resource}/${slug}`).catch(error => {
      if (errorMessage)
        throw `[Cyto] ApiService ${resource}/${slug} \n ${error.response.data.message}`;
    });
    if (showRequest) console.log(request);
    return request;
  }
};
