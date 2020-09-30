import { apiService } from "@/plugins/axios";

const experimentApi = "experiment";
export const experimentService = {
  create(params) {
    return apiService.post(experimentApi, params);
  },
  update(slug, params) {
    return apiService.update(experimentApi, slug, params);
  },
  delete(slug) {
    return apiService.delete(experimentApi, slug);
  },
  get(slug) {
    return apiService.get(experimentApi, slug);
  },
  getAll() {
    return apiService.get(experimentApi);
  }
};
