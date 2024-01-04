<script setup lang="ts">
import { RouterView } from "vue-router";
import { TarToaster, type ToastOptions } from "logitar-vue3-ui";
import { provide } from "vue";

import { toastKey, toastsKey, type ToastUtils } from "./App";
import { useToastStore } from "./stores/toast";

const toasts = useToastStore();

function toast(toast: ToastOptions): void {
  toasts.add(toast);
}
provide(toastKey, toast);

const toastUtils: ToastUtils = {
  error(options: ToastOptions): void {
    toast({
      ...options,
      duration: options.duration ?? 5000,
      fade: options.fade ?? true,
      text: options.text ?? "An unexpected error has occurred. Please retry again later or contact us if the issue persists.",
      title: options.title ?? "Error",
      variant: options.variant ?? "danger",
    });
  },
  success(options: ToastOptions): void {
    toast({
      ...options,
      duration: options.duration ?? 5000,
      fade: options.fade ?? true,
      title: options.title ?? "Success",
      variant: options.variant ?? "success",
    });
  },
  warning(options: ToastOptions): void {
    toast({
      ...options,
      duration: options.duration ?? 5000,
      fade: options.fade ?? true,
      title: options.title ?? "Warning",
      variant: options.variant ?? "warning",
    });
  },
};
provide(toastsKey, toastUtils);
</script>

<template>
  <RouterView />
  <TarToaster :toasts="toasts.toasts" @hidden="toasts.remove" />
</template>
