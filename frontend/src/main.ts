import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
// import "logitar-vue3-ui/dist/style.css"; // TODO(fpion): export css
import { createApp } from "vue";
import { createPinia } from "pinia";

import "./fontAwesome";
import App from "./App.vue";
import router from "./router";
import { useToastStore } from "./stores/toast";

const app = createApp(App);

app.use(createPinia());
app.use(router);

app.config.errorHandler = (e) => {
  console.error(e);

  const toasts = useToastStore();
  toasts.add({
    duration: 15 * 1000,
    fade: true,
    text: "An unexpected error has occurred. Please retry again later or contact us if the issue persists.",
    title: "Error",
    variant: "danger",
  });
};

app.mount("#app");

// Add to docker-compose.yml
