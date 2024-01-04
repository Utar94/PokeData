import type { InjectionKey } from "vue";
import type { ToastOptions } from "logitar-vue3-ui";

export const toastKey = Symbol() as InjectionKey<(toast: ToastOptions) => void>;
