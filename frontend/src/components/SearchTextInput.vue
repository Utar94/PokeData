<script setup lang="ts">
import { TarButton, TarInput, type InputSize } from "logitar-vue3-ui";
import { computed } from "vue";

const props = withDefaults(
  defineProps<{
    disabled?: boolean;
    floating?: boolean;
    id?: string;
    label?: string;
    modelValue?: string;
    placeholder?: string;
    required?: boolean;
    size?: InputSize;
  }>(),
  {
    floating: true,
    id: "search-text",
    label: "Search",
  },
);

const inputPlaceholder = computed<string | undefined>(() => (props.floating ? props.placeholder ?? props.label : props.placeholder));

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <TarInput
    described-by="clear-search-text"
    :disabled="disabled"
    :floating="floating"
    :label="label"
    :model-value="modelValue"
    :placeholder="inputPlaceholder"
    :required="required"
    :size="size"
    type="search"
    @update:model-value="$emit('update:model-value', $event)"
  >
    <template #append>
      <TarButton :icon="['fas', 'times']" id="clear-search-text" variant="danger" @click="$emit('update:model-value', undefined)" />
    </template>
  </TarInput>
</template>

<style scoped>
input[type="search"]::-webkit-search-decoration,
input[type="search"]::-webkit-search-cancel-button,
input[type="search"]::-webkit-search-results-button,
input[type="search"]::-webkit-search-results-decoration {
  -webkit-appearance: none;
}
</style>
