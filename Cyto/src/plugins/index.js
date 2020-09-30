import { apiService } from "./axios";

export default function addPluginsOn(app) {
  app.use(apiService);
}
