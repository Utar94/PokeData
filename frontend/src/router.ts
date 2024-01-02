import { createRouter, createWebHistory } from "vue-router";

import RosterView from "./views/RosterView.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "Roster",
      component: RosterView,
    },
    // {
    //   path: "/about",
    //   name: "about",
    //   // route level code-splitting
    //   // this generates a separate chunk (About.[hash].js) for this route
    //   // which is lazy-loaded when the route is visited.
    //   component: () => import("./views/AboutView.vue"),
    // },
  ],
});

export default router;
