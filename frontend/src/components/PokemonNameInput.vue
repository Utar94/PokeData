<script setup lang="ts">
import { TarInput, type InputSize } from "logitar-vue3-ui";
import { computed } from "vue";

const props = withDefaults(
  defineProps<{
    disabled?: boolean;
    floating?: boolean;
    id?: string;
    label?: string;
    max?: number;
    min?: number;
    modelValue?: string;
    placeholder?: string;
    required?: boolean;
    size?: InputSize;
  }>(),
  {
    floating: true,
    id: "name",
    label: "Name",
    max: 128,
  },
);

const inputPlaceholder = computed<string | undefined>(() => (props.floating ? props.placeholder ?? props.label : props.placeholder));

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <TarInput
    :disabled="disabled"
    :floating="floating"
    :id="id"
    :label="label"
    :max="max"
    :min="min"
    :model-value="modelValue"
    :placeholder="inputPlaceholder"
    :required="required"
    :size="size"
    type="text"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
