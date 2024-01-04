import type { InjectionKey } from "vue";
import type { ToastOptions } from "logitar-vue3-ui";

export type ToastUtils = {
  error: (toast: ToastOptions) => void;
  success: (toast: ToastOptions) => void;
  warning: (toast: ToastOptions) => void;
};

export const toastKey = Symbol() as InjectionKey<(toast: ToastOptions) => void>;
export const toastsKey = Symbol() as InjectionKey<ToastUtils>;
