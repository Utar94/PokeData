<script setup lang="ts">
import { TarInput, type InputSize, parsingUtils } from "logitar-vue3-ui";
import { computed } from "vue";

const props = withDefaults(
  defineProps<{
    disabled?: boolean;
    floating?: boolean;
    id?: string;
    label?: string;
    max?: number;
    min?: number;
    modelValue?: number;
    placeholder?: string;
    required?: boolean;
    size?: InputSize;
  }>(),
  {
    floating: true,
    id: "number",
    label: "Number",
    max: 9999,
    min: 1,
    modelValue: 0,
  },
);

const inputPlaceholder = computed<string | undefined>(() => (props.floating ? props.placeholder ?? props.label : props.placeholder));
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
    step="1"
    type="number"
    @update:model-value="$emit('update:model-value', parsingUtils.parseNumber($event) ?? 0)"
  />
</template>
