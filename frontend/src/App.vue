<script setup lang="ts">
import { RouterView } from "vue-router";
import { TarToaster, type ToastOptions } from "logitar-vue3-ui";
import { provide } from "vue";

import { toastKey } from "./App";
import { useToastStore } from "./stores/toast";

const toasts = useToastStore();

function toast(toast: ToastOptions): void {
  toasts.add(toast);
}
provide(toastKey, toast);
</script>

<template>
  <RouterView />
  <TarToaster :toasts="toasts.toasts" @hidden="toasts.remove" />
</template>
